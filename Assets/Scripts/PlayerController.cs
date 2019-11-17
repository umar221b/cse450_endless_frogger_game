using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

class PlayerController : MonoBehaviour
{

  int activeWorldPart = 0;
  Animator anim;
  private GameObject mainCamera;
  
  private float moveSpeed = 5f;
  private float gridSize = 1f;
  private enum Orientation
  {
    Horizontal,
    Vertical
  };
  private Orientation gridOrientation = Orientation.Vertical;
  private Vector2 input;
  private bool isMoving = false;
  private Vector3 startPosition;
  private Vector3 endPosition;
  private float t;
  private float factor = 1f;

  int counter = 0;
  int counterMax = 0;

  void Start()
  {
    anim = GetComponent<Animator>();
    mainCamera = GameManager.instance.getMainCamera();
  }

  public void Update()
  {
    if(GameManager.instance.gamePaused())
      return;

    input = new Vector2(0, 0);
    int onlyOneStep = 0;

    if (!isMoving)
    {
      if (Input.GetKey(KeyCode.DownArrow))
      {
        ++onlyOneStep;
        input.y = -1;
      }
      if (Input.GetKey(KeyCode.UpArrow))
      {
        ++onlyOneStep;
        input.y = 1;
      }

      if (Input.GetKey(KeyCode.RightArrow))
      {
        ++onlyOneStep;
        input.x = 1;
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        ++onlyOneStep;
        input.x = -1;
      }

      if (onlyOneStep == 1 && input != Vector2.zero) {
        anim.SetInteger("verticalMovement", (int)input.y);
        anim.SetInteger("horizontalMovement", (int)input.x);
        StartCoroutine(move(transform));
      }
    }
  }

  public void LateUpdate() {
    Vector3 cameraPosition = mainCamera.transform.position;
    float cameraHeight = mainCamera.GetComponent<Camera>().orthographicSize;
    if (transform.position.y > cameraPosition.y + cameraHeight) {
            //Destroy(GameObject.FindGameObjectWithTag("Keese"));
         //   GameManager.instance.Astar.GetComponent<AstarPath>().graphs.GetValue.GetComponentInParent
      GameManager.instance.monsterManager.MoveSpawnPoints(1);
      mainCamera.transform.position = cameraPosition + new Vector3(0, 16f, 0);
      int newActiveWorldPart = GameManager.instance.getActiveWorldPart();
      if (newActiveWorldPart > 1 && activeWorldPart != newActiveWorldPart) {
        GameManager.instance.generateNextWorldPart();
        activeWorldPart = newActiveWorldPart;
      }
    }

    else if (transform.position.y < cameraPosition.y - cameraHeight) {
      
      //Destroy(GameObject.FindGameObjectWithTag("Keese"));
      GameManager.instance.monsterManager.MoveSpawnPoints(-1);
      mainCamera.transform.position = cameraPosition - new Vector3(0, 16f, 0);
    }
  }

  public IEnumerator move(Transform transform)
  {
    isMoving = true;
    anim.SetBool("isMoving", isMoving);
    startPosition = transform.position;
    t = 0;

    if (gridOrientation == Orientation.Horizontal)
    {
      endPosition = new Vector3(startPosition.x + input.x * gridSize,
      startPosition.y, startPosition.z + input.y * gridSize);
    }
    else
    {
      endPosition = new Vector3(startPosition.x + input.x * gridSize,
      startPosition.y + input.y * gridSize, startPosition.z);
    }

    Vector3 direction = endPosition - startPosition;

    float distance = Vector3.Distance(startPosition, endPosition);
    // Debug.DrawLine(startPosition, endPosition, Color.black, 3f);
    RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance);
    // RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, distance, LayerMask.GetMask("Blocked Cells")); // this works GetMask instead of NameToLayer.
    bool hitBlockedCell = false;
    foreach (RaycastHit2D hit in hits)
    {
      if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Blocked Cells")) {
        hitBlockedCell = true;
        break;
      }
    }
    if (!hitBlockedCell) {
      counter += Mathf.RoundToInt(direction.y);
      counterMax = Mathf.Max(counterMax, counter);
      GameManager.instance.updateScore(counterMax);
    }

    while (!hitBlockedCell && t < 1f)
    {
      t += Time.deltaTime * (moveSpeed / gridSize) * factor;
      transform.position = Vector3.Lerp(startPosition, endPosition, t);
      yield return null;
    }

    isMoving = false;
    anim.SetBool("isMoving", isMoving);
    yield return 0;
  }
}

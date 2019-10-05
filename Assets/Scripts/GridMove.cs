using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

class GridMove : MonoBehaviour {

    Animator anim;

  private float moveSpeed = 5f;
  private float gridSize = 1f;
  private enum Orientation {
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

  void Start() {

        anim = GetComponent<Animator>();
    }
  public void Update() {
    input = new Vector2(0, 0);

    if (!isMoving) {
      if (Input.GetKey(KeyCode.UpArrow))
      {
                anim.Play("moveUp");
                input.y = 1;
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
                anim.Play("moveDown");
                input.y = -1;
      }
      if (Input.GetKey(KeyCode.RightArrow))
      {
                anim.Play("moveRight");
                input.x = 1;
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
                anim.Play("moveLeft");
                input.x = -1;
      }

      if (input != Vector2.zero) {
        StartCoroutine(move(transform));
      }
    }
  }

  public IEnumerator move(Transform transform) {
    isMoving = true;
    startPosition = transform.position;
    t = 0;

    if(gridOrientation == Orientation.Horizontal) {
      endPosition = new Vector3(startPosition.x + input.x * gridSize,
      startPosition.y, startPosition.z + input.y * gridSize);
    } else {
      endPosition = new Vector3(startPosition.x + input.x * gridSize,
      startPosition.y + input.y * gridSize, startPosition.z);
    }

    Vector3 direction = endPosition - startPosition;
    float distance = Vector3.Distance(startPosition, endPosition);
    // Debug.DrawLine(startPosition, endPosition, Color.black, 3f);
    RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance);
    // RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, distance, LayerMask.GetMask("Blocked Cells")); // this works GetMask instead of NameToLayer.
    bool hitBlockedCell = false;
    foreach (RaycastHit2D hit in hits) {
      if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Blocked Cells"))
        hitBlockedCell = true;
      else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Goal"))
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    while (!hitBlockedCell && t < 1f) {
      t += Time.deltaTime * (moveSpeed/gridSize) * factor;
      transform.position = Vector3.Lerp(startPosition, endPosition, t);
      yield return null;
    }

    isMoving = false;
    yield return 0;
  }
}

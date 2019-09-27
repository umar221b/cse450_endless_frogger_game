using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour {

  public Sprite spriteUp;
  public Sprite spriteDown;
  public Sprite spriteRight;
  public Sprite spriteLeft;

  private SpriteRenderer spriteRenderer;


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
    spriteRenderer = GetComponent<SpriteRenderer>();
  }
  public void Update() {
    Sprite newSprite = spriteRenderer.sprite;
    input = new Vector2(0, 0);

    if (!isMoving) {
      if (Input.GetKey(KeyCode.UpArrow))
      {
        newSprite = spriteUp;
        input.y = 1;
      }
      if (Input.GetKey(KeyCode.DownArrow))
      {
        newSprite = spriteDown;
        input.y = -1;
      }
      if (Input.GetKey(KeyCode.RightArrow))
      {
        newSprite = spriteRight;
        input.x = 1;
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
        newSprite = spriteLeft;
        input.x = -1;
      }

      if (input != Vector2.zero) {
        if (spriteRenderer.sprite != newSprite) {
          spriteRenderer.sprite = newSprite;
        }
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
    bool hitBlockedCell = false;
    foreach (RaycastHit2D hit in hits) {
      if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Blocked Cells"))
        hitBlockedCell = true;
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

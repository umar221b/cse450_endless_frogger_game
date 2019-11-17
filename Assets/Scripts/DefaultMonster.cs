using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultMonster : MonoBehaviour
{

  Animator anim;

  int direction;
  bool canStartMove = false;
  float speed;

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.GetComponent<PlayerController>())
    {
      GameManager.instance.updateHighscore();
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    if (other.gameObject.layer == LayerMask.NameToLayer("Blocked Cells")) {
      direction *= -1;
      if (direction == -1)
        anim.Play("MoveLeft");
      else
        anim.Play("MoveRight");
    }
  }

  void OnBecameInvisible()
  {
    Destroy(gameObject);
  }

  public void init(int direction, float speed = 0.1f)
  {
    this.direction = direction;
    this.speed = speed;
    canStartMove = true;
    anim = GetComponent<Animator>();
    if (direction == -1)
      anim.Play("MoveLeft");
  }

  void Update()
  {
    if(GameManager.instance.gamePaused())
      return;
    if (canStartMove)
      transform.position += new Vector3(direction * speed, 0, 0);
  }
}

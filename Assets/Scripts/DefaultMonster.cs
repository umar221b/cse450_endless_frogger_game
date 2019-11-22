using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMonster : Monster
{

  Animator anim;

  int direction;
  bool canStartMove = false;
  float speed;

  public override void OnCollisionEnter2D(Collision2D other) {
    base.OnCollisionEnter2D(other);
    if (other.gameObject.layer == LayerMask.NameToLayer("Blocked Cells")) {
      direction *= -1;
      if (direction == -1)
        anim.Play("MoveLeft");
      else if (direction == 1)
        anim.Play("MoveRight");
    }
  }

  public void init(int direction, float speed = 0.1f)
  {
    this.direction = direction;
    this.speed = speed;
    canStartMove = true;
    anim = GetComponent<Animator>();
    if (gameObject.tag != "One Way Monster" && direction == -1)
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

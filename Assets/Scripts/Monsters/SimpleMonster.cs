using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMonster : Monster
{
  Animator anim;
  private int direction;

  public override void Start() {
    base.Start();
    anim = GetComponent<Animator>();
    if (gameObject.transform.position.x > 5f - 0.01f)
      direction = -1;
    else if (gameObject.transform.position.x < -5f + 0.01f)
      direction = 1;
    anim.SetInteger("direction", direction);
  }

  public override void Update()
  {
    if(GameManager.instance.gamePaused())
      return;
    transform.position += new Vector3(direction * speed, 0, 0);
  }

  public override void OnCollisionEnter2D(Collision2D other) {
    base.OnCollisionEnter2D(other);
    if (other.gameObject.layer == LayerMask.NameToLayer("Blocked Cells")) {
      direction *= -1;
      anim.SetInteger("direction", direction);
    }
  }
}

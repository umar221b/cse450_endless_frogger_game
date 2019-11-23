using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

  protected float speed;

  public virtual void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.GetComponent<PlayerController>()) {
      GameManager.instance.lose();
    }
  }

  private void OnBecameInvisible() {
     Destroy(gameObject);
  }

  public virtual void Start() {
    float speedOffset = 0.05f;
    speed = Mathf.Min(speedOffset + (GameManager.instance.getDifficulty() * 0.002f), 0.1f);
  }

  public virtual void Update() {
    if(GameManager.instance.gamePaused())
      return;
  }
}

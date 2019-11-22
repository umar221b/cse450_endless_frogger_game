using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
  public virtual void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.GetComponent<PlayerController>()) {
      GameManager.instance.restartGame();
    }
  }

  private void OnBecameInvisible() {
       Destroy(gameObject);
  }
}

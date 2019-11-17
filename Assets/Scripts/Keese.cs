using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keese : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            GameManager.instance.updateHighscore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnBecameInvisible()
    {
         Destroy(gameObject);
    }
}

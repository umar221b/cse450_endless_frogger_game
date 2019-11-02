using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    //int flip = 1;
    int direction;
    bool canStartMove = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<GridMove>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //if (LayerMask.NameToLayer("Obstacles") == LayerMask.NameToLayer("Blocking Cells")){
        //        transform.position += new Vector3(-0.1f, 0, 0);
        //    }

        //transform.position += new Vector3(flip*0.1f, 0, 0);
        if (other.gameObject.layer == LayerMask.NameToLayer("Blocked Cells")){
            direction *= -1;
            print("something");
        }     
            print("something");
            // Destroy(gameObject);


        }
        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    public void init(int direction)
    {
        this.direction = direction;
        print("New Obstacle");
        print(direction);
        print(transform.position);
        canStartMove = true;
    }
    void Update()
    {
        //if (other.gameObject("Obstacles") == LayerMask.NameToLayer("Blocking Cells"))
        //{
        //    transform.position += new Vector3(-0.1f, 0, 0);
        //}
        //else
        if (canStartMove)
            transform.position += new Vector3(direction*0.1f, 0, 0);
        
        // transform.position += new Vector3(2f, 0, 0);//_startPosition + new Vector3(amplitude * Mathf.Sin(frequency * Time.time + 3 * Mathf.PI / 2) + offset, 0.0f, 0.0f);
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    transform.position += new Vector3(2f, 0, 0);
        //}
        //else
        //{
        //    transform.position = _startPosition + new Vector3(amplitude * Mathf.Sin(frequency * Time.time + 3 * Mathf.PI / 2) + offset, 0.0f, 0.0f);
        //}
        //print(distance * Mathf.Sin(speed * Time.time + Mathf.PI / 2));
    }
}



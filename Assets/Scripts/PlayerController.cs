using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool handled = true;
    Rigidbody2D _rb;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
       // anim.Play("upidle");
        if (handled)
        {


            StartCoroutine(Example());
            if (Input.GetKey(KeyCode.UpArrow))
            {

                transform.position += new Vector3(0, 0.5f, 0);
                anim.Play("moveForward");
            }
           // else
           // {
           //     anim.Play("idleLeft");
           // }
           
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -0.5f, 0);
                anim.Play("moveBackward");

            }
          //  else
         //   {
         //       anim.Play("idleBackward");
         //   }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(0.5f, 0, 0);
                anim.Play("moveRight");
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-0.5f, 0, 0);
                anim.Play("moveLeft");
                //anim.Play("moveLeft");
            }
        }
        IEnumerator Example()
        {
            handled = false;
            yield return new WaitForSecondsRealtime(.25f);
            handled = true;
        }
    }
}

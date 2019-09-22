using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool handled = true;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (handled)
        {


            StartCoroutine(Example());
            if (Input.GetKey(KeyCode.UpArrow))
            {

                transform.position += new Vector3(0, 0.5f, 0);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -0.5f, 0);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(0.5f, 0, 0);

            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-0.5f, 0, 0);

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

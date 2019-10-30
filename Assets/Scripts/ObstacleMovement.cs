using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleMovement : MonoBehaviour
{
    private Vector3 _startPosition;

    private float distance = 3;//amp
    private float speed = 1f;//frequency
    private float frequency = 1f;//frequency = 0.5 for 9x16, frequency = 0.1666f for 16x9
    private float screenWidth = Screen.width;
    private float amplitude = 18;//amplitude = 6 for 9x16, amplitude = 18 for 16x9
    private float offset = 15;//offset = 5 for 9x16, offset = 15 for 16x9
    // Start is called before the first frame update
    void Start()
    {

        float u = Mathf.Cos(speed * Mathf.PI + Mathf.PI / 2) / speed;
        distance = screenWidth / u;
        _startPosition = transform.position;
        //  print(screenWidth);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.1f, 0, 0);

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
    // private void OnBecameInvisible()
    // {
    void OnCollisionEnter2D(Collision2D collision)
    {


        print("something");
        Destroy(gameObject);


    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // }
}

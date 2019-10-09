using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleMovement : MonoBehaviour
{
    private Vector3 _startPosition;
    
    private float distance;//amp
    private float speed=1f;//frequency
    private float screenWidth = Screen.width;
    // Start is called before the first frame update
    void Start()
    {
        float u = Mathf.Cos(speed * Mathf.PI + Mathf.PI / 2) / speed;
        distance = screenWidth / u;
        _startPosition = transform.position;
        print(screenWidth);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = _startPosition + new Vector3((distance*Mathf.Sin(speed*Time.time+Mathf.PI/2*3)),0.0f , 0.0f);
        //print(distance * Mathf.Sin(speed * Time.time + Mathf.PI / 2));
    }
}

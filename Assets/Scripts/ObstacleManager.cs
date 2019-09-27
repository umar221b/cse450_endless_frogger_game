using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public int numOfObstacles;
    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
      for (int i = 0; i < numOfObstacles; i++)
        {
            Vector3 obstaclePosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            obstaclePosition.y += 2 * i + 3.5f;
            obstaclePosition.z = 0;
            Instantiate(obstacle, obstaclePosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

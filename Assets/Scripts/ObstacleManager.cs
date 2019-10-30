using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public int numOfObstacles;
    public GameObject obstacle;
    public float maxTime = 5;
    public float minTime = 2;
    public float minAsteroidDelay = 20f;
    public float maxAsteroidDelay = 10f;
    public bool wandering = false;

    public float timeElapsed;
    public float asteroidDelay;
    private float time;
    public Transform[] spawnPoint;

    //The time to spawn the object
    private float spawnTime;
    private float timer = 0.0f;
    public float waitingTime = 5.0f;
    private Vector3 _startPosition;

    private float distance = 3;//amp
    private float speed = 1f;//frequency
    private float frequency = 0.166f;//frequency = 0.5 for 9x16, frequency = 0.1666f for 16x9
    private float screenWidth = Screen.width;
    private float amplitude = 18;//amplitude = 6 for 9x16, amplitude = 18 for 16x9
    private float offset = 15;//offset = 5 for 9x16, offset = 15 for 16x9
    // Start is called before the first frame update

    void Start()
    {
        //SetRandomTime();
        //time = minTime;
        wandering = true;
        StartCoroutine("AsteroidSpawnTimer");

        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    wandering = true;
        //    StartCoroutine("AsteroidSpawnTimer");
        //}
        //else
        //{
        //    for (int i = 0; i < numOfObstacles; i++)
        //    {
        //        Vector3 obstaclePosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        //        obstaclePosition.y += 2 * i + 3.5f;
        //        obstaclePosition.z = 0;
        //        Instantiate(obstacle, obstaclePosition, Quaternion.identity);
        //       // print(obstaclePosition);
        //    }

        //}

        //Invoke("StopExecution", 3f);


    }
    void StopExecution()
    {
        StopCoroutine("AsteroidSpawnTimer");
    }
    void Update()
    {
        //Increment passage of time for each frame of the game
        // transform.position = _startPosition + new Vector3(amplitude * Mathf.Sin(frequency * Time.time + 3 * Mathf.PI / 2) + offset, 0.0f, 0.0f);
        asteroidDelay = Random.Range(2f, 6f);
    }
    void SpawnAsteroid()
    {


    }
    IEnumerator AsteroidSpawnTimer()
    {

        //wait
        yield return new WaitForSeconds(asteroidDelay);
        //Spawn
        //for (int i = 0; i < numOfObstacles; i++)
        //{
        //    Vector3 obstaclePosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        //    obstaclePosition.y += 2 * i + 3.5f;
        //    obstaclePosition.z = 0;
        Transform randSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(obstacle, randSpawnPoint.position, Quaternion.identity);
        // print(obstaclePosition);
        //}
        //repeat
        StartCoroutine("AsteroidSpawnTimer");
        wandering = false;
    }
}
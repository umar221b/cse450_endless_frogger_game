using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject spawnPoint;

    public float speed;
    public float minSpawnDelay;
    public float maxSpawnDelay;

    GameObject[] spawnPointsLeft;
    GameObject[] spawnPointsRight;

    float monsterDelay;

    void Awake() {
      spawnPointsLeft = new GameObject[15];
      spawnPointsRight = new GameObject[15];

      float startX = -5f;
      float startY = -6.5f;

      Vector3 positionLeft = new Vector3(startX, startY, 0);
      Vector3 positionRight = new Vector3(startX * -1, startY, 0);
      for (int i = 0; i < 15; ++i) {
        spawnPointsLeft[i] = Instantiate(spawnPoint, positionLeft, Quaternion.identity);
        spawnPointsRight[i] = Instantiate(spawnPoint, positionRight, Quaternion.identity);
        positionLeft.y += 1;
        positionRight.y += 1;
      }
    }

    public void MoveSpawnPoints(int direction) {
      StopCoroutine("MonsterSpawnTimer");
      for (int i = 0; i < 15; ++i) {
        spawnPointsLeft[i].transform.position += new Vector3(0, 16 * direction, 0);
        spawnPointsRight[i].transform.position += new Vector3(0, 16 * direction, 0);
      }
      StartCoroutine("MonsterSpawnTimer");
    }

    void Start()
    {
        StartCoroutine("MonsterSpawnTimer");
    }

    void StopExecution()
    {
        StopCoroutine("MonsterSpawnTimer");
    }

    void Update()
    {
        monsterDelay = Random.Range(Mathf.Max(0.3f, minSpawnDelay - (GameManager.instance.difficulty * 0.08f)), Mathf.Max(0.6f, maxSpawnDelay - (GameManager.instance.difficulty * 0.08f)));
    }

    IEnumerator MonsterSpawnTimer()
    {
        yield return new WaitForSeconds(monsterDelay);

        int randSpawnPointNumber = Random.Range(0, spawnPointsLeft.Length + spawnPointsRight.Length);

        Transform spawnPointTransform;
        GameObject curMonster;
        float monsterSpeed = Mathf.Min(speed + (GameManager.instance.difficulty * 0.002f), 0.1f);
        if (randSpawnPointNumber < spawnPointsLeft.Length)
        {
            spawnPointTransform = spawnPointsLeft[randSpawnPointNumber].transform;
            curMonster = Instantiate(monsters[Random.Range(0, monsters.Length)], spawnPointTransform.position, Quaternion.identity);
            switch (curMonster.tag) {
                case "Keese":
                break;
                default:
                curMonster.GetComponent<DefaultMonster>().init(1, monsterSpeed);
                break;
            }
        }
        else
        {
            spawnPointTransform = spawnPointsRight[randSpawnPointNumber - spawnPointsLeft.Length].transform;
            curMonster = Instantiate(monsters[Random.Range(0, monsters.Length)], spawnPointTransform.position, Quaternion.identity);
            switch (curMonster.tag)
            {
                case "Keese":
                break;
                default:
                curMonster.GetComponent<DefaultMonster>().init(-1, monsterSpeed);
                break;
            }
        }
        Destroy(curMonster, 10f);
        StartCoroutine("MonsterSpawnTimer");
    }
}

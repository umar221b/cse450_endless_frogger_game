using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject spawnPoint;

    public float minSpawnDelay;
    public float maxSpawnDelay;

    private GameObject[] spawnPointsLeft;
    private GameObject[] spawnPointsRight;

    private float monsterDelay;

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

    void Update()
    {
        monsterDelay = calcMonsterDelay();
    }

    float calcMonsterDelay() {
      return Random.Range(Mathf.Max(0.3f, minSpawnDelay - (GameManager.instance.getDifficulty() * 0.08f)), Mathf.Max(0.6f, maxSpawnDelay - (GameManager.instance.getDifficulty() * 0.08f)));
    }

    IEnumerator MonsterSpawnTimer()
    {
        yield return new WaitForSeconds(monsterDelay);

        int randSpawnPointNumber = Random.Range(0, spawnPointsLeft.Length + spawnPointsRight.Length);
        Transform spawnPointTransform;
        if (randSpawnPointNumber < spawnPointsLeft.Length)
          spawnPointTransform = spawnPointsLeft[randSpawnPointNumber].transform;
        else
          spawnPointTransform = spawnPointsLeft[randSpawnPointNumber - spawnPointsLeft.Length].transform;
        GameObject curMonster = Instantiate(monsters[Random.Range(0, monsters.Length)], spawnPointTransform.position, Quaternion.identity);

        Destroy(curMonster, 10f);
        StartCoroutine("MonsterSpawnTimer");
    }
}

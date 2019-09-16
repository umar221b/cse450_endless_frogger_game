using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public GameObject[] tiles;


    private Transform boardHolder;

    void BoardSetup()
    {
      boardHolder = new GameObject ("Board").transform;

      for (int i = - t(rows); i < t(rows); ++i) {
        for (int j = - t(columns); j < t(columns); ++j) {
          int choice = (Mathf.Abs(j) + Mathf.Abs(i)) % tiles.Length;
          GameObject tile = tiles[choice];
          GameObject instance = Instantiate (tile, new Vector3 (i, j, 0f), Quaternion.identity) as GameObject;
          instance.transform.SetParent(boardHolder);
        }
      }
    }

    int t(int x) {
      return x / 2;
    }
    public void SetupScene()
    {
      BoardSetup();
    }
}

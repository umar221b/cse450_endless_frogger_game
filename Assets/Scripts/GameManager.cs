using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public BoardManager boardScript;

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        InitializeGame();
    }

    void InitializeGame()
    {
        boardScript.SetupScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void RestartGame()
    {

    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    // Outlets
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    // Methods
    void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        ShowMainMenu();
        gameObject.SetActive(true);
        Time.timeScale = 0;
        GameManager.instance.pause();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (GameManager.instance != null)
            GameManager.instance.unPause();
    }

    void SwitchMenu(GameObject someMenu)
    {
        // Turn off all menus
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        // Turn on requested menu
        someMenu.SetActive(true);
    }

    public bool mainMenuIsActive()
    {
        return mainMenu.activeInHierarchy;
    }

    public void ShowMainMenu()
    {
        SwitchMenu(mainMenu);
    }

    public void ShowOptionsMenu()
    {
        SwitchMenu(optionsMenu);
    }

    public void ShowCreditsMenu()
    {
        SwitchMenu(creditsMenu);
    }

    public void resetHighscore()
    {
        GameManager.instance.resetHighscore();
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT");
        //Application.Quit();
        SceneManager.LoadScene("Main Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.instance.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if(GameManager.instance != null)
        {
            GameManager.instance.isPaused = false;
        }
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

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("highscore");
        GameManager.instance.score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

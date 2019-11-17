using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

  public DefaultMonsterManager defaultMonsterManager;
  private LandscapeManager landscapeManager;
  private AudioSource audioSource;
  public int difficulty;
  public GameObject player;
  public GameObject grid;
  public GameObject mainCamera;

  public Text scoreText;
  public Text highscoreText;

  private int score;
  private int highscore;
  private bool isPaused;

  void Awake() {

    //Check if instance already exists
    if (instance == null)
      //if not, set instance to this
      instance = this;
    //If instance already exists and it's not this:
    else if (instance != this)
      //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
      Destroy(gameObject);

    //Get a component reference to the attached LandscapeManager script
    landscapeManager = GetComponent<LandscapeManager>();
    landscapeManager.init();

    defaultMonsterManager = GetComponent<DefaultMonsterManager>();
    audioSource = GetComponent<AudioSource>();
  }

  void Start() {
    this.highscore = PlayerPrefs.GetInt("highscore");
    highscoreText.text = highscore.ToString();
  }

  void Update() {
    difficulty = getActiveWorldPart();
    scoreText.text = score.ToString();
    if (score > highscore)
    highscoreText.text = score.ToString();

    // Trigger menu
    if(Input.GetKeyDown(KeyCode.Escape)) {
      if (MenuManager.instance.mainMenuIsActive())
        MenuManager.instance.Hide();
      else
        MenuManager.instance.Show();
    }

    if(gamePaused())
      return;
  }

  public int getActiveWorldPart() {
    return score / 16;
  }

  public GameObject getPlayer() {
    return player;
  }
  public GameObject getGrid() {
    return grid;
  }

  public GameObject getMainCamera() {
    return mainCamera;
  }

  public int getScore() {
    return score;
  }

  public int getHighscore() {
    return highscore;
  }

  public void updateScore(int newScore) {
    this.score = newScore;
  }

  public void updateHighscore() {
    if (score > highscore) {
      this.highscore = score;
      PlayerPrefs.SetInt("highscore", this.highscore);
    }
  }

  public void resetHighscore() {
    this.highscore = 0;
    PlayerPrefs.SetInt("highscore", 0);
  }

  public void generateNextWorldPart() {
    int curPart = getActiveWorldPart();
    landscapeManager.generateBoundaryRow(curPart * 16 - 17);
    landscapeManager.generateWorldPart((curPart + 1) % 3);
    landscapeManager.displayWorldPart(curPart + 1);

  }

  public bool gamePaused() {
    return isPaused;
  }
  public void pause() {
    audioSource.Pause();
    isPaused = true;
  }

  public void unPause() {
    audioSource.Play();
    isPaused = false;
  }
}

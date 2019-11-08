using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

    public ObstacleManager obstacleManager;
    private LandscapeManager landscapeManager;

    public int difficulty;
    public GameObject player;
    public GameObject grid;
    public GameObject mainCamera;

    public Text scoreText;
    public Text highscoreText;

    private int score;
    private int highscore;

    void Awake() {

      //Check if instance already exists
      if (instance == null)
        //if not, set instance to this
        instance = this;
      //If instance already exists and it's not this:
      else if (instance != this)
        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        Destroy(gameObject);

      //Get a component reference to the attached BoardManager script
      landscapeManager = GetComponent<LandscapeManager>();
      landscapeManager.init();

      obstacleManager = GetComponent<ObstacleManager>();
    }

    void Start() {
      this.highscore = PlayerPrefs.GetInt("highscore");
      highscoreText.text = highscore.ToString();
    }

    void Update() {
      difficulty = score / 16;
      scoreText.text = score.ToString();
      if (score > highscore)
        highscoreText.text = score.ToString();
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
}

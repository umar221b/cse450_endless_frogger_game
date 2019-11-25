using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour {
  public Text scoreValueText;
  public Text highscoreValueText;

  void Awake() {
    int score = PlayerPrefs.GetInt("score");
    int highscore = PlayerPrefs.GetInt("highscore");
    scoreValueText.text = score.ToString();
    highscoreValueText.text = highscore.ToString();
  }
}

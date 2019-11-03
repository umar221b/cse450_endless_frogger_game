using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;

    public ObstacleManager obstacleManager;
    private LandscapeManager landscapeManager;

    public GameObject player;
    public GameObject grid;
    public GameObject mainCamera;

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

    public GameObject getPlayer() {
      return player;
    }
    public GameObject getGrid() {
      return grid;
    }

    public GameObject getMainCamera() {
      return mainCamera;
    }
}

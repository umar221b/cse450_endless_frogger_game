using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform targetPosition;
    private GameObject player;
    private Seeker seeker;

    void Start()
    {
        player = GameManager.instance.getPlayer();
        targetPosition = player.transform;

        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);

    }
    public void OnPathComplete(Path p)
    {
        // Debug.Log("Get a path" + p.error);
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = player.transform;
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }
}

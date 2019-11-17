using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform targetPosition;
    void Start()
    {
        targetPosition = GameManager.instance.getPlayer().transform;
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);

    }
    public void OnPathComplete(Path p)
    {
        Debug.Log("Get a path" + p.error);
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = GameManager.instance.getPlayer().transform;
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrolScript : MonoBehaviour
{
    public GameObject[] waypoints;
    public int patrolWP = 0;
    public NavMeshAgent agent;
    public NavMeshAgent chaser;


    void Start()
    {
        
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = waypoints[patrolWP].transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        patrolWP = (patrolWP + 1) % waypoints.Length;

    }


    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if ((Vector3.Distance(transform.position, chaser.transform.position)) >= 5.0f)
            GetComponent<NavMeshAgent>().speed = 0.0f;
        else
            GetComponent<NavMeshAgent>().speed = 4.5f;

    }

}

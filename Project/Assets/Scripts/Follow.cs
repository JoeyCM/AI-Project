using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlockingManager : MonoBehaviour
{

    public GameObject beePrefab;
    [System.NonSerialized]
    public GameObject[] allbees;
    public int numBees = 0;
    public float maxDistanceHive;
    public float neighbourDistance;
    public float minSpeed;
    public float maxSpeed;
    public float rotationSpeed;
    public bool bounded;
    public bool randomize;
    public bool followLeader;


    // Start is called before the first frame update
    void Start()
    {
        allbees = new GameObject[numBees];
        for (int i = 0; i < numBees; ++i)
        {

            Vector3 pos = this.transform.position + new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f));
            Vector3 randomize = new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
            allbees[i] = (GameObject)Instantiate(beePrefab, pos,
                                Quaternion.LookRotation(randomize));
            allbees[i].GetComponent<Flock>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
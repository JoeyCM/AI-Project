using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flock : MonoBehaviour
{

    public FlockingManager myManager;
    protected float speed;
    protected Vector3 direction;
    private float freq, freqMax;

    Vector3 Cohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allbees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0)
            cohesion = (cohesion / num - transform.position).normalized * speed;

        return cohesion;
    }

    Vector3 Align()
    {
        Vector3 align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allbees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    align += go.GetComponent<Flock>().direction;
                    num++;
                }
            }
        }
        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }
        return align;
    }

    Vector3 Separation()
    {
        Vector3 separation = Vector3.zero;
        foreach (GameObject go in myManager.allbees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) /
                                  (distance * distance);
            }
        }
        return separation;
    }

    void Flocking()
    {
        direction = ((Cohesion() + Align() + Separation() + new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f))).normalized * speed);
        float currentHiveDistance = Vector3.Distance(this.transform.position, myManager.transform.position);
        if (currentHiveDistance >= myManager.maxDistanceHive)
        {
            this.transform.Rotate(transform.up * Time.deltaTime * 10f * currentHiveDistance);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        freq = 0f;
        freqMax = Random.Range(0.3f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        freq = Time.deltaTime;
        if (freq <= freqMax) Flocking();
        freqMax = Random.Range(0.3f, 0.5f);

        transform.rotation = Quaternion.Slerp(transform.rotation,
                              Quaternion.LookRotation(direction),
                              myManager.rotationSpeed * Time.deltaTime);

            //Que gire alrededor del hive


            transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
        
    }
}

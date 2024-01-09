using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    public NavMeshAgent agent;
    public GameObject HidingPlace;
    private bool hidden = false;

    public void Wander()
    {
        // Update is called once per frame
        if (isWandering == false)
        {
            StartCoroutine(Wandering());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    public IEnumerator Wandering()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 6);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }

    public void Seek(Vector3 position)
    {
        agent.destination = position;
    }

    public void Hide(Transform seeker)
    {
        if (!hidden)
        {
            agent.destination = HidingPlace.transform.position;
            if (Vector3.Distance(agent.transform.position, HidingPlace.transform.position) > 2.5f)
            {
                Debug.Log("Searching Hiding Spot");                
            }
            else
            {
                hidden = true;
            }
        }
        else
        {
            Debug.Log("Avoiding Cop");
            agent.areaMask = NavMesh.GetAreaFromName("HidingSpot");
            agent.destination = GetAwayFrom(seeker.position, HidingPlace.transform.position, 2f);
        }


    }

    private Vector3 GetAwayFrom(Vector3 target, Vector3 hidingspot, float radius)
    {
        Vector3 hidingDir = hidingspot - target;
        Vector3 hidingPoint = hidingspot + (hidingDir.normalized) * radius;
        return hidingPoint;
    }

}

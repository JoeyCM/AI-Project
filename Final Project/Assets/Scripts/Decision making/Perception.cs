using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perception : MonoBehaviour
{

    public Camera frustum;
    public LayerMask mask;
    private Transform target;
    private NavMeshAgent agent;

    private bool isWandering = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = null;
        StartCoroutine(WanderRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject.CompareTag("Player")) // Assuming the target has a "Player" tag
                    {
                        target = hit.collider.transform;
                    }
                }
            }
        }
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (target == null)
            {
                // If the target is not visible, wander around
                Vector3 randomPosition = RandomNavSphere(transform.position, 5f, -1);
                agent.SetDestination(randomPosition);
            }
            else
            {
                // If the target (player) is visible, follow it
                agent.SetDestination(target.position);
            }

            yield return new WaitForSeconds(5f); // Adjust the time between wander actions
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perception : MonoBehaviour
{
    public Camera frustum;
    public LayerMask mask;
   //public clearFlags flags;
   //public fieldOfView FOV;

    private Transform target;
    private NavMeshAgent agent;

    //private bool Wanders = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = null;
        StartCoroutine(WanderRoutine());

        // Camera settings
        frustum.clearFlags = CameraClearFlags.Depth;    // Don't Clear
        frustum.cullingMask = 0;                        // Nothing
        frustum.fieldOfView = 60f;                      // Field of View
        frustum.allowHDR = false;                       // HDR Off
        frustum.allowMSAA = false;                      // MSAA Off
        frustum.depthTextureMode = DepthTextureMode.Depth;
        frustum.layerCullSpherical = true;
    }

    void detectPlayer()
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
                    if (hit.collider.gameObject.CompareTag("Player")) 
                    {
                        target = hit.collider.transform;
                    }
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (target == null)
            {
                
                Vector3 randomPosition = RandomNavSphere(transform.position, 5f, -1);
                agent.SetDestination(randomPosition);       // If the target is not visible, wander around
            }
            else
            {
                
                agent.SetDestination(target.position);      // If the target (player) is visible, follow it
            }

            yield return new WaitForSeconds(5f);            // Adjust the time between wander actions
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
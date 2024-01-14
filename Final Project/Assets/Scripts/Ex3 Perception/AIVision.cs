using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    public Camera frustum;
	public LayerMask mask;	
    float et = 0f;

    // Update is called once per frame
    void Update()
    {
        et += Time.deltaTime;
        if (et > 0.3) {
            et -= 0.3f;
            detectVillager();
        }   
    }

    
    void detectVillager()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);
        
        foreach (Collider col in colliders)
        {
//            if (col.name == "Villager") Debug.Log("Villager");
//            Debug.Log(col.name);
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject.CompareTag("Villager"))
                    {
                        Debug.Log("Villager Detected");
                        //target = hit.collider.transform;
                    }
                }
            }
        }
    }
}

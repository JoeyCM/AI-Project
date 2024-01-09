using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialRandomPosition : MonoBehaviour
{

    private float RandX;
    private float RandZ;
    private float RandYAngle;

    // Start is called before the first frame update
    void Start()
    {
        RandX = Random.Range(-24.0f, 27.0f);
        RandZ = Random.Range(-23.0f, 28.0f);
        RandYAngle = Random.Range(0.0f, 360.0f);

        transform.position = new Vector3(RandX, 1.233784f, RandZ);
        transform.rotation = Quaternion.Euler(new Vector3(0, RandYAngle, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

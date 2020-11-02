using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spherecast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position;
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, 2, transform.forward, out hit, 10))
        {
            distanceToObstacle = hit.distance;
            Debug.Log(hit.collider.name + " - " + distanceToObstacle);
        }
    }
}

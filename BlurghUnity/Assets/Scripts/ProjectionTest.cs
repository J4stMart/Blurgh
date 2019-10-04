using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionTest : MonoBehaviour
{
    public Transform target;
    public Transform gObject;


    // Update is called once per frame
    void Update()
    {
        var gPoint = gObject.position;
        Vector3 vert = target.position;
        Vector3 distVector = gPoint - vert;
        Vector3 normal = Vector3.Normalize(new Vector3(1, 1, 0));

        if (Vector3.Angle(normal, distVector) > 90)
        {
            gObject.position = (gPoint - Vector3.Dot(gPoint, normal) * Vector3.Normalize(normal) + Vector3.Dot(vert, normal) * 1.1f * Vector3.Normalize(normal));
        }

    }
}

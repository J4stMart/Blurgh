using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WarpMesh : MonoBehaviour
{
    private Mesh mesh;
    private MeshCollider collider;
    private Vector3[] originalVertices, displacedVertices;

    public Transform gravityPoint;
    private float gravityDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < displacedVertices.Length; i++)
            displacedVertices[i] = originalVertices[i];
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position.z += 0.01f;

        for (int i = 0; i < displacedVertices.Length; i++)
        {
            var vert = transform.TransformPoint(originalVertices[i]);
            var vert2 = transform.TransformPoint(displacedVertices[i]);
            Vector3 gPoint = gravityPoint.position;
            ///gPoint.y = vert.y - (gravityDistance / 2) + (vert.y - gravityPoint.y);

            var dir = Vector3.Normalize(vert - gPoint);
            float dist = Vector3.Distance(vert, gPoint);
           // dist = Mathf.Max(0, Mathf.Min(1, (1 - dist / gravityDistance)));

            if (dist < gravityDistance)
            {
                vert = dir * gravityDistance + gravityPoint.position;

               // vert.x += dir.x * dist;
               // vert.y += -Mathf.Abs(dir.y) / 2 * dist;
               // vert.z += dir.z * dist;
            }

            displacedVertices[i] = transform.InverseTransformPoint(vert);
        }
        
      
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        collider.sharedMesh = mesh;

    

    }
}

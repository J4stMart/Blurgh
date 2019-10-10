using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WarpMeshTutorial : MonoBehaviour
{
    private Mesh mesh;
    private MeshCollider collider;
    private Vector3[] originalVertices, displacedVertices;
    private Vector3[] normals;

    [HideInInspector]
    public Transform gravityPoint;
    [HideInInspector]
    public float gravityDistance;
    [HideInInspector]
    public float deformation;
    [HideInInspector]
    public float curvature;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        normals = mesh.normals;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            var vert = transform.TransformPoint(originalVertices[i]);
            Vector3 gPoint = gravityPoint.position;

            var dist = Mathf.Pow(Vector3.Distance(vert, gPoint), 1f / curvature);

            if (dist < gravityDistance)
            {
                var distVector = gPoint - vert;
                var normal = transform.TransformDirection(normals[i]);


                Vector3 direction;
                float dist2;
                float dpc;
                if (Vector3.Angle(normal, distVector) > 90)
                {
                    var gPointCorrection = (gPoint - Vector3.Dot(gPoint, normal) * normal + Vector3.Dot(vert, normal) * normal);
                    var distVector2 = gPointCorrection - vert;
                    direction = Vector3.Normalize(normal + distVector2);
                    dist2 = Mathf.Pow(Vector3.Distance(vert, gPointCorrection), 1 / curvature);
                    dpc = Vector3.Dot(distVector2, direction);
                }

                else
                {
                    direction = Vector3.Normalize(normal + distVector);
                    dpc = Vector3.Dot(distVector, direction);
                    dist2 = dist;
                }

                var dSquared = dist2 * dist2 - dpc * dpc;
                var offset = Mathf.Sqrt(gravityDistance * gravityDistance - dSquared);

                if (dpc < 0)
                    vert += direction * (dpc + offset) * deformation;
                else
                    vert += direction * (dpc - offset) * deformation;

                displacedVertices[i] = transform.InverseTransformPoint(vert);
            }
            else
            {
                displacedVertices[i] = originalVertices[i];
            }

        }
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        collider.sharedMesh = mesh;
    }

}

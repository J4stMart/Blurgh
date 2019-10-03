﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WarpMesh2 : MonoBehaviour
{
    private Mesh mesh;
    private MeshCollider collider;
    private Vector3[] originalVertices, displacedVertices;
    private Vector3[] normals;

    public Transform gravityPoint;
    private float gravityDistance = 3f;
    private float deformation = 1f;

    [SerializeField]
    private WarpMeshHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<MeshCollider>();
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        normals = mesh.normals;
        //for (int i = 0; i < displacedVertices.Length; i++)
        //   displacedVertices[i] = originalVertices[i];
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position.z += 0.01f;

        for (int i = 0; i < displacedVertices.Length; i++)
        {
            var vert = transform.TransformPoint(originalVertices[i]);
            Vector3 gPoint = gravityPoint.position;

            var dist = Mathf.Sqrt(Vector3.Distance(vert, gPoint));
             
            if (dist < gravityDistance)
            {
                var distVector = gPoint - vert;
                var normal = transform.TransformDirection(normals[i]);

                var distVector2 = (gPoint - Vector3.Dot(gPoint, normal) * Vector3.Normalize(normal) + Vector3.Dot(vert, normal) * Vector3.Normalize(normal)) - vert;

                Vector3 direction;
                if (Vector3.Angle(normal, distVector) > 90)
                    direction = Vector3.Normalize(normal + distVector2);
                else
                    direction = Vector3.Normalize(normal + distVector);

                float dpc = Vector3.Dot(distVector2, direction);
                var dSquared = dist * dist - dpc * dpc;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GravityWarpSetter : MonoBehaviour
{


    int id, idMultiplier;
    public float multiplier = 5000, distance = 5000;

    void Start()
    {
        id = Shader.PropertyToID("_GravityPoint");
        idMultiplier = Shader.PropertyToID("_GravityMulitplier");
        Set();
    }


    void Update()
    {
        Shader.SetGlobalVector(id, transform.position);
    }

    void OnValidate()
    {
        Set();
    }

    private void OnDestroy()
    {
        Shader.SetGlobalVector(id, transform.position);
    }

    public void Set()
    {
        Shader.SetGlobalFloat(idMultiplier, multiplier);
        Shader.SetGlobalFloat("_GravityDistance", distance);
    }
}
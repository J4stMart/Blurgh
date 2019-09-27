using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTransformLocater : MonoBehaviour
{
    private GameObject gravityBullet;
    private Transform gravityTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gravityBullet)
        {
            gravityBullet = GameObject.FindWithTag("GravityPoint");
        }
        if (gravityBullet)
        {
            gravityTransform = gravityBullet.GetComponent<Transform>();
        }
        
    }
}

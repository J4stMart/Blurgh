using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpCollider : MonoBehaviour
{
    private GameObject gravityBullet;
    private Transform gravityTransform;
    private Quaternion originalRotationValue;
    private Vector3 originalPos;
    private Vector3 originalScale;
    private Transform startPos;
    private float rotationResetSpeed = 1.0f;
    private float gravityMulitplier = 5.0f;
    private float gravityDistance = 5.0f;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        originalRotationValue = transform.rotation;
        originalPos = transform.position;
        originalScale = transform.localScale;
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
            if (!gravityTransform)
            {
                gravityTransform = gravityBullet.GetComponent<Transform>();
                float gravityMulitplier = gravityBullet.GetComponent<GravityWarpSetter>().multiplier;
                float gravityDistance = 1 / gravityBullet.GetComponent<GravityWarpSetter>().distance;
            }
                      
            if (gravityTransform )
            {
                Vector3 gravityPos = gravityTransform.position;
                float dist = Vector3.Distance(gravityPos, transform.position);
                
                if (gravityBullet && (dist < gravityDistance))
                {
                    transform.LookAt(gravityTransform);
                    transform.Translate(Vector3.down * Time.deltaTime * gravityMulitplier * Mathf.Clamp(1 - (dist / gravityDistance), 0.0f, 1.0f), Space.World);

                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * rotationResetSpeed);
                    //transform.Translate((originalPos - transform.position) * Time.deltaTime * translationResetSpeed, Space.World);
                    float speed = 6.0f * Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, originalPos, speed);
                }
                                
            }

        }

        if ((!gravityBullet))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * rotationResetSpeed);
            //transform.Translate((originalPos - transform.position) * Time.deltaTime * translationResetSpeed, Space.World);
            float speed = 6.0f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, originalPos, speed);


        }


    }
     
}

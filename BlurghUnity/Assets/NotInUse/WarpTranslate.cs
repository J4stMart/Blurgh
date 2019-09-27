using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTranslate : MonoBehaviour
{
    private GameObject gravityBullet;
    private Transform gravityTransform;
    private Quaternion originalRotationValue;
    private Vector3 originalPos;
    private Vector3 originalScale;
    private float stretchAmount;
    private float stretchModifier = 1.0f;
    public float rotationResetSpeed = 100.0f;
    public float locationResetSpeed = 100.0f;
    private float dist = 6;
    private float gravityMulitplier = 5;
    private float gravityDistance = 5;

    // Start is called before the first frame update
    void Awake()
    {
        //originalRotationValue = transform.rotation;
        originalPos = transform.position;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
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
                float gravityDistance = gravityBullet.GetComponent<GravityWarpSetter>().distance;
            }
            Vector3 gravityPos = gravityTransform.position;
            if (gravityTransform)
            {
                stretchAmount = Vector3.Distance(originalPos, transform.position);
                float dist = Vector3.Distance(gravityPos, transform.position);
                if (gravityBullet && (dist < gravityDistance))
                {
                    //transform.LookAt(gravityTransform);
                    transform.Translate((originalPos - gravityPos) * gravityMulitplier * dist * Time.deltaTime);
                    //* (1 - (stretchAmount / gravityDistance)));

                }
                else
                {
                    //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * rotationResetSpeed);
                    transform.Translate((originalPos - transform.position) * Time.deltaTime * locationResetSpeed);
                }

            }
        }
        else
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * rotationResetSpeed);
            transform.Translate((originalPos - transform.position) * Time.deltaTime * locationResetSpeed);
        }

    }

}
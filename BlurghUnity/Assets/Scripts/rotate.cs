using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float RotationSpeed = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "L") 
        {
            Quaternion currentRotation = other.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(0, 0, 10);
            transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 4);
        }
        else if (other.gameObject.name == "R")
        {
            Quaternion currentRotation = other.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(0, 0, -10);
            transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 4);
        }
        else
        {
            Quaternion currentRotation = other.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 4);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float RotationSpeed = 1f;
    public GameObject darm;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "L")
        {
            Quaternion currentRotation = darm.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(0, 0, 10);
            darm.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * RotationSpeed);
        }
        else if (other.gameObject.name == "R")
        {
            Quaternion currentRotation = darm.transform.rotation;
            Quaternion wantedRotation = Quaternion.Euler(0, 0, -10);
            darm.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * RotationSpeed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBackTracker : MonoBehaviour
{
    public Transform blackHole;
    public Transform player;
    public Camera cam;
    [SerializeField] private bool swap = false;

    Vector3 wtsp;
    Vector3 wtspObject;
   
    // Update is called once per frame
    void Update()
    {
        wtsp = cam.WorldToScreenPoint(blackHole.position);
        wtspObject = cam.WorldToScreenPoint(transform.position);

        //is the black hole in front of the camera
        float BPdistance = Vector3.Distance(player.position, blackHole.position);
        float BOdistance = Vector3.Distance(transform.position, blackHole.position);
        float OPdistance = Vector3.Distance(transform.position, player.position);
        bool swap = Mathf.Pow(BPdistance, 2.0f) < Mathf.Pow(BOdistance, 2.0f) + Mathf.Pow(OPdistance, 2.0f);
        Debug.Log (swap);
        if (swap)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 8;
        }
    }
}

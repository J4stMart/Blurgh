using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTutorial : MonoBehaviour
{
    [SerializeField]
    private Transform leftHand, rightHand;

    [SerializeField]
    private WarpMeshTutorial script;

    public Transform gravityPoint;
    public float gravityDistance;
    public float curvature;
    public float maxDeformation;
    public float maxHandDistance;

    private float deformation;

    // Start is called before the first frame update
    void Start()
    {
        script.gravityPoint = gravityPoint;
        script.gravityDistance = gravityDistance;
        script.curvature = curvature;
        script.deformation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(leftHand.position, rightHand.position);
        var targetDeformation = Mathf.Clamp(dist, 0, maxHandDistance);
        script.deformation = Mathf.Lerp(script.deformation, targetDeformation, dist / maxHandDistance);

    }
}

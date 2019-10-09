using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarpTutorial : MonoBehaviour
{
    [SerializeField]
    private Transform leftHand, rightHand;

    [SerializeField]
    private WarpMeshTutorial script;

    [SerializeField]
    private Text textElement;

    public Transform gravityPoint;
    public float gravityDistance;
    public float curvature;
    public float maxDeformation;
    public float minHandDistance;
    public float maxHandDistance;

    private float deformation;
    private bool activated;
    private LineRenderer line;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        script.gravityPoint = gravityPoint;
        script.gravityDistance = gravityDistance;
        script.curvature = curvature;
        script.deformation = 0.001f;

        textElement.text = "Hold your hands close to eachother\nAnd press either of the index finger triggers\n\nTo active the gravity field";
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(leftHand.position, rightHand.position) - minHandDistance;

        if(activated)
        {
            var targetDeformation = Mathf.Clamp(dist, 0.001f, maxHandDistance);
            script.deformation = Mathf.Lerp(script.deformation, targetDeformation, dist / maxHandDistance);
            color = Color.Lerp(Color.green, Color.red, dist / maxHandDistance);

            line.SetPosition(0, leftHand.position);
            line.SetPosition(1, rightHand.position);
            line.startColor = color;
            line.endColor = color;
        }
        else
        {
            if(dist <= minHandDistance && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                activated = true;
                textElement.text = "Move your hands away from eachother\nAnd experience a gravity well";
                line.enabled = true;
            }
        }
        
        if(!OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            activated = false;
            textElement.text = "Hold your hands close to eachother\nAnd press either of the index finger triggers\n\nTo active the gravity field";
            line.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadArtScene());
        }
    }

    IEnumerator LoadArtScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("art test");

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

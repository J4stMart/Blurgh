using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnableOnVr : MonoBehaviour
{
    [SerializeField]
    private bool enabledWhenVrConnected;

    // Start is called before the first frame update
    void Start()
    {
        if (enabledWhenVrConnected)
            gameObject.SetActive(XRDevice.isPresent);
        else
            gameObject.SetActive(!XRDevice.isPresent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

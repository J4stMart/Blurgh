using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip bounce;

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = bounce;
    }

    void OnCollisionEnter() 
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("bounce");
    }
}
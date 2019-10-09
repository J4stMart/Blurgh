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
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(bounce);
        }

    }
}
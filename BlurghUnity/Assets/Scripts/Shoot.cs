using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject theBullet;
    public Transform barrelEnd;
    public ParticleSystem muzzleFlash;
    public GameObject theGun;
    public Transform MuzzleFlashLocation;
    private Animator mAnimator = null;

    public int bulletSpeed;
    public float despawnTime = 3.0f;

    [SerializeField]
    private OVRInput.Controller m_controller;

    public bool shootAble = true;
    public float waitBeforeNextShot = 0.25f;

    private void Awake()
    {
        mAnimator = theGun.GetComponent<Animator>();
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger, m_controller) || Input.GetKey(KeyCode.Mouse0))   
        {
            if (shootAble)
            {
                shootAble = false;
                Shooting();
                StartCoroutine(ShootingYield());
            }
        }
        else
        {
            mAnimator.SetBool("Shoot", false);
        }
    }

    IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(waitBeforeNextShot);
        shootAble = true;
    }

    private void Shooting()
    {
        var bullet = Instantiate(theBullet, barrelEnd.position, barrelEnd.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Instantiate(muzzleFlash, MuzzleFlashLocation.position, barrelEnd.rotation);
        mAnimator.SetBool("Shoot", true);
        Destroy(bullet, despawnTime);
    }
}
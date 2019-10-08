using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject theBullet;
    private Transform gravityPoint;
    public Transform barrelEnd;
    public GameObject muzzleFlash;
    public GameObject theGun;
    public Transform MuzzleFlashLocation;
    private Animator mAnimator = null;
    public float gravityDistance = 6f;
    public float deformation = 1f;
    public float timer;
    public float curvature = 2f;
    public float unWarpTime = 0.5f;
    public float despawnTime = 12f;
    public float explosionradius = 10.0f;
    public float explosionpower = 800.0f;

    public int bulletSpeed;


    [SerializeField]
    private OVRInput.Controller m_controller;
    [SerializeField]
    private OVRPlayerController player;

    public bool shootAble = true;

    private void Start()
    {
        mAnimator = theGun.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!gravityPoint)
        {
            gravityPoint = GameObject.FindWithTag("PointStorage").GetComponent<Transform>();
            gravityPoint = theBullet.transform;
            Debug.Log("There is no gravityBullet");
        }
        if (gravityPoint == GameObject.FindWithTag("PointStorage").GetComponent<Transform>())
        {
            if (GameObject.FindWithTag("GravityPoint"))
            {
                timer = 0.0f;
                gravityPoint = GameObject.FindWithTag("GravityPoint").GetComponent<Transform>();
            }
        }

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

        if (OVRInput.Get(OVRInput.Button.One))
        {
            player.Jump();
        }
    }

    IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(despawnTime);
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
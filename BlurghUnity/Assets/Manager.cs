using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject theBullet;
    public GameObject ExplosionSoundSource;
    private Transform gravityPoint;
    public Transform barrelEnd;
    public GameObject muzzleFlash;
    public GameObject theGun;
    public Transform MuzzleFlashLocation;
    private Animator mAnimator = null;
    private bool hasExploded = true;
    private GameObject explosion;
    public float gravityDistance = 6f;
    public float deformation = 1f;
    public float timer;
    public float curvature = 2f;
    public float unWarpTime = 0.5f;
    public float despawnTime = 12f;
    public float explosionradius = 40.0f;
    public float explosionpower = 800.0f;
    private float defaultdeformation;

    public int bulletSpeed;


    [SerializeField]
    private OVRInput.Controller m_controller;
    [SerializeField]
    private OVRPlayerController player;

    public bool shootAble = true;

    private void Start()
    {
        mAnimator = theGun.GetComponent<Animator>();
        defaultdeformation = deformation;
    }

    private void Update()
    {
        if (!gravityPoint)
        {
            gravityPoint = GameObject.FindWithTag("PointStorage").GetComponent<Transform>();
            gravityPoint = theBullet.transform;
        }

        if (!shootAble)
        {
            timer += Time.deltaTime;

            if (timer > despawnTime - unWarpTime)
            {
                deformation = Mathf.Lerp(0f, deformation, (despawnTime - timer) / unWarpTime);
            }
            if (timer > despawnTime - 0.6f && !hasExploded)
            {
                explosion.SetActive(true);
                ReversedExplosion();
            }
            if (timer > despawnTime - 0.1f && !hasExploded)
            {
                hasExploded = true;
                Instantiate(ExplosionSoundSource, gravityPoint.position, gravityPoint.rotation);
                Explosion();
            }

        }
        Debug.Log(timer);

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger, m_controller) || Input.GetKey(KeyCode.Mouse0))
        {
            if (shootAble)
            {
                shootAble = false;
                hasExploded = false;
                Shooting();
                StartCoroutine(ShootingYield());
                gravityPoint = GameObject.FindWithTag("GravityPoint").GetComponent<Transform>();
                explosion = GameObject.FindWithTag("Explosion");
                explosion.SetActive(false);
                timer = 0.0f;
                deformation = defaultdeformation;
            }
        }
        else
        {
            mAnimator.SetBool("Shoot", false);
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            //player.Jump();
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

    void Explosion()
    {
        Vector3 explosionPos = gravityPoint.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionradius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionpower, explosionPos, explosionradius, 1.0F);
        }
    }

    void ReversedExplosion()
    {
        Vector3 explosionPos = gravityPoint.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionradius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(-0.1f * explosionpower, explosionPos, explosionradius, 0.0F);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float impactForce = 50f;

    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public bool isPaused;

    private void Start()
    {
        isPaused = false;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            bot_beta botSc = hit.transform.GetComponent<bot_beta>();
            if(botSc != null)
            {
                botSc.TakeDamage(damage);
            }

            GameObject room = GameObject.FindGameObjectWithTag("Room");

            if(hit.collider.gameObject.tag == "Room")
            {
                GameObject impactEffectGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactEffectGO, 2f);
            }

        }
    }

}

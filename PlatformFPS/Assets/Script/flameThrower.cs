using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameThrower : MonoBehaviour
{
    public int damage = 30;
    public float range = 6f;
    public float timeBtwShots;

    public float impactForce = 50f;

    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    void Start()
    {
    }

    private void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                timeBtwShots = 3f;
            }
        }
        else
            timeBtwShots -= Time.deltaTime;
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

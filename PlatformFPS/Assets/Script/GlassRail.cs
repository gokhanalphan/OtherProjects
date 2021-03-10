using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRail : MonoBehaviour
{
    public float speed = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "stopPoints")
            speed *= -1;
    }

    void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }
}

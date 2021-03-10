using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_beta2 : MonoBehaviour
{
    private CharacterController controller;
    private GameObject teleportPoint;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        teleportPoint = GameObject.FindGameObjectWithTag("TeleportPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        controller.enabled = false;
        controller.transform.position = teleportPoint.transform.position;
        controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

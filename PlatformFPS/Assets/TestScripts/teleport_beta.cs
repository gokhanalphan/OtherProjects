using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_beta : MonoBehaviour
{
    public Transform teleportPoint;
    private CharacterController player;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<CharacterController>();
        player.enabled = false;
        player.transform.position = teleportPoint.transform.position;
        player.enabled = true;
    }
}

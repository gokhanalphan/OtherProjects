using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private float timeBtwSpawn = 0f;
    public GameObject character;
    public Transform spawnPoint;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (timeBtwSpawn <= 0)
            {
                reSpawnPlayer();
            }
            else
                timeBtwSpawn -= Time.deltaTime;
        }
    }

    void reSpawnPlayer()
    {
        Instantiate(character, spawnPoint.transform.position, spawnPoint.transform.rotation);
        timeBtwSpawn = 5f;
    }
}

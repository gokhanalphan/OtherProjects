using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner1 : MonoBehaviour
{
    private int savedPlayer;

    public GameObject britishSpawner;
    public GameObject germanASpawner;
    public GameObject germanBSpawner;

    private void Awake()
    {
        savedPlayer = PlayerPrefs.GetInt("SelectedPlayer");

        if (savedPlayer == 0)
            britishSpawner.SetActive(true);

        if (savedPlayer == 1)
            germanASpawner.SetActive(true);

        if (savedPlayer == 2)
            germanBSpawner.SetActive(true);
    }
}

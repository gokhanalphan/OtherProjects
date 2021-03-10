using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseCont : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!pauseMenu.activeInHierarchy)
            {
                PauseGame();
            }

            else if (pauseMenu.activeInHierarchy)
            {
                ResumeGame();
            }
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<gun>())
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<gun>().enabled = false;

        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<flameThrower>())
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<flameThrower>().enabled = false;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<gun>())
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<gun>().enabled = true;

        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<flameThrower>())
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<flameThrower>().enabled = true;
    }
}

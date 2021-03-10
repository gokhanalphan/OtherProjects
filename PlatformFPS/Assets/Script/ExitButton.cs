using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("demoMainMenu");
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitClick : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}

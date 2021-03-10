using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    private int SelectedPlayer;

    public void BritishCharSelected()
    {
        SelectedPlayer = 0;
        PlayerPrefs.SetInt("SelectedPlayer", SelectedPlayer);
        SceneManager.LoadScene("Action");
    }

    public void GermanACharSelected()
    {
        SelectedPlayer = 1;
        PlayerPrefs.SetInt("SelectedPlayer", SelectedPlayer);
        SceneManager.LoadScene("Action");
    }

    public void GermanBCharSelected()
    {
        SelectedPlayer = 2;
        PlayerPrefs.SetInt("SelectedPlayer", SelectedPlayer);
        SceneManager.LoadScene("Action");
    }
}

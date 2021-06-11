using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        PlayerPrefs.SetInt("nDeaths", 0);
        PlayerPrefs.SetFloat("timer", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("salir");
        Application.Quit();
    }

    public void Leaderboard()
    {
        // TO DO
        Debug.Log("Puta ISRAEL");
    }
}

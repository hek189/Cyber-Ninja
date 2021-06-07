using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, INextLevel
{
    public void Play()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("salir");
        Application.Quit();
    }

    public void LoadNextLevel(int buildIndex){
        SceneManager.LoadScene(buildIndex + 1);
    }
}

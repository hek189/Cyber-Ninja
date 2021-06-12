using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class EndscreenManager : MonoBehaviour
{
    public TextMeshProUGUI time, deaths;
    private PlayerData playerData;
    void Start()
    {
        time.text = PlayerPrefs.GetFloat("timer").ToString("0") + " sec";
        deaths.text = PlayerPrefs.GetInt("nDeaths").ToString();
        playerData = new PlayerData();
    }

    void Update()
    {

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UploadToMongo()
    {
        //TO DO
    }
}

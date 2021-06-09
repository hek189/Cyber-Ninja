using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndscreenManager : MonoBehaviour
{
    public TextMeshProUGUI time, deaths;
    private LeaderboardManager leaderboard;
    void Start()
    {
        time.text = PlayerPrefs.GetFloat("timer").ToString("0")+" sec";
        deaths.text = PlayerPrefs.GetFloat("nDeaths").ToString();
    }

    // Update is called once per frame
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

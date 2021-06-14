using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{

    private bool isVisible;
    private DatabaseManager databaseManager;
    public TextMeshProUGUI leaderboardText;

    private void Start() {
        databaseManager = GetComponent<DatabaseManager>();
    }
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

    public async void Leaderboard()
    {
        leaderboardText.text = "";
        var task = databaseManager.GetScores();
        var result = await task;
        int num = 1;
        foreach(var player in result)
        {
            leaderboardText.text += num+"). "+player.name+" / "+ player.time.ToString("0") + " / "+player.nDeaths+"\n";
            num +=1;
        }
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndscreenManager : MonoBehaviour
{
    public TextMeshProUGUI time, deaths, playerName;
    private PlayerData playerData;
    private bool canUpload = true;

    private DatabaseManager databaseManager;
    void Start()
    {
        time.text = PlayerPrefs.GetFloat("timer").ToString("0") + " sec";
        deaths.text = PlayerPrefs.GetInt("nDeaths").ToString();
        databaseManager = GetComponent<DatabaseManager>();
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
        if (canUpload)
        {
            databaseManager.UploadToDB(playerName.text.ToString(), PlayerPrefs.GetFloat("timer"), PlayerPrefs.GetInt("nDeaths"));
            canUpload = false;
            SceneManager.LoadScene(0);
        }
    }
}



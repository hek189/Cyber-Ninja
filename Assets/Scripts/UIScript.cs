using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI time, deaths;
    private float timer;
    private int nDeaths;
    void Start()
    {
        deaths.text = nDeaths.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        time.text = timer.ToString("0");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("timer", timer);
    }

    private void OnEnable()
    {
        timer = PlayerPrefs.GetFloat("timer");
        nDeaths = PlayerPrefs.GetInt("nDeaths");
    }
}

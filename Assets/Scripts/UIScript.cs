using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI time, deaths;
    private float timer;
    private int nDeaths;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        time.text = timer.ToString("0");
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("nDeaths", nDeaths);
        PlayerPrefs.SetFloat("timer", timer);
    }

    private void OnEnable()
    {
        timer = PlayerPrefs.GetFloat("timer");
        nDeaths = PlayerPrefs.GetInt("nDeaths");
    }
}

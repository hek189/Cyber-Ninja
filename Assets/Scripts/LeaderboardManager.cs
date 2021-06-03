using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private int nDeaths = 0;
    private float time = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDeath()
    {
        nDeaths++;
    }

    public int GetDeaths()
    {
        return nDeaths;
    }

    public void addTime(float newTime)
    {
        time = time + newTime;
    }
}

using UnityEngine;

public class PlayerData
{
    public string name;
    public float time;
    public int nDeaths;

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }

    public static PlayerData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class DatabaseManager : MonoBehaviour
{
    // Start is called before the first frame update
    MongoClient client = new MongoClient("mongodb+srv://Admin:DCJzrspEzlF9JgM8@leaderboard.7w1z4.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    void Start()
    {
        database = client.GetDatabase("LeaderboardDB");
        collection = database.GetCollection<BsonDocument>("LeaderboardCollection");
    }

    public async void UploadToDB(string name, float time, int deaths)
    {
        var document = new BsonDocument { { "name", name }, { "time", time }, { "nDeaths", deaths } };
        await collection.InsertOneAsync(document);
    }

    public async Task<List<PlayerData>> GetScores()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<PlayerData> leaderboard = new List<PlayerData>();
        foreach (var data in scoresAwaited.ToList())
        {
            leaderboard.Add(Deserialize(data.ToString()));
        }
        leaderboard.Sort(Comparetimes);
        if (leaderboard.Count > 10)
        {
            return leaderboard.GetRange(0, 10);
        }
        else
        {
            return leaderboard;
        }
    }

    private PlayerData Deserialize(string rawJson)
    {
        //Ejemplo { "_id" : ObjectId("60c64bef75524c2b149b1e2b"), "name" : "test", "time" : 1564, "nDeaths" : 69 }
        var playerData = new PlayerData();
        var noID = rawJson.Substring(rawJson.IndexOf("),") + 4);
        var name = noID.Substring(noID.IndexOf(":") + 3, noID.IndexOf(",") - 16);
        noID = noID.Substring(noID.IndexOf(":") + 2, noID.IndexOf("}") - noID.IndexOf(":") - 3);
        noID = noID.Substring(noID.IndexOf(":"));
        var time = noID.Substring(noID.IndexOf(":") + 2, noID.IndexOf(",") - 2);
        noID = noID.Substring(noID.IndexOf(":") + 1);
        var nDeaths = noID.Substring(noID.IndexOf(":") + 2);
        playerData.name = name;
        playerData.time = float.Parse(time, System.Globalization.CultureInfo.InvariantCulture);
        playerData.nDeaths = int.Parse(nDeaths);
        return playerData;
    }

    private static int Comparetimes(PlayerData player1, PlayerData player2)
    {
        return player1.time.CompareTo(player2.time);
    }
}
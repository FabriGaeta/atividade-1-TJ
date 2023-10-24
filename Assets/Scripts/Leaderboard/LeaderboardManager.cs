using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Singleton { get; private set; }

    
    private const string LeaderboardKey = "leaderboard";
    public const int MaxScores = 999;
    
    public LeaderboardData LeaderboardData { get; private set; } = new LeaderboardData();
    
    
    void Awake()
    {
        Singleton = this;
        string leaderboardJson = PlayerPrefs.GetString(LeaderboardKey);
        if (leaderboardJson == null || leaderboardJson.Length == 0)
        {
            return;
        }
        var result  = JsonUtility.FromJson <LeaderboardData>(leaderboardJson);
        LeaderboardData = result;
        // DontDestroyOnLoad(gameObject);
    }

    public int FindScorePosition(int score){
        var leaderboardScores = LeaderboardData.leaderboardScores;
        for (var i = 0; i <= leaderboardScores.Count; i++)
        {
            if (i == leaderboardScores.Count)
            {
                return leaderboardScores.Count + 1;
            }
            
            var cScore = leaderboardScores[i];
            if (score > cScore.score)
            {
                return i + 1;
            }
        }

        return MaxScores;

    }

    public void AddScore(string name, int score)
    {
        LeaderboardScore nScore = new LeaderboardScore(name, score);

        var leaderboardScores = LeaderboardData.leaderboardScores;
        for (var i = 0; i <= leaderboardScores.Count; i++)
        {
            if (i == leaderboardScores.Count)
            {
                leaderboardScores.Add(nScore);
                break;
            }
            
            var cScore = leaderboardScores[i];
            if (nScore.score > cScore.score)
            {
                leaderboardScores.Insert(i, nScore);
                break;
            }
        }

        if (leaderboardScores.Count > MaxScores)
        {
            leaderboardScores = leaderboardScores.GetRange(0, MaxScores);
        }
        
        PlayerPrefs.SetString(LeaderboardKey, JsonUtility.ToJson(new LeaderboardData(leaderboardScores)));
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3)) // cheat to clear leaderboard
        {
            PlayerPrefs.DeleteKey(LeaderboardKey);
            PlayerPrefs.Save();
        }
    }
}

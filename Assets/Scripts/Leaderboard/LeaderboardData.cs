using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeaderboardData
{
    public LeaderboardData()
    {
    }

    public LeaderboardData(List<LeaderboardScore> leaderboardScores)
    {
        this.leaderboardScores = leaderboardScores;
    }

    public List<LeaderboardScore> leaderboardScores = new List<LeaderboardScore>();

}

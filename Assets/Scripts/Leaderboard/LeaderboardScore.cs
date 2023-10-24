using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeaderboardScore
{
    public LeaderboardScore()
    {
    }

    public LeaderboardScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public String name;

    public int score;

}

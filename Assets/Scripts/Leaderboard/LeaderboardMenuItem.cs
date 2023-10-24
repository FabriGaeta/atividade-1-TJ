using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardMenuItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _playerNameText;
    
    [SerializeField] private TextMeshProUGUI _scoreText;



    public void Setup(LeaderboardScore leaderboardScore)
    {
        _playerNameText.text = leaderboardScore.name;
        _scoreText.text = leaderboardScore.score.ToString();
    }
}

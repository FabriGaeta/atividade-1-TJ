using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : MonoBehaviour
{

    [SerializeField] private Transform _leaderboardItemsParent;

    [SerializeField] private GameObject _leaderboardItemPrefab;

    private void Start()
    {
        if (LeaderboardManager.Singleton != null)
        {
            var leaderboardScores = LeaderboardManager.Singleton.LeaderboardData.leaderboardScores;
            var ranking = 1;
            foreach (var leaderboardScore in leaderboardScores)
            {
                var leaderboardItemGameObject = Instantiate(_leaderboardItemPrefab, _leaderboardItemsParent);
                var leaderboardItem = leaderboardItemGameObject.GetComponent<LeaderboardMenuItem>();
                leaderboardItem.Setup(leaderboardScore, ranking);
                ranking++;
            }
        }
    }
}

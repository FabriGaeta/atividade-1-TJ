using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LeaderboardMenuItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _playerNameText;
    
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private TextMeshProUGUI _ranking;

    [SerializeField] private GameObject _background;


    public void Setup(LeaderboardScore leaderboardScore, int ranking)
    {
        _playerNameText.text = leaderboardScore.name;
        _scoreText.text = leaderboardScore.score.ToString();
        if(ranking > 3){
            Destroy(_background);
            _ranking.text = ranking.ToString();
        } else {
            Destroy(_ranking);
            Sprite[] todasSprites = Resources.LoadAll<Sprite>("medals_leaderboard");
            Sprite s1;
            if(ranking == 1){
                s1 = todasSprites[1];
            } else if (ranking == 2){
                s1 = todasSprites[0];
            } else {
                s1 = todasSprites[2];
            }
            _background.GetComponent<Image>().sprite = s1;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public static GameManager Singleton { get; private set; }
    
    [SerializeField] private int _lifes = 5;

    [SerializeField] private Transform _lifeParent;

    [SerializeField] private GameObject _lifePrefab;

    [SerializeField] private Image _artImage;
    
    [SerializeField] private List<GameOptionButton> _buttons;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private GameConfig _gameConfig;

    private int _score;

    private int _currentLife;

    private HashSet<string> _optionsSet = new();
    
    private List<ArtPiece> _pendingArts = new(); // list of all the arts that still can be choosen as the next challenge

    private List<LifeObject> _lifeObjects = new();

    private ArtPiece _currentArt;
    
    // Start is called before the first frame update
    void Awake()
    {
        Singleton = this;
        _pendingArts.AddRange(_gameConfig.ArtPieces);
        _scoreText.text = "Pontuação: " + _score.ToString();
        foreach (var aPiece in _gameConfig.ArtPieces)
        {
            _optionsSet.Add(aPiece.Movement);
        }
        SetupLifes();
        SetupNextChallenge();
    }

    private void SetupLifes()
    {
        _currentLife = _lifes;
        for (int i = 0 ; i < _lifes; i++)
        {
            var life = Instantiate(_lifePrefab);
            life.transform.SetParent(_lifeParent);
            life.transform.SetSiblingIndex(i + 1);
            _lifeObjects.Add(life.GetComponent<LifeObject>());
        }
    }
    

    private void SetupNextChallenge()
    {
        if (_pendingArts.Count > 0)
        {
            var nextArtIndex = Random.Range(0, _pendingArts.Count);
            var nextArt = _pendingArts[nextArtIndex];
            _pendingArts.RemoveAt(nextArtIndex);
            _artImage.sprite = nextArt.ArtSprite;
            _currentArt = nextArt;
            var pendingOptions = new List<string>();
            pendingOptions.AddRange(_optionsSet);
            pendingOptions.Remove(_currentArt.Movement);
            int rightAnswerIndex = Random.Range(0, _buttons.Count);
            for (var i = 0; i < _buttons.Count; i++)
            {
                var bOption = _buttons[i];
                if (i == rightAnswerIndex)
                {
                    bOption.SetupButton(_currentArt.Movement);
                }
                else
                {
                    if (pendingOptions.Count > 0)
                    {
                        int rOptionIndex = Random.Range(0, pendingOptions.Count);
                        var option = pendingOptions[rOptionIndex];
                        pendingOptions.RemoveAt(rOptionIndex);
                        bOption.SetupButton(option);
                    }
                    else
                    {
                        bOption.SetupButton("???");
                    }
                }
            }
        }
        else
        {
            EndGame();
        }
    }

    public bool OptionSelected(string option)
    {
        bool result = option == _currentArt.Movement;
        if (result)
        {
            SetupNextChallenge();
            _score++;
            _scoreText.text ="Pontuação: " + _score.ToString();
        }
        else
        {
            RemoveLife();
        }
        return result;
    }

    private void RemoveLife()
    {
        _currentLife--;
        _lifeObjects[_currentLife].SetLife(false);
        if (_currentLife == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        //TODO
        if (LeaderboardManager.Singleton != null)
        {
            LeaderboardManager.Singleton.AddScore("TestScore", _score);
        }
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

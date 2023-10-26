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

    [SerializeField] private AudioClip _wrongAnswerAudio;
    
    [SerializeField] private AudioClip _rightAnswerAudio;
    
    [SerializeField] private AudioClip _gameOverAudio;

    // Game Over Screen
    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverScreen;

    [SerializeField] private TextMeshProUGUI _gameOverScoreText;

    [SerializeField] private TextMeshProUGUI _gameOverPositionText;

    [SerializeField] private TMP_InputField _gameOverInputField;
    
    // Art Description Screen
    [Header("ArtDescription")]
    [SerializeField] private GameObject _artDescriptionScreen;
    
    [SerializeField] private TextMeshProUGUI _artDescriptionNameText;
    
    [SerializeField] private TextMeshProUGUI _artDescriptionArtistText;

    [SerializeField] private TextMeshProUGUI _artDescriptionDescriptionText;



    private AudioSource _audioSource;
    
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
        _audioSource = GetComponent<AudioSource>();
        _pendingArts.AddRange(_gameConfig.ArtPieces);
        _scoreText.text = "Pontuação: " + _score.ToString();
        foreach (var aPiece in _gameConfig.ArtPieces)
        {
            _optionsSet.Add(aPiece.Movement);
        }
        SetupLifes();
    }

    private void Start()
    {
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
            _audioSource.PlayOneShot(_rightAnswerAudio);
            _score++;
            _scoreText.text ="Pontuação: " + _score.ToString();
            ShowArtDescription();
        }
        else
        {
            _audioSource.PlayOneShot(_wrongAnswerAudio);
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

    private void ShowArtDescription()
    {
        _artDescriptionScreen.SetActive(true);
        _artDescriptionNameText.text = String.Format("{0} ({1})", _currentArt.Name, _currentArt.Date);
        _artDescriptionArtistText.text = _currentArt.Artist;
        _artDescriptionDescriptionText.text = _currentArt.Description;
        
        //this is needed to refresh the layout dimensions...
        _artDescriptionNameText.transform.parent.gameObject.SetActive(false);
        _artDescriptionArtistText.transform.parent.gameObject.SetActive(false);
        _artDescriptionDescriptionText.transform.parent.gameObject.SetActive(false);
        Invoke(nameof(RefreshLayout), 0);
        Canvas.ForceUpdateCanvases();
    }

    private void RefreshLayout()
    {
        _artDescriptionNameText.transform.parent.gameObject.SetActive(true);
        _artDescriptionArtistText.transform.parent.gameObject.SetActive(true);
        _artDescriptionDescriptionText.transform.parent.gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
    }

    private void EndGame()
    {
        _audioSource.PlayOneShot(_gameOverAudio);
        _gameOverScreen.SetActive(true);
        _gameOverScoreText.text = _score.ToString();
         if (LeaderboardManager.Singleton != null)
        {
            _gameOverPositionText.text = LeaderboardManager.Singleton.FindScorePosition(_score).ToString() +"°";
        }
    }

    public void SendScore(){
        if (LeaderboardManager.Singleton != null)
        {
            var leaderboardName = _gameOverInputField.text;
            if(leaderboardName == null || leaderboardName.Length == 0){
                leaderboardName = "JogadorSemNome";
            }
            LeaderboardManager.Singleton.AddScore(leaderboardName, _score);
        }
        
        
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        
        if (_artDescriptionScreen.activeSelf && Input.GetMouseButtonDown(0))
        {
            _artDescriptionScreen.SetActive(false);
            SetupNextChallenge();
        }
    }
}

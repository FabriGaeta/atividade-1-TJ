using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionButton : MonoBehaviour
{

    private Button _button;
    private TextMeshProUGUI _text;

    private string _option;
    
    void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        var result = GameManager.Singleton.OptionSelected(_option);

        if (!result)
        {
            _button.interactable = false;
        }
    }
    
    public void SetupButton(string option)
    {
        _option = option;
        _button.interactable = true;
        _text.text = _option;
    }

}

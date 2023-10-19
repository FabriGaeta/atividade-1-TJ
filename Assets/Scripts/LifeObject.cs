using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeObject : MonoBehaviour
{

    [SerializeField] private Image _fgImage;

    public void SetLife(bool life)
    {
        _fgImage.enabled = life;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ArtPiece
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _artSprite;
    [SerializeField] private string _movement;
    [SerializeField] private string _date;
    [SerializeField] private string _artist;
    [SerializeField] private string _description;

    public Sprite ArtSprite => _artSprite;
    public string Name => _name;
    public string Movement => _movement;
    public string Date => _date;
    public string Artist => _artist;
    public string Description => _description;
}

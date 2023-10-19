using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{

    [SerializeField] private List<ArtPiece> _artPieces = new List<ArtPiece>();
    public List<ArtPiece> ArtPieces => _artPieces;
    
}

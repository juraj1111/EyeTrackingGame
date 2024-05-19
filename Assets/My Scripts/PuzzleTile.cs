using UnityEngine;

public class PuzzleTile : GazeInteractable
{
    [SerializeField] private bool correctTile;

    public bool CorrectTile { get => correctTile;}
}

using System.Collections;
using UnityEngine;

public class Puzzle : GazeInteractable
{
    public PuzzleTile[,] tiles;
    public bool[,] selectedMatrix;
    private bool isAnyTileGazed = false;
    private bool solved = false;
    private bool allTilesCorrectValue = false;
    [SerializeField] public float maxDistance = 3.0f;

    void Awake()
    {
        // Nájdenie všetkých dlaždíc
        tiles = new PuzzleTile[3, 3];

        tiles[0, 0] = GameObject.Find("Tile00").GetComponent<PuzzleTile>();
        tiles[0, 1] = GameObject.Find("Tile01").GetComponent<PuzzleTile>();
        tiles[0, 2] = GameObject.Find("Tile02").GetComponent<PuzzleTile>();
        tiles[1, 0] = GameObject.Find("Tile10").GetComponent<PuzzleTile>();
        tiles[1, 1] = GameObject.Find("Tile11").GetComponent<PuzzleTile>();
        tiles[1, 2] = GameObject.Find("Tile12").GetComponent<PuzzleTile>();
        tiles[2, 0] = GameObject.Find("Tile20").GetComponent<PuzzleTile>();
        tiles[2, 1] = GameObject.Find("Tile21").GetComponent<PuzzleTile>();
        tiles[2, 2] = GameObject.Find("Tile22").GetComponent<PuzzleTile>();


        // 2D pole zaznamenáva ktoré dlaždice boli pozreté
        selectedMatrix = new bool[3, 3];
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                selectedMatrix[i, j] = false;
            }
        }
    }

    void Update()
    {
        if (solved) return;
        allTilesCorrectValue = true;
        isAnyTileGazed = false;
        // Kontorluje ktoré dlaždíce su pozreté, èi su pozreté správne dlaždice a èi je nejaká dlaždica pozeraná
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for(int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].getIsGazed() && tiles[i, j].getDistance() < maxDistance)
                {
                    tiles[i, j].GetComponent<Outline>().enabled = true;
                    selectedMatrix[i, j] = true;
                    isAnyTileGazed = true;
                }
                if (selectedMatrix[i, j] != tiles[i, j].CorrectTile) allTilesCorrectValue = false;
            }
        }
        // Ak sú všetky správne dlaždice oznaèené tak sa oznaèia na zeleno a spustí sa animácia 
        if (allTilesCorrectValue == true)
        {
            solved = true;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].CorrectTile == true)
                    {  
                        tiles[i, j].GetComponent<Outline>().OutlineColor = Color.green;
                    }
                    else
                    {
                        tiles[i, j].GetComponent<Outline>().enabled = false;
                    }
                }
            }
            StartCoroutine(OpenPuzzle());
        }
        // Ak sa stratí poh¾ad tak sa zresetuje pole
        if(isAnyTileGazed == false && isGazed == false) 
        {
            ResetTiles();
        }
    }

    private void ResetTiles()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                tiles[i, j].GetComponent<Outline>().enabled = false;
                selectedMatrix[i, j] = false;
            }
        }
    }

    IEnumerator OpenPuzzle()
    {
        yield return new WaitForSeconds(1f);
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("open");
    }
}
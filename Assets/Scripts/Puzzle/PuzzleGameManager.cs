using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{

    public RawImage puzzlePiecePrefab;
    public Texture image;
    public Transform puzzleBoard;

    private int xSize, ySize;
    private int numberOfPieces;

    private float xMaxVisualSize;
    private float yMaxVisualSize;
    private float xMinVisualSize;
    private float yMinVisualSize;

    private List<RawImage> puzzlePieces;

    // Use this for initialization
    void Start()
    {
        InitSettings();

        InitPieces();

    }

    private void InitSettings()
    {
        xSize = 3;
        ySize = 3;
        numberOfPieces = xSize * ySize;

        puzzleBoard.GetComponent<GridLayoutGroup>().constraintCount = xSize;

        xMinVisualSize = 300f;
        xMaxVisualSize = 850f;
        yMinVisualSize = 300f;
        yMaxVisualSize = 500f;
    }

    private void InitPieces()
    {
        puzzlePieces = new List<RawImage>(numberOfPieces);
        var pieceUvWidth = 1f / xSize;
        var pieceUvHeight = 1f / ySize;
        for (var y = 0; y < ySize; y++)
        {
            for (var x = 0; x < xSize; x++)
            {
                var piece = Instantiate<RawImage>(puzzlePiecePrefab, puzzleBoard);
                piece.texture = image;
                piece.uvRect = new Rect(x * pieceUvWidth, y * pieceUvHeight, pieceUvWidth, pieceUvHeight);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{

    public Button puzzlePiecePrefab;
    public Texture image;
    public Transform puzzleBoard;

    private int xSize, ySize;
    private int numberOfPieces;

    private List<RawImage> puzzlePieces;
    private Button selectedButton = null;

    // Use this for initialization
    void Start()
    {
        InitSettings();

        InitPieces();
        MixPiecesOnBoard();

    }

    private void InitSettings()
    {
        xSize = 3;
        ySize = 3;
        numberOfPieces = xSize * ySize;

        puzzleBoard.GetComponent<GridLayoutGroup>().constraintCount = xSize;
    }

    private void InitPieces()
    {
        puzzlePieces = new List<RawImage>(numberOfPieces);
        var pieceUvWidth = 1f / xSize;
        var pieceUvHeight = 1f / ySize;
        for (int y = 0, i = 0; y < ySize; y++)
        {
            for (var x = 0; x < xSize; x++, i++)
            {
                var piece = Instantiate<Button>(puzzlePiecePrefab, puzzleBoard);
                piece.onClick.AddListener(() => ClickOnPiece(piece));
                var pieceImage = piece.GetComponentInChildren<RawImage>();
                pieceImage.texture = image;
                pieceImage.uvRect = new Rect(x * pieceUvWidth, y * pieceUvHeight, pieceUvWidth, pieceUvHeight);
                piece.targetGraphic = pieceImage;
            }
        }
    }

    private void ClickOnPiece(Button piece)
    {
        if (OtherPieceAlreadySelected())
        {
            if (selectedButton != piece)
            {
                SwapSelectedWithTheNewPiece(piece);
            }
            RemoveSelection();
        }
        else
        {
            Debug.Log("Selection");
            SetSelection(piece);
        }
    }

    private bool OtherPieceAlreadySelected()
    {
        return selectedButton != null;
    }

    private void SwapSelectedWithTheNewPiece(Button secondPiece)
    {
        var i1 = selectedButton.transform.GetSiblingIndex();
        var i2 = secondPiece.transform.GetSiblingIndex();

        var shift = i1 > i2 ? 1 : -1;

        puzzleBoard.GetChild(i1).SetSiblingIndex(i2);
        puzzleBoard.GetChild(i2+shift).SetSiblingIndex(i1);
    }

    private void RemoveSelection()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        selectedButton = null;
    }

    private void SetSelection(Button piece)
    {
        selectedButton = piece;
    }

    private void MixPiecesOnBoard()
    {
        //puzzleBoard.GetChild(0).SetSiblingIndex(3);
    }
}

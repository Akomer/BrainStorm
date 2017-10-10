using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{

    public Button puzzlePiecePrefab;
    public Texture image;
    public Transform puzzleBoard;
    public RectTransform victoryPanel;

    private int xSize, ySize;
    private int numberOfPieces;

    private List<Button> puzzlePieces;
    private Button selectedButton;

    // Use this for initialization
    void Start()
    {


    }

    public void StartGame(int x, int y)
    {
        xSize = x;
        ySize = y;
        InitSettings();

        InitPieces();
        MixPiecesOnBoard();
    }

    private void InitSettings()
    {
        numberOfPieces = xSize * ySize;

        puzzleBoard.GetComponent<GridLayoutGroup>().constraintCount = xSize;
    }

    private void InitPieces()
    {
        puzzlePieces = new List<Button>(numberOfPieces);
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
                puzzlePieces.Add(piece);
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
            if (CheckIfFinished())
            {
                GameEnd();
            }
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
        puzzleBoard.GetChild(i2 + shift).SetSiblingIndex(i1);
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

    private bool CheckIfFinished()
    {
        for (var i = 0; i < numberOfPieces; i++)
        {
            if (puzzleBoard.GetChild(i).gameObject != puzzlePieces[i].gameObject)
            {
                return false;
            }
        }
        return true;
    }

    private void MixPiecesOnBoard()
    {
        for (var i = 0; i < numberOfPieces; i++)
        {
            var from = Random.Range(0, numberOfPieces);
            var to = Random.Range(0, numberOfPieces);
            puzzleBoard.GetChild(from).SetSiblingIndex(to);
        }
    }

    private void GameEnd()
    {
        puzzleBoard.transform.parent.gameObject.SetActive(false);
        victoryPanel.GetComponentInChildren<RawImage>().texture = image;
        victoryPanel.gameObject.SetActive(true);

        for(var i = 0; i < numberOfPieces; i++)
        {
            Destroy(puzzlePieces[i].gameObject);
        }
        puzzlePieces.Clear();
    }
}

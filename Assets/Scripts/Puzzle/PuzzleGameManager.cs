using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{

    public GameObject playground;
    public Button puzzlePiecePrefab;
    public Texture image;
    public Transform puzzleBoard;
    public RectTransform victoryPanel;

    public event System.EventHandler OnGameEnd;

    private int xSize, ySize;
    private int numberOfPieces;
    private const float maxWidth = 1400;
    private const float maxHeight = 800;
    private const float widthHeightRatio = maxWidth / maxHeight;

    private List<Button> puzzlePieces;
    private Button selectedButton;
    
    public void StartGame(int x, int y)
    {
        xSize = x;
        ySize = y;
        InitSettings();

        InitPieces();
        MixPiecesOnBoard();

        SetupImageViews();
    }

    public void StartGame(int x, int y, RawImage image)
    {
        this.image = image.mainTexture;
        StartGame(x, y);
    }

    public void ExitGame()
    {
        GameEnd();
    }

    private void InitSettings()
    {
        numberOfPieces = xSize * ySize;
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
                Victory();
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
        var buttonColors = selectedButton.colors;
        buttonColors.normalColor = Color.white;
        selectedButton.colors = buttonColors;
        selectedButton = null;
    }

    private void SetSelection(Button piece)
    {
        selectedButton = piece;
        var buttonColors = selectedButton.colors;
        buttonColors.normalColor = buttonColors.highlightedColor;
        selectedButton.colors = buttonColors;
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

    private void SetupImageViews()
    {
        var grid = puzzleBoard.GetComponent<GridLayoutGroup>();
        grid.constraintCount = xSize;
        var playgroundTransform = playground.transform as RectTransform;

        var imageSizeRatio = (float)image.width / image.height;
        if (imageSizeRatio > widthHeightRatio)
        {
            var newsizeY = maxWidth / imageSizeRatio;
            var newCellSize = new Vector2(maxWidth / xSize, newsizeY / ySize);
            grid.cellSize = newCellSize;
            playgroundTransform.sizeDelta = new Vector2(maxWidth, newsizeY);
        }
        else
        {
            var newSizeX = maxHeight * imageSizeRatio;
            var newCellSize = new Vector2(newSizeX / xSize, maxHeight / ySize);
            grid.cellSize = newCellSize;
            playgroundTransform.sizeDelta = new Vector2(newSizeX, maxHeight);
        }
        victoryPanel.sizeDelta = playgroundTransform.sizeDelta;
        playground.SetActive(true);
    }

    private void Victory()
    {
        playground.SetActive(false);
        victoryPanel.GetComponentInChildren<RawImage>().texture = image;
        victoryPanel.gameObject.SetActive(true);
    }

    private void GameEnd()
    {
        playground.SetActive(false);
        victoryPanel.gameObject.SetActive(false);

        for (var i = 0; i < numberOfPieces; i++)
        {
            Destroy(puzzlePieces[i].gameObject);
        }
        puzzlePieces.Clear();
        OnGameEnd?.Invoke(this, new System.EventArgs());
    }
}

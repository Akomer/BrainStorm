using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderingGameManager : MonoBehaviour
{

    public Button numberButtonPrefab;
    public Transform buttonParent;

    private List<Button> buttons;
    private int xSize;
    private int ySize;
    private int numberOfButtons;
    private Vector2 spacing;
    private Vector2 startPosition;

    private OrderingModel model;

    // Use this for initialization
    void Start()
    {
        xSize = 3;
        ySize = 3;
        numberOfButtons = xSize * ySize;
        spacing = new Vector2(5f, 5f);
        var visualTotalSize = new Vector2(xSize * (50f + spacing.x) - spacing.x,
                                          ySize * (50f + spacing.y) - spacing.y);
        var buttonVisualCenter = new Vector2(50f, 50f) / 2f;
        var visualTopLeftCorner = -visualTotalSize / 2f;
        startPosition = visualTopLeftCorner + buttonVisualCenter;

        var playground = buttonParent.transform as RectTransform;
        playground.sizeDelta = visualTotalSize + new Vector2(10f, 10f);

        model = new OrderingModel();
        InitButtons();
    }

    private void InitButtons()
    {
        buttons = new List<Button>(numberOfButtons);
        for (var i = 0; i < numberOfButtons; i++)
        {
            InitButton(i);
        }
        RandomizeButtonOrder();
        SetButtonsPosition();
        EnableAllButtons();
    }

    private void InitButton(int i)
    {
        var button = Instantiate<Button>(numberButtonPrefab);
        var rectTransform = button.transform as RectTransform;
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.name += $"_{i}";
        rectTransform.SetParent(buttonParent);
        button.GetComponentInChildren<Text>().text = (i + 1).ToString();

        button.onClick.AddListener(() => ButtonClicked(button, i));

        button.gameObject.SetActive(false);
        buttons.Add(button);
    }

    private void RandomizeButtonOrder()
    {
        for (var i = 0; i < numberOfButtons; i++)
        {
            var rng = Random.Range(0, numberOfButtons);
            if (rng == i)
            {
                continue;
            }
            var tmp = buttons[i];
            buttons[i] = buttons[rng];
            buttons[rng] = tmp;
        }
    }

    private void SetButtonsPosition()
    {
        for (int i = 0, y = 0; y < ySize; y++)
        {
            for (var x = 0; x < xSize; x++, i++)
            {
                var rectTransform = buttons[i].transform as RectTransform;
                var shift = new Vector2(x * (50f + spacing.x), y * (50f + spacing.y));
                rectTransform.anchoredPosition = startPosition + shift;
            }
        }
    }

    private void EnableAllButtons()
    {
        for (var i = 0; i < numberOfButtons; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    private void Reset()
    {
        model.Reset();
        RandomizeButtonOrder();
        SetButtonsPosition();
        EnableAllButtons();
    }

    private void ButtonClicked(Button sender, int i)
    {
        if (!model.NextIs(i))
        {
            Reset();
        }
        else
        {
            sender.gameObject.SetActive(false);
            if (i == numberOfButtons - 1)
            {
                Reset();
            }
        }
    }
}

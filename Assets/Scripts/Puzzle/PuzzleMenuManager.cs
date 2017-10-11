using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMenuManager : MonoBehaviour {

    public PuzzleGameManager gameManager;
    public GameObject MenuPanel;
    public GameObject Playground;

    private void Start()
    {
        gameManager.OnGameEnd += GameEnd;
    }

    private void OnDestroy()
    {
        gameManager.OnGameEnd -= GameEnd;
    }

    public void StartSmall()
    {
        StartGame(3, 3);
    }

    public void StartMedium()
    {
        StartGame(5, 5);
    }

    public void StartBig()
    {
        StartGame(7, 7);
    }

    public void StartCustomGame(int x, int y, RawImage image)
    {
        gameManager.StartGame(x, y, image);
    }

    private void StartGame(int x, int y)
    {
        MenuPanel.SetActive(false);
        gameManager.StartGame(x, y);
    }

    private void GameEnd(object sender, EventArgs e)
    {
        Playground.SetActive(false);
        MenuPanel.SetActive(true);
    }
}

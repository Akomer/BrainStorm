using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderingMenuManager : MonoBehaviour
{
    public OrderingGameManager gameManager;
    public GameObject Playground;
    public GameObject MenuPanel;
    public GameObject InfoPanel;
    public GameObject CustomPanel;

    private void Start()
    {
        gameManager.OnGameEnd += GameEnd;
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

    public void ShowInformation()
    {
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(true);
    }

    public void ShowCustomStart()
    {
        MenuPanel.SetActive(false);
        CustomPanel.SetActive(true);
    }

    public void BackToMenuFromInfo()
    {
        InfoPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void BackToMenuFromCustom()
    {
        CustomPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void StartCustomGame(int x, int y)
    {
        CustomPanel.SetActive(false);
        StartGame(x, y);
    }

    public void BackToMainPage()
    {
        SceneManager.LoadScene("MainMenu");
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

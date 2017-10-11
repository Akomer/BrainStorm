using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleMenuManager : MonoBehaviour {

    public PuzzleGameManager gameManager;
    public GameObject MenuPanel;
    public GameObject Playground;
    public GameObject InfoPanel;
    public GameObject CustomPanel;

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

    public void BackToMainPage()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ShowInformation()
    {
        ShowPanelFromMenu(InfoPanel);
    }

    public void ShowCustomStart()
    {
        ShowPanelFromMenu(CustomPanel);
    }

    public void BackToMenuFromInfo()
    {
        BackToMenuFrom(InfoPanel);
    }

    public void BackToMenuFromCustom()
    {
        BackToMenuFrom(CustomPanel);
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

    private void BackToMenuFrom(GameObject panel)
    {
        panel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    private void ShowPanelFromMenu(GameObject panel)
    {
        MenuPanel.SetActive(false);
        panel.SetActive(true);
    }
}

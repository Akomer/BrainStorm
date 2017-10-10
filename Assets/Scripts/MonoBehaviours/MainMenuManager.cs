using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void StartOrdering()
    {
        SceneManager.LoadScene("Ordering");
    }

    public void StartPuzzle()
    {

    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

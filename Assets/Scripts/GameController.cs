using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Game Started!");
        //Change Scene
        SceneManager.LoadScene("MultiTarget");
    }

    public void EndGame()
    {
        //Close Game
        Application.Quit();
    }

    public void BackToMenu()
    {
        Debug.Log("Back to Menu!");
        //Change Scene
        SceneManager.LoadScene("Menu");
    }
}

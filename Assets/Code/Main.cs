using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Main 
{
    [RuntimeInitializeOnLoadMethod]
    public static void FirstEntryPoint()
    {
        GoToMenu();
    }

    public static void StartGame(int difficulty)
    {
        GameData.Reset();
        GameData.Difficulty = difficulty;   
        SceneManager.LoadScene("Gameplay");
    }

    public static void EndGame()
    {
        SceneManager.LoadScene("ScoreScreen");
    }

    public static void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Main
{

    public const string GameSettings = "GameSettings";
    [RuntimeInitializeOnLoadMethod]
    public static void FirstEntryPoint()
    {
        GameData.Settings = Resources.Load<GameSettings>(GameSettings);
        Debug.Log(GameData.Settings.WelcomeMessage);
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

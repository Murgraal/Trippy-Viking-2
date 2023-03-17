using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using static Code.GameplayFunctions;

public static class Main
{
    public const string GameSettings = "GameSettings";
    [RuntimeInitializeOnLoadMethod]
    public static void FirstEntryPoint()
    {
        GameData.GameSettings = Resources.Load<GameSettings>(GameSettings);
        Debug.Log(GameData.GameSettings.WelcomeMessage);
        GoToMenu();
    }
}

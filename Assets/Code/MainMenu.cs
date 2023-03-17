using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame(int difficulty)
    {
        GameplayFunctions.StartGame(difficulty);
    }
}

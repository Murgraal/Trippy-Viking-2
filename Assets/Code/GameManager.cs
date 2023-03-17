using System;
using System.Collections.Generic;
using UnityEngine;
using static Code.GameplayFunctions;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        public static event Action PhaseChanged;
        public static event Action TimerUpdated;
        
        private void Start()
        { 
            GameData.ResetGameData();
            PhaseChanged?.Invoke();
            Debug.Log("Game is running");
            StartCoroutine(TickTimer(TimerUpdated));
            
        }
        
        public void Update()
        {
            GameData.GameTimer += Time.deltaTime;
            GameData.TimeSpentInCurrentPhase += Time.deltaTime;
            
            switch (GameData.CurrentGamePhase)
            {
                case GamePhase.Regular:
                    RegularPhase(PhaseChanged);
                    break;
                case GamePhase.Transition:
                    TransitionPhase(PhaseChanged);
                    break;
                case GamePhase.Cloud:
                    CloudPhase(PhaseChanged);
                    break;
                case GamePhase.Asteroid:
                    AsteroidPhase(PhaseChanged);
                    break;
            }
        }
    }

    public enum GamePhase
    {
        Regular = 0,
        Transition = 1,
        Cloud = 2,
        Asteroid = 3,
    }

    public static class CollectionExt
    {
        public static void SwapValuesAtIndex<T>(this List<T> collection, int a, int b)
        {
            collection[a] = collection[b];
            collection[b] = collection[a];
        }
    }
}
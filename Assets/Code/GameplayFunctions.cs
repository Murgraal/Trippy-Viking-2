using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public static class GameplayFunctions
    {
        public static IEnumerator TickTimer(Action timerUpdated)
        {
            timerUpdated?.Invoke();
            while (true)
            {
                yield return GameData.TickRate;
                timerUpdated?.Invoke(); 
            }
        }
        
        public static void OnPickup(PickupType type)
        {
            switch (type)
            {
                case PickupType.Mushroom:
                    GameData.ProjectileCount++;
                    break;
                case PickupType.MiniBubble:
                    GameData.PlayerScore += GameData.Settings.PointPerPickup;
                    break;
            }
        }
        public static void SpawnEntity(Entity prefab, Vector2 position,Quaternion rotation, Transform parent)
        {
            var go = GameObject.Instantiate(prefab,position,rotation,parent);
            GameData.Entities.Add(go.GetInstanceID(),go.gameObject);
        }
        public static void RegularPhase(Action phaseChanged)
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.RegularPhaseLength)
            {
                GoToTransitionPhase();
                GameData.RegularPhasesCounter++;
                phaseChanged?.Invoke();
            }
        }

        public static void TransitionPhase(Action phaseChanged)
        {
            if (GameData.EnemiesOnScreen <= 0)
            {
                GoToNextPhase();
                phaseChanged?.Invoke();
            }
        }

        public static void CloudPhase(Action phaseChanged)
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.CloudPhaseLength)
            {
                GoToTransitionPhase();
                phaseChanged?.Invoke();
            }
        }

        public static void AsteroidPhase(Action phaseChanged)
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.AsteroidPhaseLength)
            {
                GoToTransitionPhase();
                phaseChanged?.Invoke();
            }
        }
        
        public static void OnProjectileHitEnemy(int instanceID)
        {
            AddScore();
            DespawnEntity(instanceID);
        }
        
        public static void DespawnEntity(int instanceId)
        {
            if (GameData.Entities.TryGetValue(instanceId, out var entity))
            {
                GameObject.Destroy(entity);
                GameData.LocationDatas.Remove(instanceId);
            }
            else
            {
                Debug.LogError("Trying to despawn entity that doesn't exist");
            }
        }
        
        public static void StartGame(int difficulty)
        {
            GameData.ResetGameData();
            GameData.Difficulty = difficulty;
            GameData.PlayerIsDead = false;
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
        
        public static void GoToTransitionPhase()
        {
            if (GameData.CurrentGamePhase == GamePhase.Asteroid)
            {
                GameData.NextPhase = GamePhase.Cloud;
            }
            if (GameData.CurrentGamePhase == GamePhase.Cloud)
            {
                if (GameData.RegularPhasesCounter != 0 && GameData.RegularPhasesCounter % GameData.Settings.AsteroidInterval == 0)
                {
                    GameData.RegularPhasesCounter = 0;
                    GameData.NextPhase = GamePhase.Asteroid;
                }
                else
                {
                    GameData.NextPhase = GamePhase.Regular;
                }
            }
            else if (GameData.CurrentGamePhase == GamePhase.Regular)
            {
                GameData.NextPhase = GamePhase.Cloud;
            }
            
            GameData.CurrentGamePhase = GamePhase.Transition;
        }
        public static void GoToNextPhase()
        {
            GameData.TimeSpentInCurrentPhase = 0f;
            GameData.CurrentGamePhase = GameData.NextPhase;
            GameData.PhaseCount++;
        }
        
        public static void AddScore()
        {
            
        }
    }
}
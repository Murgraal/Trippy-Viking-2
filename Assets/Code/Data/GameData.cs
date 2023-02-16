using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public static class GameData
    {
        public static GameSettings GameSettings;
        public static DifficultyData Settings => GameSettings.DifficultyDatas[Difficulty];
        public static Dictionary<int,LocationData> LocationDatas;
        public static Dictionary<int, GameObject> Entities;
        public static float PlayerScore;
        public static float GlobalMoveSpeed;
        public static int Difficulty;
        public static int ProjectileCount;
        public static PlayerWeaponType PlayerWeaponType;
        public static GamePhase GamePhase;
        public static GamePhase NextPhase;
        public static float GameTimer;
        public static float TimeSpentInCurrentPhase;
        public static int EnemiesOnScreen;
        public static int LaneCount;
        public static Vector2[] SpawnPositions;
        public static int PhaseCount;
        public static int RegularPhasesCounter;


        public static void AddLane()
        {
            LaneCount++;
            CalculateSpawnPositions();
        }
        
        private static void CalculateSpawnPositions()
        {
            SpawnPositions = new Vector2[LaneCount];
            var fragment = Screen.height / LaneCount;
            for (int i = 0; i < LaneCount; i++)
            {
                SpawnPositions[i] = new Vector2(Screen.width + 5f, fragment * i);
            }
        }
        
        public static void GoToTransitionPhase()
        {
            
            if (GamePhase == GamePhase.Asteroid)
            {
                NextPhase = GamePhase.Cloud;
            }
            if (GamePhase == GamePhase.Cloud)
            {
                if (RegularPhasesCounter != 0 && RegularPhasesCounter % Settings.AsteroidInterval == 0)
                {
                    RegularPhasesCounter = 0;
                    NextPhase = GamePhase.Asteroid;
                }
                else
                {
                    NextPhase = GamePhase.Regular;
                }
            }
            else if (GamePhase == GamePhase.Regular)
            {
                NextPhase = GamePhase.Cloud;
            }
            
            GamePhase = GamePhase.Transition;
        }
        public static void GoToNextPhase()
        {
            TimeSpentInCurrentPhase = 0f;
            GamePhase = NextPhase;
            PhaseCount++;
        }
        
        public static void AddScore()
        {
            
        }

        public static void Reset()
        {
            LocationDatas = new Dictionary<int, LocationData>();
            PlayerScore = 0f;
            GameTimer = 0f;
            PlayerWeaponType = PlayerWeaponType.Default;
            GamePhase = GamePhase.Regular;
            EnemiesOnScreen = 0;

            if (Difficulty > GameSettings.DifficultyDatas.Count) return;
            
            GlobalMoveSpeed = Settings.StartSpeed;
            ProjectileCount = Settings.StartProjectiles;
            LaneCount = Settings.StartLaneCount;
            CalculateSpawnPositions();
        }
    
        public static void UpdateData(int instanceId, LocationData data)
        {
            if (!LocationDatas.ContainsKey(instanceId))
            {
                LocationDatas.Add(instanceId,data);
            }
            LocationDatas[instanceId] = data;
        }

        public static LocationData GetLocationData(int guid)
        {
            return LocationDatas[guid];
        }
    }

    public enum PlayerWeaponType
    {
        Default,
        Spread,
        Penetrator,
        Laser,
    }
}
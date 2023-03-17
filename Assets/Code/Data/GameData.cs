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
        public static GamePhase CurrentGamePhase;
        public static GamePhase NextPhase;
        public static float GameTimer;
        public static float TimeSpentInCurrentPhase;
        public static int EnemiesOnScreen;
        public static int LaneCount;
        public static Vector2[] SpawnPositions;
        public static int PhaseCount;
        public static int RegularPhasesCounter;
        public static readonly WaitForSeconds TickRate = new WaitForSeconds(0.5f);


        public static void AddLane()
        {
            LaneCount++;
            SpawnPositions = CalculateSpawnPositions();
        }
        
        public static Vector2[] CalculateSpawnPositions()
        {
            var result = new Vector2[LaneCount];
            var fragment = Screen.height / LaneCount;
            for (int i = 0; i < LaneCount; i++)
            {
                SpawnPositions[i] = new Vector2(Screen.width + 5f, fragment * i);
            }

            return result;
        }
        
        public static void ResetGameData()
        {
            LocationDatas = new Dictionary<int, LocationData>();
            PlayerScore = 0f;
            GameTimer = 0f;
            PlayerWeaponType = PlayerWeaponType.Default;
            CurrentGamePhase = GamePhase.Regular;
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
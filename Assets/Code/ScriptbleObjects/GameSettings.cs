using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(menuName = "Murgraal/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public string WelcomeMessage;
        public List<DifficultyData> DifficultyDatas = new List<DifficultyData>();
    }
    
    [Serializable]
    public struct DifficultyData
    {
        public int PointPerPickup;
        public int RegularPhaseLength;
        public int CloudPhaseLength;
        public int AsteroidPhaseLength;
        public int AsteroidInterval;
        public float StartSpeed;
        public int StartProjectiles;
        public int StartLaneCount;
    }
}
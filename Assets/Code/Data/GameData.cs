using System.Collections.Generic;

namespace Code
{
    public static class GameData
    {
        public static GameSettings Settings;
        public static Dictionary<int,LocationData> LocationDatas;
        public static float PlayerScore;
        public static float GlobalMoveSpeed;
        public static int Difficulty;
    
        public static void Reset()
        {
            LocationDatas = new Dictionary<int, LocationData>();
            PlayerScore = 0f;
        
            if (Settings.startSpeeds.Length < Difficulty) return;
        
            GlobalMoveSpeed = Settings.startSpeeds[Difficulty];

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
}
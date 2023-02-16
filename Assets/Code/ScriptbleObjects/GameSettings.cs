using UnityEngine;

namespace Code
{
    [CreateAssetMenu(menuName = "Murgraal/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public string WelcomeMessage;
        public int pointPerPickup;
        public float[] StartSpeeds = new[]
        {
            3f,
            4f,
            5f,
        };

        public int[] StartProjectiles = new[]
        {
            5,
            4,
            3,
        };
    }
}
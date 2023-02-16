using UnityEngine;

namespace Code
{
    [CreateAssetMenu(menuName = "Murgraal/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public string WelcomeMessage;
        public float[] startSpeeds = new[]
        {
            3f,
            4f,
            5f,
        };
    }
}
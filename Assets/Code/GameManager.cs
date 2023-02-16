using UnityEngine;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            GameData.Reset();
            Debug.Log("Game is running");
        }

        public static void SpawnEntity(Entity prefab, Vector2 position,Quaternion rotation, Transform parent)
        {
            var go = Instantiate(prefab,position,rotation,parent);
        }

        public static void OnPickup(PickupType type)
        {
        
        }
    
        public void Update()
        {
        
        }
    }
}
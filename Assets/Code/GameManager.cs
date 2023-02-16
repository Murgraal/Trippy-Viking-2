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
            GameData.Entities.Add(go.GetInstanceID(),go.gameObject);
        }

        public static void DespawnEntity(int instanceId)
        {
            if (GameData.Entities.TryGetValue(instanceId, out var entity))
            {
                Destroy(entity);
                GameData.LocationDatas.Remove(instanceId);
            }
            else
            {
                Debug.LogError("Trying to despawn entity that doesn't exist");
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
                    GameData.PlayerScore += GameData.Settings.pointPerPickup;
                    break;
            }
        }

        public static void OnProjectileHitEnemy(int instanceID)
        {
            GameData.AddScore();
            DespawnEntity(instanceID);
        }
    
        public void Update()
        {
            switch (GameData.GamePhase)
            {
                case GamePhase.Regular:
                    RegularPhase();
                    break;
                case GamePhase.Transition:
                    TransitionPhase();
                    break;
                case GamePhase.Cloud:
                    CloudPhase();
                    break;
                case GamePhase.Laser:
                    LaserPhase();
                    break;
            }
        }

        public void RegularPhase()
        {
            
        }

        public void TransitionPhase()
        {
            
        }

        public void CloudPhase()
        {
            
        }

        public void LaserPhase()
        {
            
        }
    }
    
    public enum GamePhase
    {
        Regular,
        Transition,
        Cloud,
        Laser
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<GamePhase> OnPhaseChanged;
        public static event Action OnTimerUpdated;
        
        private void Start()
        {
            GameData.Reset();
            OnPhaseChanged?.Invoke(GameData.GamePhase);
            Debug.Log("Game is running");
            StartCoroutine(TickTimer());
        }

        private WaitForSeconds TickRate = new WaitForSeconds(0.5f);

        private IEnumerator TickTimer()
        {
            OnTimerUpdated?.Invoke();
            while (true)
            {
                yield return TickRate;
                OnTimerUpdated?.Invoke(); 
            }
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
            GameData.GameTimer += Time.deltaTime;
            GameData.TimeSpentInCurrentPhase += Time.deltaTime;
            
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
            }
        }

        public void RegularPhase()
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.RegularPhaseLength)
            {
                GameData.GoToTransitionPhase();
                OnPhaseChanged?.Invoke(GameData.GamePhase);
            }
        }

        public void TransitionPhase()
        {
            if (GameData.EnemiesOnScreen <= 0)
            {
                GameData.GoToNextPhase();
                OnPhaseChanged?.Invoke(GameData.GamePhase);
            }
        }

        public void CloudPhase()
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.CloudPhaseLength)
            {
                GameData.GoToTransitionPhase();
                OnPhaseChanged?.Invoke(GameData.GamePhase);
            }
        }
        
    }
    
    public enum GamePhase
    {
        Regular = 0,
        Transition = 1,
        Cloud = 2,
    }
}
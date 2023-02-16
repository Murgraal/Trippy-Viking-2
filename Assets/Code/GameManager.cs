using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        public static event Action PhaseChanged;
        public static event Action TimerUpdated;
        
        private void Start()
        {
            GameData.Reset();
            PhaseChanged?.Invoke();
            Debug.Log("Game is running");
            StartCoroutine(TickTimer());
        }

        private WaitForSeconds TickRate = new WaitForSeconds(0.5f);

        private IEnumerator TickTimer()
        {
            TimerUpdated?.Invoke();
            while (true)
            {
                yield return TickRate;
                TimerUpdated?.Invoke(); 
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
                    GameData.PlayerScore += GameData.Settings.PointPerPickup;
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
                case GamePhase.Asteroid:
                    AsteroidPhase();
                    break;
            }
        }

        public void RegularPhase()
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.RegularPhaseLength)
            {
                GameData.GoToTransitionPhase();
                GameData.RegularPhasesCounter++;
                PhaseChanged?.Invoke();
            }
        }

        public void TransitionPhase()
        {
            if (GameData.EnemiesOnScreen <= 0)
            {
                GameData.GoToNextPhase();
                PhaseChanged?.Invoke();
            }
        }

        public void CloudPhase()
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.CloudPhaseLength)
            {
                GameData.GoToTransitionPhase();
                PhaseChanged?.Invoke();
            }
        }

        public void AsteroidPhase()
        {
            if (GameData.TimeSpentInCurrentPhase > GameData.Settings.AsteroidPhaseLength)
            {
                GameData.GoToTransitionPhase();
                PhaseChanged?.Invoke();
            }
        }
        
    }

    public enum GamePhase
    {
        Regular = 0,
        Transition = 1,
        Cloud = 2,
        Asteroid = 3,
    }

    public static class CollectionExt
    {
        public static void SwapValuesAtIndex<T>(this List<T> collection, int a, int b)
        {
            collection[a] = collection[b];
            collection[b] = collection[a];
        }
    }
}
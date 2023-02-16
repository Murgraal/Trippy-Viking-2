using System;
using System.Collections;
using System.Collections.Generic;
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

    public abstract class Entity : MonoBehaviour
    {
        protected LocationData data;
        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
            UpdateBehaviour();
        }

        protected abstract void UpdateBehaviour();
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Player : Entity
    {
        private Rigidbody2D rigid;
    
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
        
        }

        

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                StartCoroutine(Die());
            }
        }
    
        private IEnumerator Die()
        {
            Main.EndGame();
            yield return null;
        }

        public override void UpdateBehaviour()
        {
            throw new NotImplementedException();
        }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Pickup : Entity
    {
        private Rigidbody2D rigid;
        [SerializeField]
        private PickupType type;
    
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rigid.velocity = Vector2.left * GameData.GlobalMoveSpeed;
        }

        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
        }

        public override void UpdateBehaviour()
        {
            throw new NotImplementedException();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                GameManager.OnPickup(type);
            }
        }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : Entity
    {
        private Rigidbody2D rigid;
    
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rigid.velocity = Vector2.left * GameData.GlobalMoveSpeed;
        }

        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
        }

        public override void UpdateBehaviour()
        {
            throw new NotImplementedException();
        }
    }

    public enum PickupType
    {
        Mushroom,
        Penetrator,
        Spread,
        MiniBubble
    }

    public class GameSettings
    {

        public float[] startSpeeds = new[]
        {
            3f,
            4f,
            5f,
        };
    }

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

    public struct LocationData
    {
        public Vector2 Position;
        public Quaternion Rotation;
    }
}
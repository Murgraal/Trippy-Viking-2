using System;
using UnityEngine;

namespace Code
{
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
            GameData.EnemiesOnScreen++;
        }

        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
            
            if (!OnScreen())
            {
                GameManager.DespawnEntity(GetInstanceID());
            }
        }

        public override void Despawn()
        {
            GameData.EnemiesOnScreen--;
        }

        protected override void UpdateBehaviour()
        {
            
        }
    }
}
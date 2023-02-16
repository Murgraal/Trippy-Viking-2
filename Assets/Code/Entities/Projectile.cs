using System;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : Entity
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

        protected override void UpdateBehaviour()
        {
            throw new NotImplementedException();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                GameManager.OnProjectileHitEnemy(col.GetInstanceID());
            }
        }
    }
}
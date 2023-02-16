using System;
using UnityEngine;

namespace Code
{
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

        public override void Despawn()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateBehaviour()
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
}
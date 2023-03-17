using UnityEngine;
using static Code.GameplayFunctions;

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
        
        public override void Despawn()
        {
            GameData.EnemiesOnScreen--;
        }

        protected override void UpdateBehaviour()
        {
            GameData.UpdateData(GetInstanceID(),data);
            
            if (!OnScreen())
            {
                DespawnEntity(GetInstanceID());
            }
        }
    }
}
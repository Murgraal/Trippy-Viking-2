using UnityEngine;
using static Code.GameplayFunctions;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : Entity
    {
        private Rigidbody2D rigid;
        private bool justSpawned = true;
        
        private void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.left * GameData.GlobalMoveSpeed;
            GameData.EnemiesOnScreen++;
        }
        
        public override void Despawn()
        {
            GameData.EnemiesOnScreen--;
        }

        protected override void UpdateBehaviour()
        {
            if (OnScreen() && justSpawned)
            {
                justSpawned = false;
            }
            if (!OnScreen() && !justSpawned)
            {
                DespawnEntity(GetInstanceID());
            }
        }
    }
}
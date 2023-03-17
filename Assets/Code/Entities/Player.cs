using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Code.PlayerFunctions;
using static Code.GameplayFunctions;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : Entity
    {
        private Rigidbody2D rigid;
        private Vector2 startPos;
        private Vector2 upPos;
        private Vector2 downPos;
        private Vector2 bounds;
    
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            startPos = transform.position;
            upPos = startPos;
            downPos = startPos;
            bounds = GetComponent<Collider2D>().bounds.extents;
            
            upPos.y += Camera.main.orthographicSize;
            downPos.y -= Camera.main.orthographicSize;
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
            EndGame();
            yield return null;
        }

        public override void Despawn()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateBehaviour()
        {
            HoldPlayerInScreen(transform,bounds,downPos,upPos,rigid);
        }
    }

    public static class PlayerFunctions
    {
        public static void HoldPlayerInScreen(Transform transform, Vector2 bounds, Vector2 downPos, Vector2 upPos, Rigidbody2D rigid)
        {
            if (transform.position.y + bounds.y < downPos.y)
            {
                if (rigid.velocity.y > 0) return;
                transform.position = upPos;
            }
            else if (transform.position.y - bounds.y > upPos.y)
            {
                if (rigid.velocity.y < 0) return;
                transform.position = downPos;
            }
        }
    }
    
}
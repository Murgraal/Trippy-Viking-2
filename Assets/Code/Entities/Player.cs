using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
            Main.EndGame();
            yield return null;
        }

        public override void Despawn()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateBehaviour()
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
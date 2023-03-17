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
        private Vector2 size;

        private bool flyPressed;
    
        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            startPos = transform.position;
            upPos = startPos;
            downPos = startPos;
            size = GetComponent<Collider2D>().bounds.size;
            
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
            GameData.PlayerIsDead = true;
            var deathTimer = 0f;
            rigid.AddForce(Vector2.up * 10,ForceMode2D.Impulse);
            while (deathTimer < 3f)
            {
                transform.RotateAround(transform.position,transform.up,Time.deltaTime * 90f);
                deathTimer += Time.deltaTime;
                yield return null;
            }
            yield return null;
            EndGame();
        }

        public override void Despawn()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateBehaviour()
        {
            if (GameData.PlayerIsDead) return;
            HoldPlayerInScreen(transform,size,downPos,upPos,rigid);
            flyPressed = Input.GetKey(KeyCode.Space);
        }

        protected override void FixedUpdateBehaviour()
        {
            if (flyPressed)
            {
                Fly(rigid,GameData.GameSettings.BaseFlySpeed);
            }
        }
    }

    public static class PlayerFunctions
    {
        public static void Fly(Rigidbody2D rigid, float speed)
        {
            rigid.AddForce(Vector2.up * speed);
        }
        
        public static void HoldPlayerInScreen(Transform transform, Vector2 size, Vector2 downPos, Vector2 upPos, Rigidbody2D rigid)
        {
            if (transform.position.y  < downPos.y)
            {
                if (rigid.velocity.y > 0) return;
                var pos = upPos;
                transform.position = pos;
            }
            else if (transform.position.y > upPos.y)
            {
                if (rigid.velocity.y < 0) return;
                var pos = downPos;
                transform.position = downPos;
            }
        }
    }
    
}
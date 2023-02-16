using System;
using System.Collections;
using UnityEngine;

namespace Code
{
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

        protected override void UpdateBehaviour()
        {
            throw new NotImplementedException();
        }
    }
}
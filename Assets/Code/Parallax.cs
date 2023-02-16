using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code
{
    public class Parallax : MonoBehaviour
    {
        private List<SpriteRenderer> Renderers;
        private float delta;
        private float sizeX;
        
        [SerializeField]
        private float speed;

        private List<Vector2> startPositions = new List<Vector2>();
        
        private void Start()
        {
            Renderers = transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>().ToList();
            sizeX = Renderers[0].sprite.bounds.size.x;
            for (int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].transform.position += (Vector3)Vector2.right * sizeX * i;
                startPositions.Add(Renderers[i].transform.position);
            }
        }

        public void Update()
        {
            delta += Time.deltaTime * speed;
            for (int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].gameObject.transform.position += (Vector3) Vector2.left * Time.deltaTime * speed;
            }

            if (delta >= sizeX)
            {
                Debug.Log("ReachedDelta");
                var last = Renderers.Count - 1;
                
                var movedOutOfScreen = Renderers[0];
                
                for (int i = 1; i < Renderers.Count; i++)
                {
                    Renderers[i - 1] = Renderers[i];
                }
                
                Renderers[Renderers.Count - 1] = movedOutOfScreen;
                
                for (int i = 0; i < Renderers.Count; i++)
                {
                    Renderers[i].transform.position = startPositions[i];
                }
                
                delta = 0f;
            }
        }
    }
}

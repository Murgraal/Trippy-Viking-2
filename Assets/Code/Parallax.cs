using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Code.ParallaxFunctions;

namespace Code
{
    public class Parallax : MonoBehaviour
    {
        public List<SpriteRenderer> Renderers;
        public float delta;
        public float sizeX;
        
        [SerializeField]
        private float speed;

        private List<Vector2> startPositions = new List<Vector2>();
        
        public void Init()
        {
            Renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            sizeX = Renderers[0].sprite.bounds.size.x;
            for (int i = 0; i < Renderers.Count; i++)
            {
                Renderers[i].transform.position += (Vector3)Vector2.right * sizeX * i;
                startPositions.Add(Renderers[i].transform.position);
            }
        }

        public void Update()
        {
            UpdateParallax(ref delta,speed,Renderers,sizeX,startPositions);
        }
    }

    public static class ParallaxFunctions
    {

        public static void UpdateParallax(ref float delta,float speed,List<SpriteRenderer> renderers,float sizeX, List<Vector2> startPositions)
        {
            delta += Time.deltaTime * speed;
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].gameObject.transform.position += (Vector3) Vector2.left * Time.deltaTime * speed;
            }

            if (delta >= sizeX)
            {
                Debug.Log("ReachedDelta");
                var last = renderers.Count - 1;
                
                var movedOutOfScreen = renderers[0];
                
                for (int i = 1; i < renderers.Count; i++)
                {
                    renderers[i - 1] = renderers[i];
                }
                
                renderers[renderers.Count - 1] = movedOutOfScreen;
                
                for (int i = 0; i < renderers.Count; i++)
                {
                    renderers[i].transform.position = startPositions[i];
                }
                
                delta = 0f;
            }
        }
        
        public static void SetOrderInLayerForAll(int order, IEnumerable<SpriteRenderer> renderers)
        {
            foreach (var rend in renderers)
            {
                rend.sortingOrder = order;
            }
        }
    }
}

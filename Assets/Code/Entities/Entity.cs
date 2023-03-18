using System;
using UnityEngine;

namespace Code
{
    public abstract class Entity : MonoBehaviour
    {
        protected LocationData data;

        private void Awake()
        {
            
        }

        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
            UpdateBehaviour();
        }

        private void FixedUpdate()
        {
            FixedUpdateBehaviour();
        }

        protected bool OnScreen()
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            var onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
            return onScreen;
        }

        public abstract void Despawn();

        protected abstract void UpdateBehaviour();

        protected virtual void FixedUpdateBehaviour()
        {
            
        }
    }
}
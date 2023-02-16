using System;
using UnityEngine;

namespace Code
{
    public abstract class Entity : MonoBehaviour
    {
        protected LocationData data;
        protected Camera currentCam;

        private void Awake()
        {
            currentCam = Camera.current;
        }

        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
            UpdateBehaviour();
        }

        protected bool OnScreen()
        {
            var screenPos = currentCam.WorldToScreenPoint(transform.position);
            var onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;
            return onScreen;
        }

        public abstract void Despawn();

        protected abstract void UpdateBehaviour();
    }
}
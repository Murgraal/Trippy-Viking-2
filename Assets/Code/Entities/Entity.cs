using System;
using UnityEngine;

namespace Code
{
    public abstract class Entity : MonoBehaviour
    {
        protected LocationData data;
        private void Update()
        {
            GameData.UpdateData(GetInstanceID(),data);
            UpdateBehaviour();
        }

        protected void Despawn()
        {
            
        }

        protected abstract void UpdateBehaviour();
    }
}
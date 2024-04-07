using System;
using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        public event Action<float> OnDamage;
        
        public void ApplyDamage(float damageValue)
        {
            OnDamage?.Invoke(damageValue);
        }
        
#if UNITY_EDITOR
        private void Awake()
        {
            if (!TryGetComponent(out Collider2D component))
            {
                Debug.LogError("Damageable component requires a Collider2D component!");
            }
        }
#endif
    }
}
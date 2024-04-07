using System;
using Sirenix.OdinInspector;
using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public event Action<EnemyLinker> OnEnemyDied;

        [Header("Scripts")]
        [SerializeField] private EnemyLinker _enemyLinker;
        
        [Header("Stats")]
        [SerializeField] private float _maxHealth = 10;

        [InfoBox("Zero value will completely remove coming damage")]
        [Range(0, 1f)]
        [SerializeField] private float _armorValue = 1;
        
        private float _healthValue;

        private float HealthValue
        {
            get => _healthValue;
            set => _healthValue = Math.Clamp(value, 0, _maxHealth);
        }

        private void Start()
        {
            HealthValue = _maxHealth;
        }

        public void ApplyDamage(float damageValue)
        {
            HealthValue = _armorValue * damageValue;

            if (HealthValue == 0)
            {
                OnEnemyDied?.Invoke(_enemyLinker);
                Destroy(gameObject);
            }
        }
    }
}
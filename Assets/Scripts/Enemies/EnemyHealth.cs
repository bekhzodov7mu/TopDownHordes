using System;
using TMPro;
using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public event Action<EnemyLinker> OnEnemyDied;

        [Header("Scripts")]
        [SerializeField] private EnemyLinker _enemyLinker;
        [Space]
        [SerializeField] private Damageable _damageable;

        [Header("Components")]
        [SerializeField] private TextMeshPro _healthValueText;
        
        [Header("Stats")]
        [SerializeField] private float _maxHealth = 10;
        
        [Range(0, 1f)]
        [SerializeField] private float _armorValue = 1;
        
        private float _healthValue;

        private float HealthValue
        {
            get => _healthValue;
            set
            {
                _healthValue = Math.Clamp(value, 0, _maxHealth);
                _healthValueText.text = $"{Mathf.Round(_healthValue)}";
            }
        }

        private void Start()
        {
            HealthValue = _maxHealth;
        }

        private void OnEnable()
        {
            _damageable.OnDamage += ApplyDamage;
        }

        private void OnDisable()
        {
            _damageable.OnDamage -= ApplyDamage;
        }

        public void ApplyDamage(float damageValue)
        {
            HealthValue -= _armorValue * damageValue;

            if (HealthValue == 0)
            {
                OnEnemyDied?.Invoke(_enemyLinker);
                Destroy(gameObject);
            }
        }
    }
}
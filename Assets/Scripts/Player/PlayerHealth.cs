using System;
using TopDownHordes.Interfaces;
using TopDownHordes.UI;
using UnityEngine;
using Zenject;

namespace TopDownHordes.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Space]
        [SerializeField] private Damageable _damageable;
        
        [Header("Stats")]
        [SerializeField] private float _maxHealth = 100;
        
        [Range(0, 1f)]
        [SerializeField] private float _armorValue = 1;

        [Inject] private GamePlayUI _gamePlayUI;
        
        private float _healthValue;

        private float HealthValue
        {
            get => _healthValue;
            set
            {
                _healthValue = Math.Clamp(value, 0, _maxHealth);
                _gamePlayUI.OnPlayerHealthChange(_maxHealth, value);
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
                Destroy(gameObject);
            }
        }
    }
}
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public abstract class SpellProjectile : MonoBehaviour
    { 
        [SerializeField] private Transform _visual;
        
        [Header("Stats")]
        [SerializeField] private float _lifeTime = 7f; // TODO: Move to PlayerSpell SO
        [Space]

        private float _speed;
        private float _damage;
        
        private void Update()
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        }

        protected async UniTask StartCountdown(CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime), cancellationToken: cancellationToken);
            Explode();
        }
        
        protected abstract void Explode();

        protected void DealDamage(IDamageable damageable)
        {
            damageable.ApplyDamage(_damage);
        }
        
        public void Init(float speed, float damage)
        {
            _speed = speed;
            _damage = damage;

            var scale = _visual.localScale;
            _visual.localScale = Vector3.zero;
            _visual.DOScale(scale, 0.3f);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public class DeadlyMist : SpellProjectile
    {
        [SerializeField] private float _damageRepetitionTime = 0.1f;
        
        private readonly HashSet<IDamageable> _damageables = new();

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        private void Start()
        {
            DamageOverTime(_cancellationTokenSource.Token).Forget();
            StartCountdown(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            if (other.TryGetComponent(out IDamageable damageable))
            {
                _damageables.Add(damageable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && _damageables.Contains(damageable))
            {
                _damageables.Remove(damageable);
            }
        }

        private async UniTask DamageOverTime(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var cached = new HashSet<IDamageable>(_damageables);
                foreach (var damageable in cached)
                {
                    DealDamage(damageable);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_damageRepetitionTime), cancellationToken: cancellationToken);
            }
        }
        
        protected override void Explode()
        {
            _cancellationTokenSource?.Cancel();

            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                Destroy(gameObject); // TODO: ObjectPool
            });
        }
    }
}
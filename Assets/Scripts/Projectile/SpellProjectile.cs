using DG.Tweening;
using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public abstract class SpellProjectile : MonoBehaviour
    { 
        [SerializeField] private Transform _visual;

        private float _speed;
        private float _damage;
        
        private void Update()
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
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
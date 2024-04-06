using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public class MiniFireball : SpellProjectile
    {
        [SerializeField] private GameObject _explosionParticle;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }

            if (other.TryGetComponent(out IDamageable damageable))
            {
                DealDamage(damageable);
            }
            
            Explode();
        }
        
        protected override void Explode()
        {
            Instantiate(_explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
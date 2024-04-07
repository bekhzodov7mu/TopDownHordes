using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Enemies
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private float _damage = 1;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player") && other.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
            }
        }
    }
}
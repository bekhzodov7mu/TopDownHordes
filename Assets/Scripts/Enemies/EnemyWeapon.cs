using TopDownHordes.Interfaces;
using UnityEngine;

namespace TopDownHordes.Enemies
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private float _damage = 1;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            // TODO: tags should be moved to "Constants" static class
            if (other.transform.CompareTag("Player") && other.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
            }
        }
        // TODO: change OnCollisionEnter2D to Physics2D.OverlapCircleAll
        // TODO: attack interval value (sync with animation if required) and attack range
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public class SpellProjectile : MonoBehaviour
    {
        [Header("Components")]
        [Required]
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private float _speed;
        
        public void Init(float speed)
        {
            _speed = speed;
        }
        
        private void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.up * _speed;
        }

        private void Explode()
        {
            // TODO:
            Destroy(gameObject);
        }
    }
}
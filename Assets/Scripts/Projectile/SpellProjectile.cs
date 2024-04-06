using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public class SpellProjectile : MonoBehaviour
    {
        [Header("Components")]
        [Required]
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private Transform _visual;

        private float _speed;
        
        private void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.up * _speed;
        }

        private void Explode()
        {
            // TODO:
            Destroy(gameObject);
        }
        
        public void Init(float speed)
        {
            _speed = speed;

            var scale = _visual.localScale;
            _visual.localScale = Vector3.zero;
            _visual.DOScale(scale, 0.3f);
        }
    }
}
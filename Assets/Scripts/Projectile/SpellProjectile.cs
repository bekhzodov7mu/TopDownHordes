using DG.Tweening;
using UnityEngine;

namespace TopDownHordes.Projectile
{
    public class SpellProjectile : MonoBehaviour
    { 
        [SerializeField] private Transform _visual;

        private float _speed;
        
        private void Update()
        {
            _visual.Translate(Vector3.up * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }
            
            Explode();
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
using UnityEngine;

namespace TopDownHordes.Enemies
{
    public abstract class BaseEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 1;

        private Transform _target;
        
        private void Update()
        {
            if (_target != null)
            {
                Vector3 direction = _target.position - transform.position;
                float distanceThisFrame = _movementSpeed * Time.deltaTime;

                if (direction.magnitude <= distanceThisFrame)
                {
                    transform.position = _target.position;
                }
                else
                {
                    transform.Translate(direction.normalized * distanceThisFrame, Space.World);
                }
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
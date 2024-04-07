using UnityEngine;

namespace TopDownHordes.Enemies
{
    public abstract class BaseEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 1;

        private const int RotationOffset = -90;
        
        private Transform _target;
        
        private void Update()
        {
            MoveEnemy();
        }

        private void MoveEnemy()
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

                float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotationZ + RotationOffset);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
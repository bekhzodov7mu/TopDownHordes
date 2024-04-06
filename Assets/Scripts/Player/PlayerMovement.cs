using UnityEngine;

namespace TopDownHordes.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float _movementSpeed = 5f;
        
        // Move to some LevelData so every level can have different size
        // If level has different shape - just use BoxColliders
        [SerializeField] private float _radius = 5f;
        [SerializeField] private Vector3 _circlePos = Vector3.zero;
        
        private const float Offset = 270;
        
        private Vector2 _movementVelocity;
        private Vector2 _movementInput;
        
        private float _horizontalMove;
        private float _verticalMove;

        private void Update()
        {
            GetInput();
            MovePlayer();
        }
        
        private void LateUpdate()
        {
            ClampToCircle();
        }

        private void GetInput()
        {
            _horizontalMove = Input.GetAxis("Horizontal");
            _verticalMove = Input.GetAxis("Vertical");
        }

        private void MovePlayer()
        {
            Vector3 movement = new(_horizontalMove, _verticalMove);
            transform.position += movement * (_movementSpeed * Time.deltaTime);
            
            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + Offset;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void ClampToCircle()
        {
            float distanceFromCenter = (_circlePos - transform.position).magnitude;

            if (distanceFromCenter > _radius)
            {
                Vector3 fromCenterToObject = transform.position - _circlePos;
                transform.position = _circlePos + Vector3.ClampMagnitude(fromCenterToObject, _radius);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_circlePos, _radius);
        }
    }
}

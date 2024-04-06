using Sirenix.OdinInspector;
using UnityEngine;

namespace TopDownHordes.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        [Required]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [Header("Stats")]
        [SerializeField] private float _movementSpeed = 5f;
        
        private const float Offset = 270;
        
        private Vector2 _movementVelocity;
        private Vector2 _movementInput;
        private Camera _camera;
        
        private float _horizontalMove;
        private float _verticalMove;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            GetInput();
            RotatePlayer();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void GetInput()
        {
            _horizontalMove = Input.GetAxis("Horizontal");
            _verticalMove = Input.GetAxis("Vertical");
        }

        private void MovePlayer()
        {
            _rigidbody2D.velocity = new Vector2(_horizontalMove * _movementSpeed, 
                _verticalMove * _movementSpeed);
        }

        private void RotatePlayer()
        {
            Vector3 difference = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);
        }
    }
}

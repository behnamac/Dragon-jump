using Controllers;
using Enums;
using UnityEngine;

namespace Player
{
    public class PlayerFlipController : MonoBehaviour
    {
        [SerializeField] private Vector3 jumpForce;
        [SerializeField] private Vector3 backJumpForce;
        [SerializeField] private float runSpeed;
        [SerializeField] private int jumpValue;

        private bool _canRun;
        private bool _hasJumped;
        private bool _isFalling;
        private bool _canBackPush;
        private float _heightFromPreviousFrame;
        private int _currentJumpValue;

        private Rigidbody _rigidbody;
        private PlayerAnimationController _playerAnimationController;

        public PlayerAnimationController AnimationController => _playerAnimationController;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerAnimationController = GetComponent<PlayerAnimationController>();
            _currentJumpValue = jumpValue;

            _canBackPush = true;
        }

        private void OnEnable()
        {
            InputController.OnTap += OnTapHandler;
        }

        private void OnDisable()
        {
            InputController.OnTap -= OnTapHandler;
        }

        private void Update()
        {
            if (GameManager.Instance.IsLevelFinished) return;
            
            if (_canRun || _isFalling)
            {
                float moveSpeed = _isFalling ? runSpeed / 1.5f : runSpeed;
                transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
            }

            float currentHeight = transform.position.y;
            if (_heightFromPreviousFrame > currentHeight + 0.05f)
            {
               // _isFalling = SetOtherStatesFalse();
                _playerAnimationController.ActivateFallAnimation();
            }
            
            _heightFromPreviousFrame = currentHeight;
        }

        public void PushBack()
        {
            Vector3 reverseJump = backJumpForce;
            
            _rigidbody.velocity = reverseJump;

            ResetJump();
        }

        public void Run()
        {
            _canRun = SetOtherStatesFalse();
            _heightFromPreviousFrame = -100;

            _playerAnimationController.ActivateRunAnimation();
            _currentJumpValue = jumpValue;
        }

        private void OnTapHandler()
        {
            Jump();
        }
        
        private void Jump()
        {
            if (_currentJumpValue <= 0) return;
            _rigidbody.velocity = jumpForce;
            
            _currentJumpValue--;
            ResetJump();
        }
        
        private void ResetJump()
        {
            _hasJumped = SetOtherStatesFalse();
            _heightFromPreviousFrame = -100;
            _playerAnimationController.ActivateFlipAnimation();
        }
        private void ActiveCanBackPush() 
        {
            _canBackPush = true;
        }

        /// <summary>
        /// Sets false every state and returns true to assign chosen state.
        /// </summary>
        /// <returns> Always TRUE.</returns>
        private bool SetOtherStatesFalse()
        {
            _canRun = false;
            _isFalling = false;
            _hasJumped = false;
            
            return true;
        }
    }
}
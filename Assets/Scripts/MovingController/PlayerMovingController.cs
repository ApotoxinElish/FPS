using UnityEngine;
using Managers;
using AbstractClass;

namespace MovingController
{
    public class PlayerMovingController : AbstractMovingObject
    {
        // player moving controller
        private Rigidbody _rgBody;

        private bool _moveForward = false;
        private bool _moveBackward = false;
        private bool _moveLeft = false;
        private bool _moveRight = false;
        private bool _playerJump = false;
        private bool _playerDodge = false;

        public float playerMoveSpeed;
        public float playerJumpSpeed;
        public float gravitySpeed;
        public float capsColdRadiusOffsetFactor = 0.5f;  // (0.5 recommended)

        // keycodes
        private KeyCode _moveForwardKey;
        private KeyCode _moveBackwardKey;
        private KeyCode _moveLeftKey;
        private KeyCode _moveRightKey;
        private KeyCode _jumpKey;
        private KeyCode _dodgeKey;
        
        // for detecting whether the player is grounded
        private CapsuleCollider _capsCold;
        private float _capsColdRadius;
        private Vector3 _capsPointUp;
        private Vector3 _capsPointDown;
        public float overLapCapsuleOffset = 1.55f;  // (1.5 recommended)
        private bool _stayUngrounded;  // flag representing whether the the player is grounded

        private void Start()
        {
            // base.Start();
            _rgBody = GetComponent<Rigidbody>();
            _capsCold = GetComponent<CapsuleCollider>();
            SetPlayerMoveSpeed(playerMoveSpeed);
            InitJumpSpeed(playerJumpSpeed);

            // load keybinding from GlobalManager
            _moveForwardKey = GlobalManager.Instance.KeyBinding["player move forward"];
            _moveBackwardKey = GlobalManager.Instance.KeyBinding["player move backward"];
            _moveLeftKey = GlobalManager.Instance.KeyBinding["player move left"];
            _moveRightKey = GlobalManager.Instance.KeyBinding["player move right"];
            _jumpKey = GlobalManager.Instance.KeyBinding["player jump"];
            _dodgeKey = GlobalManager.Instance.KeyBinding["player dodge"];
            
            // jump related
            _capsColdRadius = _capsCold.radius * capsColdRadiusOffsetFactor;
        }

        private void GetInputMove()
        {
            // get keyboard input
            _moveForward = Input.GetKey(_moveForwardKey);
            _moveBackward = Input.GetKey(_moveBackwardKey);
            _moveLeft = Input.GetKey(_moveLeftKey);
            _moveRight = Input.GetKey(_moveRightKey);
            _playerJump = Input.GetKeyDown(_jumpKey);
        }

        public override bool CanMove()
        {
            return _moveForward || _moveBackward || _moveLeft || _moveRight;
        }

        private void SetPlayerMoveSpeed(float speed)
        {
            ForceSetMoveSpeed(speed);
        }

        private void Update()
        {
            GetInputMove(); // get keyboard input
            
            // can not place the codes in FixedUpdate()!!!
            if (_playerJump)
            {
                // detect whether the player is grounded
                var transform1 = transform;
                var up = transform1.up;
                var position = transform1.position;
                _capsPointUp =  position + up * _capsCold.height - up * _capsColdRadius;
                _capsPointDown = position + up * _capsColdRadius - up * overLapCapsuleOffset;
                LayerMask ignoreMask = (1 << 6);
                var outputCols = Physics.OverlapCapsule(_capsPointDown, _capsPointUp, _capsColdRadius, ignoreMask);
                // Debug.DrawLine(_capsPointDown, _capsPointUp, Color.green);
                if (outputCols.Length != 0)
                {
                    // the player is grounded, then player jumps
                    Debug.Log("player jump");
                    _rgBody.AddForce(Vector3.up * 1000f);

                }
            }

        }
        

        private void FixedUpdate()
        {
            // player moving, adding force to rigidBody
            if (CanMove())
            {
                var p = _rgBody.position;
                var transform1 = transform;
                var facing = transform1.forward;
                var right = transform1.right;
                
                // when moving with 45 degree, force on x, y axis should be divided by root 2
                if (_moveForward && _moveLeft)
                {
                    _rgBody.AddForce(facing * playerMoveSpeed / 1.414f);
                    _rgBody.AddForce(right * -playerMoveSpeed / 1.414f);
                    // p.z += GetMoveSpeed() * Time.deltaTime;
                }
                else if (_moveBackward && _moveLeft)
                {
                    _rgBody.AddForce(facing * -playerMoveSpeed / 1.414f);
                    _rgBody.AddForce(right * -playerMoveSpeed / 1.414f);
                    // p.z -= GetMoveSpeed() * Time.deltaTime;
                }
                else if (_moveForward && _moveRight)
                {
                    _rgBody.AddForce(facing * playerMoveSpeed / 1.414f);
                    _rgBody.AddForce(right * playerMoveSpeed / 1.414f);
                    // p.z += GetMoveSpeed() * Time.deltaTime;
                }
                else if (_moveBackward && _moveRight)
                {
                    _rgBody.AddForce(facing * -playerMoveSpeed / 1.414f);
                    _rgBody.AddForce(right * playerMoveSpeed / 1.414f);
                    // p.z -= GetMoveSpeed() * Time.deltaTime;
                }
                else if (_moveForward) _rgBody.AddForce(facing * playerMoveSpeed);
                else if (_moveBackward) _rgBody.AddForce(facing * -playerMoveSpeed);
                else if (_moveLeft) _rgBody.AddForce(right * -playerMoveSpeed);
                else if (_moveRight) _rgBody.AddForce(right * playerMoveSpeed);
            }

            // the code-made gravity
            _rgBody.AddForce(Vector3.down * gravitySpeed);
        }
    }
}
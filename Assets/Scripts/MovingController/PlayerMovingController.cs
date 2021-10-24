using UnityEngine;
using Managers;

namespace MovingController
{
    public class PlayerMovingController : AbstractMovingObject
    {
        // 玩家移动
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

        // keycodes
        private KeyCode _moveForwardKey;
        private KeyCode _moveBackwardKey;
        private KeyCode _moveLeftKey;
        private KeyCode _moveRightKey;
        private KeyCode _jumpKey;
        private KeyCode _dodgeKey;
        
        // 用于跳跃键后的地面检测
        private CapsuleCollider _capsCold;
        private float _capsColdRadius;
        private Vector3 _capsPointUp;
        private Vector3 _capsPointDown;
        public float overLapCapsuleOffset = 1.1f;

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
            _capsColdRadius = _capsCold.radius * 0.6f;
        }

        private void GetInputMove()
        {
            // 获取键盘输入
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
            GetInputMove();
            
            // 玩家跳跃，放fixedUpdate里会卡
            if (_playerJump)
            {
                // 玩家跳跃时地面检测
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
                    Debug.Log("player jump");
                    _rgBody.AddForce(Vector3.up * 1000f);

                }
            }

        }
        

        private void FixedUpdate()
        {
            // 玩家移动
            if (CanMove())
            {
                var p = _rgBody.position;
                var transform1 = transform;
                var facing = transform1.forward;
                var right = transform1.right;
                
                // 斜着走，x，y方向速度除根2
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

            // 重力
            _rgBody.AddForce(Vector3.down * gravitySpeed);
        }
    }
}
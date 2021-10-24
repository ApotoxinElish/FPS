using System;
using UnityEngine;

namespace MovingController
{
    public abstract class AbstractMovingObject : MonoBehaviour
    {
        // 可单独挂在对象上
        // 封装移动，跳跃信息
        
        private float _moveSpeed;  // 移动速度
        private float _moveSpeedBackup;  // 移动速度备份（用于重置）
        private bool _isMoveable;  // 能否移动flag

        private float _jumpSpeed;  // 跳跃速度
        private float _jumpSpeedBackup;  // 跳跃速度备份（用于重置）
        private bool _canJump = true;  // 能否跳跃（防止空中一直跳）
        

        protected void InitMoveSpeed(float speed)
        {
            // 初始化移动速度
            _moveSpeed = speed;
            _moveSpeedBackup = speed;
        }
        
        protected void InitJumpSpeed(float speed)
        {
            // 初始化跳跃速度
            _jumpSpeed = speed;
            _jumpSpeedBackup = speed;
        }

        public void ImmediateFreezeMoving()
        {
            _isMoveable = true;
        }

        public void UnfreezeMoving()
        {
            _isMoveable = true;
        }

        protected float GetMoveSpeed()
        {
            // 获取速度
            return _moveSpeed;
        }
        
        protected float GetJumpSpeed()
        {
            // 获取速度
            return _jumpSpeed;
        }


        public void ResetMoveSpeed()
        {
            // 重置速度
            _moveSpeed = _moveSpeedBackup;
        }
        
        public void ResetJumpSpeed()
        {
            // 重置速度
            _jumpSpeed = _jumpSpeedBackup;
        }
        
        public void ForceSetMoveSpeed(float speed)
        {
            // 强行设置速度
            _moveSpeed = speed;
        }
        
        public void ForceSetJumpSpeed(float speed)
        {
            // 强行设置速度
            _jumpSpeed = speed;
        }
        
        protected bool CanJump()
        {
            return _canJump;
        }

        public abstract bool CanMove();  // 判断能否移动，需子类完善

        protected void SetJumpFlag(bool flag)
        {
            _canJump = flag;
        }
    }
}
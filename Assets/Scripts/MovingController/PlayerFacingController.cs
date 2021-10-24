using System;
using UnityEngine;

namespace MovingController
{
    public class PlayerFacingController : MonoBehaviour
    {
        // 玩家朝向
        // private Vector3 _facingVector;
        // private Vector3 _facingOriVector = Vector3.forward;
        private bool _playerRotatable = true;  // 玩家是否能旋转
        public float playerRotateSpeed;  // 玩家旋转速度

        public GameObject aimObject;  // aim对象
        private readonly float[] _rotateRangeX = {-12f, 75f};
        private float _angleX = 20f;
        
        private void Update()
        {
            if (_playerRotatable)
            {
                // 没写反
                ChangeYRotationWithValue(Input.GetAxis("Mouse X") * playerRotateSpeed);
                _angleX += Input.GetAxis("Mouse Y") * -playerRotateSpeed;
                if (_angleX < _rotateRangeX[0]) _angleX = _rotateRangeX[0];
                else if (_angleX > _rotateRangeX[1]) _angleX = _rotateRangeX[1];
                ChangeXRotationToValue(_angleX);
            }
        }
        
        private void ChangeYRotationWithValue(float value)
        {
            // 玩家水品旋转
            var transform1 = transform;
            var transformRotation = transform1.rotation;
            var transformRotationEulerAngles = transformRotation.eulerAngles;
            transformRotationEulerAngles.y += value;
            transformRotation.eulerAngles = transformRotationEulerAngles;
            transform1.rotation = transformRotation;
        }
        
        private void ChangeXRotationToValue(float value)
        {
            // aim竖直旋转
            var transform1 = aimObject.transform;
            var transformRotation = transform1.rotation;
            var transformRotationEulerAngles = transformRotation.eulerAngles;
            transformRotationEulerAngles.x = value;
            transformRotation.eulerAngles = transformRotationEulerAngles;
            transform1.rotation = transformRotation;
        }
    }
}
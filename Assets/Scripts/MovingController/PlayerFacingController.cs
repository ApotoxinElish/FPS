using System;
using Cinemachine;
using UnityEngine;

namespace MovingController
{
    public class PlayerFacingController : MonoBehaviour
    {
        // player orientation
        // private Vector3 _facingVector;
        // private Vector3 _facingOriVector = Vector3.forward;
        private bool _playerRotatable = true;  // is the player rotatable
        private float _playerRotateSpeed;  // player rotation angular speed
        public float playerRotateSpeedOri;  // player rotation angular speed commonly (1.5)
        public float playerRotateSpeedLow;  // player rotation angular speed when scaling view filed (0.3)

        public GameObject aimObject;  // aim object
        public CinemachineVirtualCamera virtualCamera;  // virtual camera script
        
        private readonly float[] _rotateRangeX = {-83f, 73f};
        private float _angleX;
        
        private readonly float[] _viewFieldRange = {12f, 60f};  // change the field of view
        public float viewFieldChangeSpeed;
        private bool _viewFieldScaling;  // is changing the field of view

        private void Start()
        {
            _playerRotateSpeed = playerRotateSpeedOri;
        }

        private void Update()
        {
            if (_playerRotatable)
            {
                ChangeYRotationWithValue(Input.GetAxis("Mouse X") * _playerRotateSpeed);
                _angleX += Input.GetAxis("Mouse Y") * -_playerRotateSpeed;
                if (_angleX < _rotateRangeX[0]) _angleX = _rotateRangeX[0];
                else if (_angleX > _rotateRangeX[1]) _angleX = _rotateRangeX[1];
                ChangeXRotationToValue(_angleX);
            }

            // filed of view scale
            if (Input.GetMouseButtonDown(1))
            {
                _viewFieldScaling = true;
                _playerRotateSpeed = playerRotateSpeedLow;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                _viewFieldScaling = false;
                _playerRotateSpeed = playerRotateSpeedOri;
            }
          
            if (_viewFieldScaling)
            {
                // decrease field of view
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
                    _viewFieldRange[0],Time.deltaTime * viewFieldChangeSpeed);
            }
            else if (virtualCamera.m_Lens.FieldOfView < 59)
            {
                // field if view resumes
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView,
                    _viewFieldRange[1],Time.deltaTime * viewFieldChangeSpeed);
                Debug.Log(1);
            }
            
        }
        
        private void ChangeYRotationWithValue(float value)
        {
            // aim view turning horizontally
            var transform1 = transform;
            var transformRotation = transform1.rotation;
            var transformRotationEulerAngles = transformRotation.eulerAngles;
            transformRotationEulerAngles.y += value;
            transformRotation.eulerAngles = transformRotationEulerAngles;
            transform1.rotation = transformRotation;
        }
        
        private void ChangeXRotationToValue(float value)
        {
            // aim view turning vertically
            var transform1 = aimObject.transform;
            var transformRotation = transform1.rotation;
            var transformRotationEulerAngles = transformRotation.eulerAngles;
            transformRotationEulerAngles.x = value;
            transformRotation.eulerAngles = transformRotationEulerAngles;
            transform1.rotation = transformRotation;
        }
    }
}
using System;
using AbstractClass;
using Pathfinding;
using UnityEngine;

namespace Characters.MovingController
{
    public class EnemyMovingController : AbstractMovingObject
    {
        // A* path finding management
        public float moveSpeed;
        private Vector3 _targetPosition;

        private float _nextWayPointDistance = 0.1f;  // the distance os selecting the next way point
        private Path _path;  // the found path will be stored in it
        private int _currentWayPoint = 0;  // the indexof the surrent

        private Seeker _seeker;
        private Rigidbody _rgBody;

        private bool _startMoving;

        public float calculateNextPathInterval = 3;  // （安路径行走时）重新计算下一次路径的时间间隔
        private float _nextCalculatePathTime;  // 重新计算下一次路径的时间

        
        protected void Start()
        {
            InitMoveSpeed(moveSpeed);
            _seeker = GetComponent<Seeker>();
            _rgBody = GetComponent<Rigidbody>();
        }

        public void MoveToTarget(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _seeker.StartPath(_rgBody.position, targetPosition, OnPathComplete);  // start an A* path finding
        }

        private void OnPathComplete(Path path)
        {
            // the callback function of _seeker.StartPath()
            if (path.error) return;
            _path = path;
            _currentWayPoint = 0;
            _startMoving = true;
            _nextCalculatePathTime = Time.time + calculateNextPathInterval;  // 计算重新计算一次路径的时间
        }
        
        private void Update()
        {
            if (!_startMoving) return;
            
            // 时间到，重新计算一次路径
            if (Time.time > _nextCalculatePathTime)
            {
                MoveToTarget(_targetPosition);
                _nextCalculatePathTime = Time.time + calculateNextPathInterval;
            }
            
            if (_currentWayPoint >= _path.vectorPath.Count)
            {
                // reach the end of the path
                _startMoving = false;
                Debug.Log($"Enemy with id:{gameObject.GetInstanceID()} has moved to target.");
                return;
            }
        }

        private void FixedUpdate()
        {
            if (!_startMoving) return;
            
            // 算出方向的单位向量
            var wayPoint = _path.vectorPath[_currentWayPoint];
            var direction = (wayPoint - _rgBody.position).normalized;
            
            var force = direction * GetMoveSpeed() * Time.deltaTime;
            _rgBody.AddForce(force);
            
            if (Vector2.Distance(transform.position, wayPoint) < _nextWayPointDistance)
            {
                // 移动到离下个路径点很近（由nextWayPointDistance衡量）的位置
                _currentWayPoint++;
            }
        }

        public override bool CanMove()
        {
            return true;
        }
    }
}
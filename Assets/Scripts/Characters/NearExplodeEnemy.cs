﻿using System;
using AbstractClass;
using Characters.MovingController;
using UnityEngine;

namespace Characters
{
    public enum NearExplodeEnemyState
    {
        Roam = 0,
        Idle = 1,
        Chase = 2,
        Explode = 3,
    }

    public class NearExplodeEnemy : RangeCheckingEnemy
    {
        // the most common enemy, can be inherited

        public int hp;
        public string enemyName;
        public float enterRangeYDistance;
        public float exitRangeYDistance;
        public float exitRangeRadius;
        public GameObject explodeRangeChecker;
        public GameObject explodeRangeParticle;

        private EnemyMovingController _movingControllerScript;
        private NearExplodeEnemyState _state;
        private GameObject _chasingTarget;

        private Animator _animator;
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Chase = Animator.StringToHash("chase");
        private static readonly int Explode = Animator.StringToHash("explode");

        // debug
        float m_Theta = 0.1f;

        private void Start()
        {
            SetHp(hp);
            _state = NearExplodeEnemyState.Idle;
            _movingControllerScript = GetComponent<EnemyMovingController>();
            _animator = GetComponent<Animator>();
        }
        protected override void ZeroHpHandle()
        {
            Debug.Log($"Enemy with id {GetInstanceID()} retired with 0 hp");
            Destroy(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject otherObj = other.gameObject;
            if (otherObj.layer == LayerMask.NameToLayer("pBullet"))
            {
                Debug.Log("hit");
                AbstractBullet bulletScript = otherObj.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
                hp = hp - (int)bulletScript.damage;
                Destroy(otherObj);
            }
        }

        private void Update()
        {
            if (_state == NearExplodeEnemyState.Chase)
            {
                var position = transform.position;
                var targetPosition = _chasingTarget.transform.position;
                if (Math.Abs(targetPosition.y - position.y) > exitRangeYDistance) PlayerExit(_chasingTarget);
                else
                {
                    // var pos1 = new Vector2(position.x, position.z);
                    // var pos2 = new Vector2(targetPosition.x, targetPosition.z);
                    var distancePow = Math.Pow(position.x - targetPosition.x, 2) + Math.Pow(position.z - targetPosition.z, 2);
                    if (distancePow > Math.Pow(31, 2))
                    {
                        // Debug.Log(Vector2.Distance(pos1, pos2));
                        PlayerExit(_chasingTarget);
                    }
                }
            }
        }

        public override void PlayerEnterOuterRange(Collider player)
        {
            // explode
            _movingControllerScript.StopMoving();
            _state = NearExplodeEnemyState.Explode;
            _animator.SetTrigger(Explode);
        }

        public override void PlayerEnterInnerRange(Collider player)
        {
            if (!(_chasingTarget is null)) return;
            if (_state == NearExplodeEnemyState.Explode) return;
            if (Math.Abs(player.transform.position.y - transform.position.y) > enterRangeYDistance) return;
            _chasingTarget = player.gameObject;
            _movingControllerScript.EnableMoving();
            _movingControllerScript.MoveToTarget(_chasingTarget);
            _state = NearExplodeEnemyState.Chase;
            _animator.SetTrigger(Chase);
        }

        private void PlayerExit(GameObject obj)
        {
            if (obj.GetInstanceID() == _chasingTarget.GetInstanceID())
            {
                _chasingTarget = null;
                _state = NearExplodeEnemyState.Idle;
                _movingControllerScript.StopMoving();
                _animator.SetTrigger(Idle);
            }
        }

        // evoked by animator
        private void AddExplodeRangeChecker()
        {
            // generate the explosion effect
            var pos = transform.position;
            Instantiate(explodeRangeParticle, pos, Quaternion.identity);
            pos.y -= 1f;
            Instantiate(explodeRangeChecker, pos, Quaternion.identity);
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        //debug
        void OnDrawGizmos()
        {
            // 设置颜色
            Color defaultColor = Gizmos.color;
            Gizmos.color = Color.green;

            // 设置矩阵
            Matrix4x4 defaultMatrix = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;

            if (m_Theta < 0.0001f) m_Theta = 0.0001f;

            // 绘制圆环
            Vector3 beginPoint = Vector3.zero;
            Vector3 firstPoint = Vector3.zero;
            for (float theta = 0; theta < 2 * Mathf.PI; theta += m_Theta)
            {
                float x = exitRangeRadius * Mathf.Cos(theta);
                float z = exitRangeRadius * Mathf.Sin(theta);
                Vector3 endPoint = new Vector3(x, 0, z);
                if (theta == 0)
                {
                    firstPoint = endPoint;
                }
                else
                {
                    Gizmos.DrawLine(beginPoint, endPoint);
                }
                beginPoint = endPoint;
            }
            // 绘制最后一条线段
            Gizmos.DrawLine(firstPoint, beginPoint);

            // 恢复默认颜色
            Gizmos.color = defaultColor;

            // 恢复默认矩阵
            Gizmos.matrix = defaultMatrix;
        }

    }
}

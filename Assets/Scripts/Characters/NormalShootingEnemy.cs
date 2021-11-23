using System;
using System.Collections.Generic;
using Characters.MovingController;
using UnityEngine;
using Weapons.Concrete.Guns;

namespace Characters
{
    public enum NormalShootingState
    {
        Roam = 0,
        Idle = 1,
        Chase = 2,
        Shooting = 3,
        Die = 4,
    }

    public class NormalShootingEnemy : RangeCheckingEnemy
    {
        public string enemyName;
        public float enterRangeYDistance;
        public float exitRangeYDistance;
        public float exitRangeRadius;
        public float turningSpeed;
        public SlimeWeapon weapon;
        public Animator animator;
        public Collider enemyCollider;
        public GameObject slimeModel;
        public HudText hudText;

        private EnemyMovingController _movingControllerScript;
        private NormalShootingState _state;
        private List<GameObject> _chasingTargets;
        private GameObject _chasingTarget;

        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Chase = Animator.StringToHash("chase");
        private static readonly int Die = Animator.StringToHash("die");
        private static readonly int Attack = Animator.StringToHash("attack");

        // private RaycastHit _hit;
        // private Ray _ray;
        

        // debug
        float m_Theta = 0.1f;

        private void Start()
        {
            _state = NormalShootingState.Idle;
            _movingControllerScript = GetComponent<EnemyMovingController>();
            _chasingTargets = new List<GameObject>();
        }
        protected override void ZeroHpHandle()
        {
            _movingControllerScript.StopMoving();
            animator.SetTrigger(Die);
            Invoke("SelfDestroy", 3f);
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject otherObj = other.gameObject;
            if (otherObj.layer == LayerMask.NameToLayer("pBullet"))
            {
                AbstractBullet bulletScript = otherObj.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
                var damage = (int) bulletScript.damage;
                Hurt(damage);
                hudText.HUD(damage);
            }
        }
        
        private bool HasObjBetweenTwoPosition(Vector3 from, Vector3 to)
        {
            Debug.DrawLine(from, to);
            //发射射线长度为100
            enemyCollider.enabled = false;
            var ret = Physics.Linecast(from, to, 1 << 6 | 1 << 11);
            enemyCollider.enabled = true;
            if (ret)
            {
                Debug.Log("blocked:");
            }
            return ret;
        }

        private void Update()
        {
            if (_state == NormalShootingState.Shooting)
            {
                // var pos = transform.position;
                // pos.y += 0.5f;
                // if (HasObjBetweenTwoPosition(pos, _chasingTarget.transform.position))
                // {
                //     _state = NormalShootingState.Chase;
                //     animator.SetTrigger(Chase);
                //     _movingControllerScript.MoveToTarget(_chasingTarget);
                // }

                Debug.Log(weapon.CanShoot());
                if (weapon.CanShoot())
                {
                    animator.SetTrigger(Attack);
                    weapon.Shoot(_chasingTarget.transform);
                }

                // slowly facing to target
                var position1 = slimeModel.transform.position;
                var position2 = _chasingTarget.transform.position;
                Quaternion quaternion = Quaternion.LookRotation(position2 - position1);
                transform.rotation = Quaternion.Lerp(slimeModel.transform.rotation, quaternion, turningSpeed * Time.deltaTime);
            }
            else if (_state == NormalShootingState.Chase)
            {
                // var pos = transform.position;
                // pos.y += 0.5f;
                // if (!HasObjBetweenTwoPosition(pos, _chasingTarget.transform.position))
                // {
                //     _state = NormalShootingState.Shooting;
                //     _movingControllerScript.StopMoving();
                // }
            }
            
            // check whether player leaves range
            var position = slimeModel.transform.position;
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

        public override void PlayerEnterInnerRange(Collider player)
        {
            if (_chasingTargets.Count == 4) return;
            if (_state == NormalShootingState.Die) return;
            if (Math.Abs(player.transform.position.y - transform.position.y) > enterRangeYDistance) return;
            
            _chasingTargets.Add(player.gameObject);
            if (_chasingTarget == null)
            {
                _chasingTarget = player.gameObject;
                _movingControllerScript.EnableMoving();
                _state = NormalShootingState.Chase;
                animator.SetTrigger(Chase);
                // _state = NormalShootingState.Shooting;
                // animator.SetTrigger(Attack);
                _movingControllerScript.MoveToTarget(_chasingTarget);
            }
        }

        private void PlayerExit(GameObject obj)
        {
            if (_chasingTargets.Contains(obj)) _chasingTargets.Remove(obj);
            if (obj.GetInstanceID() == _chasingTarget.GetInstanceID())
            {
                if (_chasingTargets.Count > 0)
                {
                    // chase the last entered player
                    _chasingTarget = _chasingTargets[_chasingTargets.Count - 1];
                }
                else
                {
                    // no player in target array, set trigger Idle
                    _chasingTarget = null;
                    _state = NormalShootingState.Idle;
                    _movingControllerScript.StopMoving();
                    animator.SetTrigger(Idle);
                }
            }
        }

        // evoked by animator
        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

    }
}

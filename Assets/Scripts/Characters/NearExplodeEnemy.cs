using AbstractClass;
using UnityEngine;

namespace Characters
{
    public enum NearExplodeEnemyState
    {
        Roam = 0,
        Idle = 1,
        Chase = 2,
    }
    
    public class NearExplodeEnemy : RangeCheckingEnemy
    {
        // the most common enemy, can be inherited

        public int hp;
        public string enemyName;

        private Transform _chasingTarget;

        private void Start()
        {
            SetHp(hp);
        }
        protected override void ZeroHpHandle()
        {
            Debug.Log($"Enemy with id {GetInstanceID()} retired with 0 hp");
            Destroy(this);
        }

        private void OnTriggerEnter(Collider other) {
            GameObject otherObj=other.gameObject;
            if(otherObj.layer==LayerMask.NameToLayer("pBullet")){
                Debug.Log("hit");
                AbstractBullet bulletScript=otherObj.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
                hp=hp-(int)bulletScript.damage;
                Destroy(otherObj);
            }
        }

        public override void PlayerEnterInnerRange(Collision player)
        {
            throw new System.NotImplementedException();
        }

        public override void PlayerEnterOtterRange(Collision player)
        {
            throw new System.NotImplementedException();
        }
    }
}

using AbstractClass;
using UnityEngine;

namespace Character
{
    public class CommonEnemy : AbstractHpObject
    {
        // the most common enemy, can be inherited

        public int hp;
        public string enemyName;

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
    }
}

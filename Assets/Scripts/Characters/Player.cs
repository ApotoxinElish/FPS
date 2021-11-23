using System;
using AbstractClass;
using UnityEngine;

namespace Character
{
    public class Player : AbstractHpObject
    {
        public string playerName;

        public GameObject weaponHolder;

        private void Start()
        {
            SetHp(hp);
        }
        protected override void ZeroHpHandle()
        {
            Debug.Log("Player retires with 0 hp");
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject otherObj = other.gameObject;
            if (otherObj.layer == LayerMask.NameToLayer("eBullet"))
            {
                // Debug.Log("hit");
                var bulletScript = otherObj.GetComponent(typeof(AbstractBullet)) as AbstractBullet;
                Hurt((int)bulletScript.damage);
            }
        }
    }
}

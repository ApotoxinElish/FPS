using System;
using UnityEngine;

namespace Weapons.Concrete.Bullets
{
    public class SlimeBullet : AbstractBullet
    {
        public Rigidbody rgBody;

        private void Awake()
        {
            rgBody = GetComponent<Rigidbody>();
        }
        
        public void SetVelocity(Vector3 Direction, float Speed)
        {
            rgBody.velocity = Direction * Speed;
        }
    }
}
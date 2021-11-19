using System;
using UnityEngine;

namespace Scripts.Weapon
{
    public abstract class Firearms : MonoBehaviour, IWeapon
    {

        public Transform MuzzlePoint;
        public Transform CasingPoint;

        public ParticleSystem MuzzleParticle;
        public ParticleSystem CasingParticle;

        public float FireRate;

        public int AmmoInMag = 30;
        public int MaxAmmoCarried = 120;


        protected int CurrentAmmo;
        protected int CurrentMaxAmmoCarried;
        private float lastFireTime;
        protected Animator GunAnimator;


        protected virtual void Start()
        {
            CurrentAmmo = AmmoInMag;
            CurrentMaxAmmoCarried = MaxAmmoCarried;
        }


        public void DoAttack()
        {
            if (CurrentAmmo <= 0) return;
            CurrentAmmo -= 1;
            Shooting();
        }


        protected abstract void Shooting();
        protected abstract void Reload();


        private bool IsAllowShooting()
        {
            return Time.time - lastFireTime > 1 / FireRate;
        }
    }
}

using UnityEngine;
using UnityEngine.WSA;
using Weapons.Concrete.Bullets;

namespace Weapons.Concrete.Guns
{
    public class SlimeWeapon : MonoBehaviour
    {
        public float cd;
        public GameObject slimeBullet;
        public float damage;
        public float bulletSpeed;

        private float _nextShootingTime;
        private bool _canShoot = true;

        public void Shoot(Transform target)
        {
            Invoke(nameof(ShootBullet), 0.08f);
            _nextShootingTime = Time.time + cd;
            _canShoot = false;
        }

        private void ShootBullet()
        {
            var transform1 = transform;
            var currentBullet = Instantiate(slimeBullet, transform1.position, Quaternion.identity);
            var bulletScript = currentBullet.GetComponent<SlimeBullet>();
            bulletScript.damage = damage;
            bulletScript.direction = transform.rotation * Vector3.forward;
            bulletScript.speed = bulletSpeed;
            bulletScript.SetVelocity(transform.rotation * Vector3.forward, bulletSpeed);
        }
        
        private void Update()
        {
            if (_canShoot) return;
            if (Time.time > _nextShootingTime)
            {
                _canShoot = true;
            }
        }

        public bool CanShoot()
        {
            return _canShoot;
        }
    }
}
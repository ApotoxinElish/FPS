using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class BaseBullet : MonoBehaviour
    {
        public float _damage;
        public float _speed;
        public Vector3 _direction;
        private Rigidbody _rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("create a bullet");
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.velocity = _direction * _speed;
            Debug.Log($"current speed:{_rigidbody.velocity}");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

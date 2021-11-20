using System;
using System.Collections.Generic;
using Character;
using MovingController;
using TMPro;
using UnityEngine;

namespace Characters.RangeChecker
{
    public class ExplodeRangeChecker : MonoBehaviour
    {
        public float explodeStrength;

        private List<int> _effectedObjInstanceIds;

        private void Start()
        {
            _effectedObjInstanceIds = new List<int>();
            Invoke(nameof(SelfDestroy), 1f);   // disappear in 1 sec
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public void SetExplodeStrength(float strength)
        {
            explodeStrength = strength;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;  // only take effect to players
            
            var instanceId = other.GetInstanceID();
            if (_effectedObjInstanceIds.Contains(instanceId)) return;  // each player can only be add such effect once
            _effectedObjInstanceIds.Add(instanceId);

            var playerScript = other.GetComponent<Player>();
            var playerControlScript = other.GetComponent<PlayerMovingController>();
            
            // hurt value is a quarter of (11 - distance)^2 
            // the max value of _sphereCollider.radius is 15
            var position = transform.position;
            var playerRgBodyPos = playerControlScript.GetRgBodyPos();
            var distance = Vector3.Distance(playerRgBodyPos, position);
            playerScript.Hurt((int)(Math.Pow(16 - distance, 2) * 0.25));

            var direction = (playerRgBodyPos - position).normalized;
            playerControlScript.PassiveAddForce(direction * (16 - distance) * explodeStrength);
        }
    }
}
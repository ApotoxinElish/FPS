using System;
using AbstractClass;
using UnityEngine;

namespace Characters.RangeChecker
{
    public class InnerSphereRangeChecker : MonoBehaviour
    {
        public RangeCheckingEnemy enemyObj;
        
        private void OnCollisionEnter(Collision other)
        {
            enemyObj.PlayerEnterInnerRange(other);
        }
    }
}
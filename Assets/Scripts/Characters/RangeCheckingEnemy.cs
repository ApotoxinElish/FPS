using AbstractClass;
using UnityEngine;

namespace Characters
{
    public abstract class RangeCheckingEnemy : AbstractHpObject
    {
        // Enemy that has ranges to check whether player is in range
        
        public abstract void PlayerEnterInnerRange(Collision player);
        public abstract void PlayerEnterOtterRange(Collision player);
    }
}
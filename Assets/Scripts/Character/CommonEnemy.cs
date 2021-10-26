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
        }
    }
}
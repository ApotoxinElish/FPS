using AbstractClass;
using UnityEngine;

namespace Character
{
    public class Player : AbstractHpObject
    {
        public int hp;
        public string playerName;

        private void Start()
        {
            SetHp(hp);
        }
        protected override void ZeroHpHandle()
        {
            Debug.Log("Player retires with 0 hp");
        }
    }
}
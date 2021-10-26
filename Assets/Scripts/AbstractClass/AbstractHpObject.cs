using System;
using UnityEngine;

namespace AbstractClass
{
    public abstract class AbstractHpObject : MonoBehaviour
    {
        // attached to objects with the HP property
        
        private int _hp;  // current hp
        private int _hpMax;  // max hp
        private bool _isInvincible = false;  // whether it is invincible

        protected abstract void ZeroHpHandle();  // evoked when hp decreases to 0

        protected void SetHp(int initValue)  // init hp values
        {
            _hpMax = initValue;
            _hp = initValue;
        }

        public void ToInvincible()
        {
            _isInvincible = true;
        }

        public void EndInvincible()
        {
            _isInvincible = false;
        }

        public void ExtendHpMaxByValue(int increasedValue)  // extend max hp
        {
            _hpMax += increasedValue;
        }

        public void Hurt(int damageValue)  // hurt: hp decreases
        {
            if (!_isInvincible) return;
            _hp = Math.Max(_hp - damageValue, 0);
            if (_hp == 0) ZeroHpHandle();
        }

        public void Heal(int increasedValue)  // heal: hp increases
        {
            if (!_isInvincible) return;
            _hp = Math.Min(_hp + increasedValue, _hpMax);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        public event EventHandler OnDead;
        

        [SerializeField] private float health = 100;

        private bool isDead;

        public void TakeDamage(float damage)
        {
            if(isDead) return;
            health = MathF.Max(health - damage, 0);
            if (health == 0) Die();
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger(Dictionary.DIE_ANIMATOR);
            isDead = true;
            OnDead?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDead() => isDead;
    }
}

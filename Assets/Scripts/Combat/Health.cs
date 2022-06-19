using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 20f;

        Animator animator;
        bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            isDead = false;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0f);

            if (healthPoints == 0f)
            {
                if (isDead) return;
                isDead = true;
                animator.SetTrigger("die");
            }
        }
    }
}
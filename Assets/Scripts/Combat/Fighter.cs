using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Health target;

        float timeSinceLastAttack;
        Mover moverComponent;
        Animator animator;

        private void Awake()
        {
            timeSinceLastAttack = 0f;
            moverComponent = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (isInWeaponRange(target.transform))
            {
                moverComponent.Cancel();
                AttackBehaviour();
            }
            else
            {
                moverComponent.MoveTo(target.transform.position);
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                animator.SetTrigger("attack");  //Will call Hit() method by itself.
                timeSinceLastAttack = 0f;
            }
        }

        //Animation Event
        private void Hit()
        {
            target.TakeDamage(weaponDamage);
        }

        private bool isInWeaponRange(Transform combatTarget)
        {
            float distanceBetween = Vector3.Distance(transform.position, combatTarget.position);
            if (distanceBetween <= weaponRange)
            {
                return true;
            }
            return false;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }
    }
}
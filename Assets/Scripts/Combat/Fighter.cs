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

        Transform target;

        float timeSinceLastAttack;
        Mover moverComponent;

        private void Awake()
        {
            timeSinceLastAttack = 0f;
            moverComponent = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null)
            {
                if (isInWeaponRange(target))
                {
                    moverComponent.Cancel();
                    AttackBehaviour();
                }
                else
                {
                    moverComponent.MoveTo(target.position);
                }
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");  //Will call Hit() method by itself.
                timeSinceLastAttack = 0f;
            }
        }

        //Animation Event
        private void Hit()
        {
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(weaponDamage);
            }
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
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
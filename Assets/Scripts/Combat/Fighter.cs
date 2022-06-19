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

        Transform target;
        Mover moverComponent;

        private void Awake()
        {
            moverComponent = GetComponent<Mover>();
        }

        private void Update()
        {
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
            GetComponent<Animator>().SetTrigger("attack");
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

        //Animation Event
        private void Hit()
        {

        }
    }
}
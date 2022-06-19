using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Transform target; 

        private void Update()
        {
            if (target != null)
            {
                if (isInWeaponRange(target))
                {
                    GetComponent<Mover>().Stop();
                }
                else
                {
                    GetComponent<Mover>().MoveTo(target.position);
                }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 3f;

        GameObject playerObject;
        ActionScheduler actionScheduler;
        Fighter AIFighter;
        Health health;
        Mover mover;

        Vector3 guardingPosition;
        float timeSinceLastSeenPlayer;


        private void Awake()
        {
            AIFighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardingPosition = transform.position;
            timeSinceLastSeenPlayer = Mathf.Infinity; ;
        }

        private void Start()
        {
            playerObject = GameObject.FindWithTag("Player");
            actionScheduler = GetComponent<ActionScheduler>();
        }


        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && AIFighter.CanAttack(playerObject))
            {
                timeSinceLastSeenPlayer = 0;
                AttackState();
            }
            else if (!InAttackRange() && timeSinceLastSeenPlayer < suspiciousTime)
            {
                SuspicionState();
            }
            else
            {
                GuardState();
            }

            timeSinceLastSeenPlayer += Time.deltaTime;
        }

        private void SuspicionState()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void GuardState()
        {
            mover.StartMoveAction(guardingPosition);
        }

        private void AttackState()
        {
            AIFighter.Attack(playerObject);
        }

        public bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(playerObject.transform.position, transform.position);
            return chaseDistance >= distanceToPlayer;
        }

        //Called by Unity internally
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
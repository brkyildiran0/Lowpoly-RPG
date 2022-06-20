using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float dwellTime = 5f;

        GameObject playerObject;
        ActionScheduler actionScheduler;
        Fighter AIFighter;
        Health health;
        Mover mover;

        Vector3 guardingPosition;
        float timeSinceLastSeenPlayer;
        float timeSinceLastDwell;
        int currentWaypointIndex;


        private void Awake()
        {
            AIFighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardingPosition = transform.position;
            timeSinceLastSeenPlayer = Mathf.Infinity;
            currentWaypointIndex = 0;
            timeSinceLastDwell = Mathf.Infinity;
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
                AttackState();
            }
            else if (!InAttackRange() && timeSinceLastSeenPlayer < suspiciousTime)
            {
                SuspicionState();
            }
            else
            {
                PatrolState();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastDwell += Time.deltaTime;
            timeSinceLastSeenPlayer += Time.deltaTime;
        }

        private void SuspicionState()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void PatrolState()
        {
            mover.SetSpeed(3.5f);
            Vector3 nextPosition = guardingPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceLastDwell > dwellTime)
            {
                mover.StartMoveAction(nextPosition);
                timeSinceLastDwell = 0;
            }
        }

        private void AttackState()
        {
            mover.SetSpeed(4.5f);
            timeSinceLastSeenPlayer = 0;
            AIFighter.Attack(playerObject);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject playerObject;
        Fighter AIFighter;
        Health health;

        private void Awake()
        {
            AIFighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            playerObject = GameObject.FindWithTag("Player");
        }


        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && AIFighter.CanAttack(playerObject))
            {
                AIFighter.Attack(playerObject);
            }
            else
            {
                AIFighter.Cancel();
            }
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
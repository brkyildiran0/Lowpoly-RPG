using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject playerObject;
        Fighter AIFighter;

        private void Awake()
        {
            AIFighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            playerObject = GameObject.FindWithTag("Player");
        }


        private void Update()
        {
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
    }
}
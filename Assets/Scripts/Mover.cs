using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIMovment : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    void Start() // Start is called before first frame update
    {
        agent = GetComponent<NavMeshAgent>(); 
        agent.updateRotation = false; // Falsefying the update on rotation to allow agent to rotate freely
        agent.updateUpAxis = false;
    }

    void Update() // Update is called once per frame
    {
        agent.SetDestination(target.position);
    }
}

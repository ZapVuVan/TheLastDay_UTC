using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMesh : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void Update()
    {
        agent.destination = target.position;
    }
}

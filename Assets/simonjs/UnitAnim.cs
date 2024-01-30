using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAnim : MonoBehaviour
{
    public Animator anim;
    public GameObject unit;
    private NavMeshAgent agent;
    private void Start()
    {
        unit.GetComponent<UnitScript>().animScript = this;
        anim = GetComponent<Animator>();
        agent = unit.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        bool isRunning = agent.hasPath && Vector3.Distance(agent.destination, agent.transform.position) > agent.stoppingDistance;
        anim.SetBool("Run", isRunning);
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
 
}

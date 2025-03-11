using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour_Far : FSM_Base_Far
{
    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo,layerIndex);
        timer = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points_far").transform;
        foreach (Transform t in pointsObject) 
            points.Add(t);

        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(points[0].position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        
        timer += Time.deltaTime;
        if(timer > 20) 
            animator.SetBool("isPatrolling", false);

        
        float distance = Vector3.Distance(animator.transform.position, player.transform.position);

        if (distance < aggroRange)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
    
}

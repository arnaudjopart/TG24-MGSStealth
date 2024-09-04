using ajc.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

internal class PatrolStrategy : IStrategy
{
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private int _index;

    public PatrolStrategy(NavMeshAgent agent, Transform[] waypoints)
    {
        this.agent = agent;
        this.waypoints = waypoints;
    }

    public Node.STATUS Process(float _deltaTime)
    {
        agent.SetDestination(waypoints[_index].position);
        if(Vector3.SqrMagnitude(agent.transform.position - waypoints[_index].position) < 1f) 
        {
            _index++;
            if(_index >= waypoints.Length) { 
                
                _index = 0;
                return  Node.STATUS.Success; }
           
        }
        return Node.STATUS.Running;
    }
}
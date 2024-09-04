using ajc.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

internal class ChaseTargetStrategy : IStrategy
{
    private readonly NavMeshAgent _agent;
    private readonly Transform _target;

    public ChaseTargetStrategy(NavMeshAgent agent, Transform target)
    {
        _agent = agent;
        _target = target;
    }
    public Node.STATUS Process(float _deltaTime)
    {
        _agent.SetDestination(_target.position);
        if (Vector3.SqrMagnitude(_target.position - _agent.transform.position)<1) return Node.STATUS.Success;
        return Node.STATUS.Running;
    }
}
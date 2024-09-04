using ajc.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTreeController : MonoBehaviour
{
    private BehaviourTree _behaviourTree;

    [SerializeField] private TargetDetector _detector;
    [SerializeField] private Player _player;
    private NavMeshAgent _agent;
    [SerializeField] Transform[] _waypoints;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _behaviourTree = new BehaviourTree();

        var selector = new ExtendedSelector("Patrolling");

        selector.AddNode(new Leaf("Check if player is Detected", new CheckTargetDetectionStrategy(_detector, _player)));
        selector.AddNode(new Leaf("Patrolling", new PatrolStrategy(_agent, _waypoints)));


        var chasingSequence = new SequenceNode("In pursuit");
        chasingSequence.AddNode(new Leaf("test", new ActionStrategy(()=> { _agent.isStopped = true; })));
        chasingSequence.AddNode(new Leaf("Aiming", new ReactToDetectionStrategy(_detector, _player)));
        chasingSequence.AddNode(new Leaf("test", new ActionStrategy(() => { _agent.isStopped = false; })));
        chasingSequence.AddNode(new Leaf("Chasing Player", new ChaseTargetStrategy(_agent, _player.transform)));
        //chasingSequence.AddNode(new Leaf("Chasing Player", new AttackTargetStrategy(_agent, _player.transform)));

        _behaviourTree.AddNode(selector);
        _behaviourTree.AddNode(chasingSequence);
    }

    // Update is called once per frame
    void Update()
    {
        _behaviourTree.Tick(Time.deltaTime);
        _animator.SetFloat("speed", Mathf.Clamp01(_agent.velocity.magnitude));
    }
}

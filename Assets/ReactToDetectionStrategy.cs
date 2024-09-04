using ajc.BehaviourTree;
using UnityEngine;


internal class ReactToDetectionStrategy : IStrategy
{
    private float _timer;
    private TargetDetector detector;
    private Player player;

    public ReactToDetectionStrategy(TargetDetector detector, Player player)
    {
        this.detector = detector;
        this.player = player;
    }

    public Node.STATUS Process(float _deltaTime)
    {
        detector.ReactToDetection();
        var direction = player.transform.position - detector.transform.position;
        detector.transform.rotation = Quaternion.LookRotation(direction.normalized);
        _timer += _deltaTime;
        if (_timer > 2f) 
        {
            _timer = 0;
            return Node.STATUS.Success; 
        }
        return Node.STATUS.Running;
    }
}
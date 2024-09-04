using ajc.BehaviourTree;

internal class CheckTargetDetectionStrategy : IStrategy
{
    public CheckTargetDetectionStrategy(TargetDetector detector, Player player)
    {
        _detector = detector;
        _player = player;
    }

    private readonly TargetDetector _detector;
    private readonly Player _player;

    public Node.STATUS Process(float _deltaTime)
    {
        return _detector.CheckPlayerDetection(_player)? Node.STATUS.Success:Node.STATUS.Failure;
    }
}
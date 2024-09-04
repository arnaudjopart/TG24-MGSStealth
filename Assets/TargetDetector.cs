using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private float _detectionDistance;
    public bool CheckPlayerDetection(Player player)
    {
        var playerDirection = player.transform.position - transform.position;
        var distanceSqr = Vector3.SqrMagnitude(playerDirection);
        var inSight = Vector3.Dot(playerDirection.normalized, transform.forward)>.8f;
        return distanceSqr < _detectionDistance * _detectionDistance && inSight;
    }

    internal void ReactToDetection()
    {
        GetComponent<Animator>().SetTrigger("DetectTrigger");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<MGSInputController>();
        _characterController.OnLeaveCoverEvent += LeaveCover;
    }

    private void LeaveCover()
    {
        _leftCoverDetector.enabled = false;
        _rightCoverDetector.enabled = false;
        _state = State.Default;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.Default:

                var ray = new Ray(transform.position + _raycastStartOffset, transform.forward);
                if (Physics.Raycast(ray, out var hit, _maxDistance, _layerMask))
                {
                    
                    _timer += Time.deltaTime;
                    if (_timer > _getCoverTimer)
                    {
                        _characterController.SetCover(hit.normal);
                        _state = State.InCover;
                        _leftCoverDetector.enabled = true;
                        _rightCoverDetector.enabled = true;
                    }
                }
                else
                {
                    _timer = 0;
                }
                Debug.DrawLine(ray.origin, ray.GetPoint(_maxDistance));


                break;
            case State.InCover:
                _characterController.ReactToEndCoverLeft(_leftCoverDetector.HasCover);
                _characterController.ReactToEndCoverRight(_rightCoverDetector.HasCover);

                
                break;
        }
        
    }

    private enum State
    {
        Default,
        InCover
    }
    private State _state;
    [SerializeField] private Vector3 _raycastStartOffset;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;
    private float _timer;
    private MGSInputController _characterController;
    [SerializeField] private float _getCoverTimer =.8f;
    [SerializeField] private EndOfCoverDetection _leftCoverDetector;
    [SerializeField] private EndOfCoverDetection _rightCoverDetector;
}

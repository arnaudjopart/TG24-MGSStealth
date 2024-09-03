using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _target.transform.position + _offset;
    }

    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;
}

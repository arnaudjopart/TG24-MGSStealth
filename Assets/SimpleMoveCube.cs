using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveCube : MonoBehaviour
{
    private Transform _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var inputVector = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        var forwardCamera = _camera.forward;
        var rightCamera = _camera.right;

        var forwardOnXZPlane = new Vector3(forwardCamera.x,0,forwardCamera.z);
        var rightOnXZPlane = Vector3.ProjectOnPlane(rightCamera, Vector3.up).normalized;

        var movement = inputVector.x*rightOnXZPlane+inputVector.z*forwardOnXZPlane;

        
        transform.position += movement * 5*Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CharacterInputController : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        //_animator.SetFloat("inputXFloat", inputX);
        _animator.SetFloat("inputYFloat", inputY);

        if (Mathf.Abs(inputY) > .1f)
        { 
            var characterForwardBasedOnCamera = 
                Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
            transform.rotation = Quaternion.LookRotation(characterForwardBasedOnCamera);
            
        }

        var move = transform.TransformDirection(new Vector3(inputX,0, inputY));
        //var move = new Vector3(inputX, 0, inputY);
        _characterController.SimpleMove(move * _moveSpeed);
    }

    private Transform _cameraTransform;
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField] private float _moveSpeed;
}



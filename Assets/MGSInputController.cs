using System;
using UnityEngine;

public class MGSInputController : MonoBehaviour
{

    enum State
    {
        Walking,
        Cover
    }
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
        HandleMovement();
        HandleInteraction();

        
    }

    private void HandleInteraction()
    {
        if (_currentInteractable != null)
        {
            if (Input.GetKeyDown(_currentInteractable.ActionKey))
            {
                Debug.Log("Action");
                _currentInteractable.Interact();
            }
        }

    }

    private void HandleMovement()
    {
        var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputVector.sqrMagnitude > 1) inputVector = inputVector.normalized;

        //Apply input to movement Axis
        var move = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isCrouched = !_animator.GetBool("Crouch");
            _animator.SetBool("Crouch", _isCrouched);
        }

        switch (_currentState)
        {
            case State.Walking:


                if (inputVector.sqrMagnitude < .35f)
                {
                    //Debug.Log(inputVector.sqrMagnitude);
                    _overrideInputUntilNextInputPose = false;
                }

                if (!_overrideInputUntilNextInputPose)
                {
                    _cameraForwardOnXZ = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
                    _cameraRightOnXZ = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;
                }

                move = _cameraForwardOnXZ * inputVector.y + _cameraRightOnXZ * inputVector.x;

                _animator.SetFloat("inputYFloat", inputVector.y);

                if (Mathf.Abs(inputVector.y) > .1f)
                {
                    var characterForwardBasedOnCamera =
                        Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
                    transform.rotation = Quaternion.LookRotation(characterForwardBasedOnCamera);
                }

                _currentMoveSpeed = _walkingSpeed;
                _animator.SetFloat("inputYFloat", inputVector.magnitude);
                if (inputVector.sqrMagnitude > .1f) transform.rotation = Quaternion.LookRotation(move);

                break;
            case State.Cover:
                _mainCoverCamera.SetActive(true);
                //Reset Input axis if no input
                if (inputVector.sqrMagnitude < .1f)
                {
                    //Convert Input based on camera orientation
                    _cameraForwardOnXZ = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
                    _cameraRightOnXZ = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;
                }

                move = _cameraForwardOnXZ * inputVector.y + _cameraRightOnXZ * inputVector.x;

                _currentMoveSpeed = _sneakingSpeed;

                if (Vector3.Dot(_leaveCoverDirection, move) > .5f)
                {
                    _animator.SetBool("Sneak", false);
                    _currentState = State.Walking;
                    m_leftCoverCamera.SetActive(false);
                    m_rightCoverCamera.SetActive(false);
                    _mainCoverCamera.SetActive(false);
                    _overrideInputUntilNextInputPose = true;
                    OnLeaveCoverEvent?.Invoke();
                    return;
                }

                move = Vector3.ProjectOnPlane(move, _leaveCoverDirection);
                if (_limitMovesOnLeft)
                {
                    if (Vector3.Dot(move, transform.right) < 0) move = Vector3.zero;
                }
                if (_limitMovesOnRight)
                {
                    if (Vector3.Dot(move, transform.right) > 0) move = Vector3.zero;
                }

                var moveDirection = Vector3.Dot(move, transform.right);
                _animator.SetFloat("inputYFloat", moveDirection);

                break;
        }
        _currentMoveSpeed = _isCrouched ? _sneakingSpeed :_walkingSpeed;
        _characterController.SimpleMove(move * _currentMoveSpeed);
    }

    internal void SetCover(Vector3 _coverNormal)
    {
        _animator.SetBool("Sneak", true);
        transform.rotation = Quaternion.LookRotation(_coverNormal);
        _leaveCoverDirection = _coverNormal;
       _currentState = State.Cover;
    }

    internal void ReactToEndCoverLeft(bool hasCover)
    {
        _limitMovesOnLeft = !hasCover;
        m_leftCoverCamera.SetActive(!hasCover);
    }

    internal void ReactToEndCoverRight(bool hasCover)
    {
        _limitMovesOnRight = !hasCover;
        m_rightCoverCamera.SetActive(!hasCover);
    }

    public void SetInteractable(IInteractable interactable)
    {
        _currentInteractable = interactable;
        _currentInteractable.InitInteraction();
    }

    public void ReleaseInteractable()
    {
        if (_currentInteractable!=null)
        {
            _currentInteractable.CancelInteraction();
        }
        _currentInteractable = null;
    }

    private Transform _cameraTransform;
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _sneakingSpeed=1;
    private Vector3 _leaveCoverDirection;
    private State _currentState;
    private float _currentMoveSpeed;
    [SerializeField] private GameObject m_leftCoverCamera;
    [SerializeField] private GameObject m_rightCoverCamera;
    private Vector3 _cameraForwardOnXZ;
    private Vector3 _cameraRightOnXZ;
    private bool _limitMovesOnRight;
    private bool _limitMovesOnLeft;
    [SerializeField] private GameObject _mainCoverCamera;
    private bool _overrideInputUntilNextInputPose;
    private IInteractable _currentInteractable;
    private bool _isCrouched;

    public event Action OnLeaveCoverEvent;
}

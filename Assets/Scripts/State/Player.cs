using UnityEngine;
using BurgerBoy;

public class Player : StateMachine
{
    // Components
    public PlayerInput Input { get; private set; }

    private CharacterController _characterController;

    public Animator Animator { get; private set; }

    public Targeter Targeter { get; private set; }

    // Camera Access
    private Transform _mainCameraTransform;

    // Knobs
    [SerializeField]
    private float _runSpeed = 10f;

    [SerializeField]
    private float _faceDampening = .1f;

    [SerializeField]
    private float _animationDampTime = 25f;

    private float _currentGravity = 0f;


    private void Awake() {
        Input = GetComponent<PlayerInput>();

        _characterController = GetComponent<CharacterController>();

        Animator = GetComponent<Animator>();
        Targeter = GetComponentInChildren<Targeter>();

        _mainCameraTransform = Camera.main.transform;
    }

    private void Start() {
        _globalState = new PlayerGlobalState(this);
        SwitchState(new PlayerFreeLookState(this));
    }

    public void ApplyGravity() {
        if (_characterController.isGrounded) {
            _currentGravity = 0;
        } else {
            _currentGravity += Constants.GRAVITY;
            _characterController.Move(new Vector3(0, _currentGravity, 0) * Time.deltaTime);
        }
    }

    public void Move() {
        bool isMoving = Input.MovementVector != Vector2.zero;

        if (isMoving) {
            PerformMovement();
        }

        BlendMovementAnimation(isMoving);
    }

    private void PerformMovement() {
            Vector3 movement = CalculateMovement();

            _characterController.Move(movement * Time.deltaTime * _runSpeed);

        FaceMovementDirection(movement);
    }

    private Vector3 CalculateMovement() {
        Vector3 cameraForward = _mainCameraTransform.forward;
        Vector3 cameraRight = _mainCameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * Input.MovementVector.y + cameraRight * Input.MovementVector.x;
    }

    private void FaceMovementDirection(Vector3 movement) {
        transform.rotation = Quaternion.Lerp(
           transform.rotation,
           Quaternion.LookRotation(movement),
           Time.deltaTime * _faceDampening
        );
    }

    private void BlendMovementAnimation(bool isMoving) {
        Animator.SetFloat("IdleRunBlend", isMoving ? 1 : 0, _animationDampTime, Time.deltaTime);
    }

}

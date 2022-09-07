using UnityEngine;

public class Player : StateMachine
{
    public PlayerInput Input { get; private set; }

    private CharacterController _characterController;

    private Animator _animator;

    private Transform _mainCameraTransform;

    [SerializeField]
    private float _runSpeed = 10f;

    [SerializeField]
    private float _faceDampening = .1f;

    [SerializeField]
    private float _animationDampTime = 25f;

    private void Awake() {
        Input = GetComponent<PlayerInput>();

        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCameraTransform = Camera.main.transform;
    }

    private void Start() {
        SwitchState(new PlayerFreeLookState(this));
    }

    public void Move(float deltaTime) {
        bool isMoving = Input.MovementVector != Vector2.zero;

        if (isMoving) {
            PerformMovement(deltaTime);
        }

        BlendAnimation(isMoving, deltaTime);
    }

    private void PerformMovement(float deltaTime) {
            Vector3 movement = CalculateMovement();

            _characterController.Move(movement * deltaTime * _runSpeed);

        FaceMovementDirection(movement, deltaTime);
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

    private void FaceMovementDirection(Vector3 movement, float deltaTime) {
        transform.rotation = Quaternion.Lerp(
           transform.rotation,
           Quaternion.LookRotation(movement),
           deltaTime * _faceDampening
        );

    }

    private void BlendAnimation(bool isMoving, float deltaTime) {
        _animator.SetFloat("IdleRunBlend", isMoving ? 1 : 0, _animationDampTime, deltaTime);
    }
}

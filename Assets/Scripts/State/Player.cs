using UnityEngine;
using BurgerBoy;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Player : StateMachine
{
    // Components
    public PlayerInput Input { get; private set; }

    private CharacterController _characterController;

    public Animator Animator { get; private set; }

    public Targeter Targeter { get; private set; }

    public AudioSource Audio { get; private set; }

    // Camera Access
    private Transform _mainCameraTransform;

    // Knobs
    [SerializeField]
    private float _runSpeed = 20f;

    [SerializeField]
    private float _targetingSpeedDampening = 0.5f;

    [SerializeField]
    private float _faceDampening = 20f;

    [SerializeField]
    private float _animationDampTime = .1f;

    // Forces
    private float _currentGravity = 0f;

    // Attacking
    [SerializeField]
    private Attack[] _attacks;

    public Attack CurrentAttack { get; private set; }

    private Vector3 impact = Vector3.zero;


    private void Awake()
    {
        Input = GetComponent<PlayerInput>();

        _characterController = GetComponent<CharacterController>();

        Animator = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
        Targeter = GetComponentInChildren<Targeter>();

        _mainCameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        _globalState = new PlayerGlobalState(this);
        _globalState.Enter();
        SwitchState(new PlayerFreeLookState(this));
    }

    public void ApplyGravity()
    {
        if (_characterController.isGrounded)
        {
            _currentGravity = 0;
        }
        else
        {
            _currentGravity += Constants.GRAVITY;
            _characterController.Move(new Vector3(0, _currentGravity, 0) * Time.deltaTime);
        }
    }

    public void Move()
    {
        bool isMoving = Input.MovementVector != Vector2.zero;

        if (isMoving)
        {
            PerformMovement();
        }

        BlendMovementAnimation(isMoving);
    }

    private void PerformMovement()
    {
        Vector3 movement = CalculateMovement();

        float multiplier = _currentState.Name == "PlayerTargetingState" ? _targetingSpeedDampening : 1;

        _characterController.Move(movement * Time.deltaTime * _runSpeed * multiplier);

        FaceMovementDirection(movement);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 cameraForward = _mainCameraTransform.forward;
        Vector3 cameraRight = _mainCameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * Input.MovementVector.y + cameraRight * Input.MovementVector.x;
    }

    private void FaceMovementDirection(Vector3 movement)
    {
        transform.rotation = Quaternion.Lerp(
           transform.rotation,
           Quaternion.LookRotation(movement),
           Time.deltaTime * _faceDampening
        );
    }

    private void BlendMovementAnimation(bool isMoving)
    {
        Animator.SetFloat("IdleRunBlend", isMoving ? 1 : 0, _animationDampTime, Time.deltaTime);
    }

    public void ApplyForce(Vector3 force)
    {
        _characterController.Move(force);
    }

    public void PerformAttack(AttackName attackName)
    {
        CurrentAttack = _attacks.FirstOrDefault(attack => attack.Name == attackName);

        Audio.clip = CurrentAttack.AudioClip;

        Audio.PlayDelayed(CurrentAttack.AudioDelay);

        Animator.Play(CurrentAttack.Animation.ToString(), 0, 0);
    }
}

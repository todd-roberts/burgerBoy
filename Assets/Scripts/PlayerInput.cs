using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, PlayerControls.IPlayerActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetingEvent;
    public event Action CancelEvent;

    public Vector2 MovementVector { get; private set; }
    private PlayerControls _controls;

    private void Start() {
        _controls = new PlayerControls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDestroy(){
        _controls.Player.Disable();
    }

    public void OnLook(InputAction.CallbackContext context){}

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            MovementVector = context.ReadValue<Vector2>();
        } else {
            MovementVector = new Vector2(0, 0);
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            JumpEvent?.Invoke();
        }
    }

    public void OnDodge(InputAction.CallbackContext context) {
        if (context.performed) {
            DodgeEvent?.Invoke();
        }
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (context.performed) {
            TargetingEvent?.Invoke();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) {
            CancelEvent?.Invoke();
        }
    }
}

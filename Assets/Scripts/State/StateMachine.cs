using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State _currentState;
    protected State _globalState;

    void Update()
    {
        _globalState?.Tick(Time.deltaTime);
        _currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State state)
    {
        if (_currentState == null) {
            Debug.Log($"Switching to state {state.Name}");
        } else {
            Debug.Log($"Switching states: {_currentState.Name} -> {state.Name}");
            _currentState.Exit();
        }

        _currentState = state;

        _currentState.Enter();
    }
}

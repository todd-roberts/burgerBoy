using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _previousState;
    protected State _currentState;
    protected State _globalState;

    protected float _timeThisState = 0f;

    void Update()
    {
        if (Time.timeScale == 1)
        {
            _timeThisState += Time.deltaTime;
            _globalState?.Tick(Time.deltaTime);
            _currentState?.Tick(Time.deltaTime);
        }
    }

    public float GetTimeThisState() => _timeThisState;


    public void SwitchState(State state)
    {
        Debug.Log(gameObject.name + "transition to state: " + state.Name);
        _currentState?.Exit();

        _previousState = _currentState;

        _timeThisState = 0f;

        _currentState = state;

        _currentState.Enter();
    }

    public void RewindState()
    {
        if (_previousState != null)
        {
            SwitchState(_previousState);
        }
    }
}

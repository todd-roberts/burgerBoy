using UnityEngine;

public class RetreatState : FortuneCookieBaseState
{

    public RetreatState(FortuneCookie fortuneCookie) : base(fortuneCookie)
    {
        Name = "RetreatState";
    }

    private Vector3 _targetPosition;

    private float _lastDistanceToTarget;

    public override void Enter()
    {

        CalculateTargetPosition();
    }

    private bool ReachedTarget() => GetDistanceToTarget() < _fortuneCookie.RetreatTolerance;

    private float GetDistanceToTarget() => Vector3.Distance(_targetPosition, _fortuneCookie.transform.position);
    private void CalculateTargetPosition()
    {
        Vector3 direction = Vector3.Normalize(_fortuneCookie.transform.position - _fortuneCookie.GetPlayerPosition());

        direction.y = 0f;

        _fortuneCookie.transform.rotation = Quaternion.LookRotation(direction);

        _targetPosition = (direction * _fortuneCookie.RetreatDistance) + _fortuneCookie.transform.position;
    }

    public override void Tick(float deltaTime)
    {
        _fortuneCookie.Character.Run();

        _fortuneCookie.CharacterController.Move(CalculateMovement());

        CheckPosition();
    }

    private Vector3 CalculateMovement() => (Vector3.Normalize(_targetPosition - _fortuneCookie.transform.position)) * Time.deltaTime * _fortuneCookie.RetreatSpeed;

    private void CheckPosition()
    {
        float distanceToTarget = GetDistanceToTarget();

        float d2tDelta = Mathf.Abs(distanceToTarget - _lastDistanceToTarget);

        bool moving = d2tDelta >= .01f;

        if (ReachedTarget() || !moving)
        {
            _fortuneCookie.SwitchState(new CastState(_fortuneCookie));
        }

        _lastDistanceToTarget = distanceToTarget;
    }
}

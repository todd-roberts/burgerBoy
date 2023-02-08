using UnityEngine;

public class Character : MonoBehaviour
{
    Animator _animator;

    [SerializeField]
    private string _idleRunBlendTreeName = "IdleRunBlendTree";

    [SerializeField]
    private string _idleRunBlendParameter = "IdleRunBlend";

    [SerializeField]
    private string _damageStateName = "Damage";

    [SerializeField]
    private float _transitionDuration = .1f;

    // The time at which the Character's arms are fully extended in the cast
    [SerializeField]

    private float _castSpellAt = .9f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void IdleRunBlend()
    {
        print("Yo?");
        _animator.Play(_idleRunBlendTreeName);
        //_animator.CrossFadeInFixedTime(_idleRunBlendTreeName, _transitionDuration);
    }

    public void Idle()
    {
        _animator.SetFloat(_idleRunBlendParameter, 0, .1f, Time.deltaTime);
    }

    public void Run()
    {
        _animator.SetFloat(_idleRunBlendParameter, 1, .1f, Time.deltaTime);
    }

    public void Cast()
    {
        _animator.CrossFadeInFixedTime("Cast", .1f);
        //_animator.Play("Cast");
    }

    public bool AnimationProgressReached(string animationName, float progress)
    {
        AnimatorStateInfo currentInfo = _animator.GetCurrentAnimatorStateInfo(0);

        return currentInfo.IsName(animationName) && currentInfo.normalizedTime >= progress;
    }

    public bool ShouldCastSpell() => AnimationProgressReached("Cast", _castSpellAt);

    public bool CastComplete() => AnimationProgressReached("Cast", 1.0f);

    public void Damage()
    {
        _animator.Play(_damageStateName, -1, 0f);
    }
    public bool DamageComplete() => AnimationProgressReached("Damage", 1.0f) || AnimationProgressReached("Idle", 0);
}

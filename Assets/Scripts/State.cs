public abstract class State
{
    public string Name { get; protected set; }
    public virtual void Enter() { }

    public abstract void Tick(float deltaTime);

    public virtual void Exit() { }
}
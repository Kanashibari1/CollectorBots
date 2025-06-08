public abstract class Fsm
{
    protected StateMachine _stateMachine;

    public Fsm(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Update() { }
}

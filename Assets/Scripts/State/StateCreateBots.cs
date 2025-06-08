public class StateCreateBots : Fsm
{
    private int _resourceCreateBots;
    private Warehouse _warehouse;
    private Base _base;

    public StateCreateBots(StateMachine stateMachine, Warehouse warehouse, Base @base, int resourceCreateBots) : base(stateMachine)
    {
        _resourceCreateBots = resourceCreateBots;
        _warehouse = warehouse;
        _base = @base;
    }

    public override void Update()
    {
        if(_base.IsBuilding)
        {
            _stateMachine.GetState<StateCreateBase>();
        }
        else if(_warehouse.ResourceCount == _resourceCreateBots)
        {
            _base.Spawn();
            _warehouse.Remove();
        }
    }
}

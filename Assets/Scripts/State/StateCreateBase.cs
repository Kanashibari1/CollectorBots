using Unity.VisualScripting;
using UnityEngine;

public class StateCreateBase : Fsm
{
    private Base _base;
    private Warehouse _werehouse;
    private int _resourceBuildBase;

    public StateCreateBase(StateMachine stateMachine, Base @base, Warehouse warehouse, int resourceBuildBase) : base(stateMachine)
    {
        _base = @base;
        _werehouse = warehouse;
        _resourceBuildBase = resourceBuildBase;
    }

    public override void Update()
    {
        if(_base.IsBuilding == false)
        {
            _stateMachine.GetState<StateCreateBots>();
        }
        else if(_werehouse.ResourceCount == _resourceBuildBase)
        {
            _base.StartBuildingBase();
        }
    }
}

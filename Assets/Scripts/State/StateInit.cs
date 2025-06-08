using UnityEngine;

public class StateInit : MonoBehaviour
{
    private int _resourceCreateBots = 3;
    private int _resourceCreateBase = 5;
    private StateMachine _stateMachine;
    private Base _base;
    private Warehouse _warehouse;

    private void Awake()
    {
        _base = GetComponent<Base>();
        _warehouse = GetComponent<Warehouse>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();

        _stateMachine.AddState(new StateCreateBots(_stateMachine, _warehouse, _base, _resourceCreateBots));
        _stateMachine.AddState(new StateCreateBase(_stateMachine, _base, _warehouse, _resourceCreateBase));

        _stateMachine.GetState<StateCreateBots>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}

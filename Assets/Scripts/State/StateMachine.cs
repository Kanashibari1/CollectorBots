using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, Fsm> _state = new();
    private Fsm _currentState;

    public void AddState(Fsm fsm)
    {
        _state.Add(fsm.GetType(), fsm);
    }

    public void GetState<T>() where T : Fsm
    {
        Type type = typeof(T);

        if(_currentState != null && _currentState.GetType() == type)
        {
            return;
        }

        if(_state.TryGetValue(type, out var newState))
        {
            _currentState = newState;
        }
    }

    public void Update()
    {
        _currentState?.Update();
    }
}
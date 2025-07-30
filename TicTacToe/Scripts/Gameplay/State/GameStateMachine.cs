using System;
using System.Collections.Generic;

namespace TicTacToe.Gameplay.State;

public interface IGameStateMachine
{
    void EnterState<TState>() where TState : IState;
    void Update();
    void ExitState();
}

public class GameStateMachine : IGameStateMachine 
{
    private Dictionary<Type, IState> _states;
    
    private IState _currentState;

    public void SetupStates(Dictionary<Type, IState> states)
    {
        _states = states;
    }

    public void EnterState<TState>() where TState : IState
    {
        var state = _states[typeof(TState)];
        
        ExitState();
        _currentState = state;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void ExitState()
    {
        _currentState?.Exit();
        _currentState = null;
    }
}
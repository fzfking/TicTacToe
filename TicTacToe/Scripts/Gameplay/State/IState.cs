﻿namespace TicTacToe.Gameplay.State;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}
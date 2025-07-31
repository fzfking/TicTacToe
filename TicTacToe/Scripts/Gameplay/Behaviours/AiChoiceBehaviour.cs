using System;
using Godot;

namespace TicTacToe.Gameplay.Behaviours;

public class AiChoiceBehaviour : IChoiceBehaviour
{
    private GameState _gameState;

    public AiChoiceBehaviour(GameState gameState)
    {
        _gameState = gameState;
    }

    public void StartChoice(Action onChoiceComplete)
    {
        //todo
    }
}
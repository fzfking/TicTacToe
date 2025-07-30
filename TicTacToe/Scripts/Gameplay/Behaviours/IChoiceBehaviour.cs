using System;
using Godot;

namespace TicTacToe.Gameplay.Behaviours;

public interface IChoiceBehaviour
{
    void StartChoice(Action onChoiceComplete);
}
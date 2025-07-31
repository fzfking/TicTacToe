using System.Collections.Generic;
using Godot;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay.View;

public partial class GameScreen : Node
{
    [Export] public Node CellsContainer;
    [Export] public Label Player1Label;
    [Export] public Label Player2Label;
    [Export] public Label PlayerTurnLabel;
    [Export] public GameOverPopup GameOverPopup;
}
using Godot;
using Godot.Collections;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay.View;

public partial class GameScreen : Node
{
    [Export] public Node CellsContainer;
    [Export] public Dictionary<OccupiedBy, Label> Counters;
    [Export] public GameOverPopup GameOverPopup;
}
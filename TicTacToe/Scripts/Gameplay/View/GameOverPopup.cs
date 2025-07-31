using Godot;

namespace TicTacToe.Gameplay.View;

public partial class GameOverPopup : Control
{
    [Export] public Button RetryButton;
    [Export] public Button ExitButton;
    [Export] public Label WinnerLabel;
}
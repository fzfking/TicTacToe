using AutofacGodotDi;
using AutofacGodotDi.Attributes;
using Godot;

namespace TicTacToe.Presentation;

public partial class MainScreen : Node
{
    [Export] public Button PlayWithBotButton;
    [Export] public Button PlayWithFriendButton;
    
    private Timer _timer;

    private LoadingScreenController _loadingScreenController;

    [Inject]
    public void Inject(LoadingScreenController loadingScreenController)
    {
        _loadingScreenController = loadingScreenController;
    }

    public override void _Ready()
    {
        _loadingScreenController.Hide();
    }
}
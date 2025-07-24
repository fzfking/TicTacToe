using AutofacGodotDi;
using AutofacGodotDi.Attributes;
using Godot;

namespace TicTacToe.Presentation;

public partial class MainScreen : Node
{
    [Export] public Button PlayWithBotButton;
    [Export] public Button PlayWithFriendButton;
    
    private Timer _timer;

    [Inject]
    public void Inject()
    {
    }
}
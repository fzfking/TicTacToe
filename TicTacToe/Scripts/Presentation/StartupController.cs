using AutofacGodotDi;
using AutofacGodotDi.Attributes;
using Godot;

namespace TicTacToe.Presentation;

public partial class StartupController : Node
{
    private NavigationController _navigationController;

    [Inject]
    public void Inject(NavigationController navigationController)
    {
        _navigationController = navigationController;
    }

    public override void _Ready()
    {
        _navigationController.GoTo("res://Scenes/MainScene.tscn");
    }
}
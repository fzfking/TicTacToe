using AutofacGodotDi;
using AutofacGodotDi.Attributes;
using Godot;

namespace TicTacToe.Presentation;

public partial class StartupController : Node
{
    private LoadingScreenController _loadingScreenController;
    private ISceneController _sceneController;

    [Inject]
    public void Inject(LoadingScreenController loadingScreenController, ISceneController sceneController)
    {
        _loadingScreenController = loadingScreenController;
        _sceneController = sceneController;
    }

    public override void _Ready()
    {
        _loadingScreenController.Show(() =>
        {
            _sceneController.ChangeScene("res://Scenes/MainScene.tscn", _loadingScreenController.Reorder);
        });
    }
}
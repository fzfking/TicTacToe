using AutofacGodotDi;
using AutofacGodotDi.Attributes;
using Godot;
using TicTacToe.Gameplay;

namespace TicTacToe.Presentation;

public partial class MainScreen : Node
{
    [Export] public Button PlayWithBotButton;
    [Export] public Button PlayWithFriendButton;
    
    private Timer _timer;

    private LoadingScreenController _loadingScreenController;
    private ISceneController _sceneController;
    private GameState _gameState;

    [Inject]
    public void Inject(LoadingScreenController loadingScreenController, ISceneController sceneController, GameState gameState)
    {
        _loadingScreenController = loadingScreenController;
        _sceneController = sceneController;
        _gameState = gameState;
    }

    public override void _Ready()
    {
        _loadingScreenController.Hide();
        PlayWithFriendButton.Pressed += PlayWithFriend;
        PlayWithBotButton.Pressed += PlayWithBot;
    }

    private void PlayWithFriend()
    {
        _gameState.Session = new Session(true);
        _sceneController.ChangeScene("res://Scenes/GameScene.tscn");
    }

    private void PlayWithBot()
    {
        _gameState.Session = new Session(false);
        _sceneController.ChangeScene("res://Scenes/GameScene.tscn");
    }
}
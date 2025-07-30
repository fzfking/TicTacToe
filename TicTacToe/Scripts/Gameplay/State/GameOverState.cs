using AutofacGodotDi;
using TicTacToe.Gameplay.View;

namespace TicTacToe.Gameplay.State;

public class GameOverState : IState
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ISceneController _sceneController;
    private readonly GameScreen _gameScreen;

    public GameOverState(IGameStateMachine gameStateMachine, ISceneController sceneController, GameScreen gameScreen)
    {
        _gameStateMachine = gameStateMachine;
        _sceneController = sceneController;
        _gameScreen = gameScreen;
    }
    
    public void Enter()
    {
        _gameScreen.GameOverPopup.RetryButton.Pressed += RetryButtonPressed;
        _gameScreen.GameOverPopup.ExitButton.Pressed += ExitButtonPressed;
    }

    private void RetryButtonPressed()
    {
        _sceneController.ChangeScene("res://Scenes/GameScene.tscn");
    }

    private void ExitButtonPressed()
    {
        _sceneController.ChangeScene("res://Scenes/MainMenu.tscn");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
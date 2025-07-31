using AutofacGodotDi;
using TicTacToe.Gameplay.Enums;
using TicTacToe.Gameplay.View;

namespace TicTacToe.Gameplay.State;

public class GameOverState : IState
{
    private readonly GameState _gameState;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ISceneController _sceneController;
    private readonly GameScreen _gameScreen;

    public GameOverState(IGameStateMachine gameStateMachine, ISceneController sceneController, GameScreen gameScreen, GameState gameState)
    {
        _gameStateMachine = gameStateMachine;
        _sceneController = sceneController;
        _gameScreen = gameScreen;
        _gameState = gameState;
    }

    public void Enter()
    {
        _gameScreen.GameOverPopup.RetryButton.Pressed += RetryButtonPressed;
        _gameScreen.GameOverPopup.ExitButton.Pressed += ExitButtonPressed;
        
        //todo: gettext usage
        var winnerText = _gameState.Session.CurrentMatch.IsGameDraw 
            ? "Матч закончился ничьей." 
            : _gameState.Session.CurrentMatch.Winner == OccupiedBy.Player1 
                ? "Матч закончился победой игрока X" 
                : "Матч закончился победой игрока O";
        
        _gameScreen.GameOverPopup.WinnerLabel.Text = winnerText;
        _gameScreen.GameOverPopup.Show();
    }

    private void RetryButtonPressed()
    {
        _sceneController.ChangeScene("res://Scenes/GameScene.tscn");
    }

    private void ExitButtonPressed()
    {
        _sceneController.ChangeScene("res://Scenes/MainScene.tscn");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
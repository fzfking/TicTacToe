using System;
using System.Collections.Generic;
using AutofacGodotDi;
using TicTacToe.Gameplay.State;
using TicTacToe.Gameplay.View;
using TicTacToe.Interfaces;

namespace TicTacToe.Gameplay;

public class GameController : IEntryPoint
{
    private GameStateMachine _gameStateMachine;
    private readonly GameState _gameState;
    private readonly MapViewController _mapViewController;
    private readonly ISceneController _sceneController;
    private readonly GameScreen _gameScreen;

    public GameController(GameState gameState, MapViewController mapViewController, ISceneController sceneController, GameScreen gameScreen)
    {
        _gameState = gameState;
        _mapViewController = mapViewController;
        _sceneController = sceneController;
        _gameScreen = gameScreen;
    }

    public void Start()
    {
        _gameStateMachine = new GameStateMachine();
        _gameState.Session.StartMatch();
        _mapViewController.Init();
        _gameStateMachine.SetupStates(new Dictionary<Type, IState>
        {
            { typeof(TurnState), new TurnState(_gameState, _gameStateMachine, _mapViewController, _gameScreen) },
            { typeof(GameOverState), new GameOverState(_gameStateMachine, _sceneController, _gameScreen, _gameState) },
            { typeof(CheckWinnerState), new CheckWinnerState(_gameState, _gameStateMachine)}
        });
        _gameStateMachine.EnterState<TurnState>();
    }
}
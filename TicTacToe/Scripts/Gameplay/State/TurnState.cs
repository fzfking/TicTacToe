using Godot;
using TicTacToe.Gameplay.Behaviours;
using TicTacToe.Gameplay.Enums;
using TicTacToe.Gameplay.View;

namespace TicTacToe.Gameplay.State;

public class TurnState : IState
{
    private readonly GameState _gameState;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly MapViewController _mapViewController;
    private readonly GameScreen _gameScreen;
    private OccupiedBy CurrentPlayer => _gameState.Session.CurrentMatch.CurrentPlayer;
    private IChoiceBehaviour _choiceBehaviour;

    public TurnState(GameState gameState, IGameStateMachine gameStateMachine, MapViewController mapViewController, GameScreen gameScreen)
    {
        _gameState = gameState;
        _gameStateMachine = gameStateMachine;
        this._mapViewController = mapViewController;
        _gameScreen = gameScreen;
    }

    public void Enter()
    {
        var playerSymbol = CurrentPlayer == OccupiedBy.Player1 ? "X" : "O";
        
        //todo: gettext usage
        _gameScreen.PlayerTurnLabel.Text = $"Ход игрока {playerSymbol}";
        _choiceBehaviour = _gameState.Session.CurrentMatch.CurrentPlayer switch
        {
            OccupiedBy.Player1 => new PlayerChoiceBehaviour(_gameState, _mapViewController),
            OccupiedBy.Player2 => _gameState.Session.Pvp 
                ? new PlayerChoiceBehaviour(_gameState, _mapViewController) 
                : new AiChoiceBehaviour(_gameState)
        };
        
        _choiceBehaviour.StartChoice(OnChoiceComplete);
    }

    public void Update()
    {
    }

    private void OnChoiceComplete()
    {
        _gameStateMachine.EnterState<CheckWinnerState>();
    }

    public void Exit()
    {
        _gameState.Session.CurrentMatch.CurrentPlayer = CurrentPlayer == OccupiedBy.Player1 
            ? OccupiedBy.Player2 
            : OccupiedBy.Player1;
    }
}
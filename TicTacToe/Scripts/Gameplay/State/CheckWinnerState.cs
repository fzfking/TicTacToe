using System.Linq;
using TicTacToe.Gameplay.Entities;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay.State;

public class CheckWinnerState : IState
{
    private readonly GameState _gameState;
    private readonly IGameStateMachine _gameStateMachine;

    public CheckWinnerState(GameState gameState, IGameStateMachine gameStateMachine)
    {
        _gameState = gameState;
        _gameStateMachine = gameStateMachine;
    }
    
    public void Enter()
    {
        var map = _gameState.Session.CurrentMatch.Map;
        var size = map.Size.X;

        var winner = OccupiedBy.None;
        
        for (var row = 0; row < size; row++)
        {
            winner = CheckLine(map, startX: 0, startY: row, deltaX: 1, deltaY: 0);
            if (winner != OccupiedBy.None)
                break;
        }
        
        if (winner == OccupiedBy.None)
        {
            for (var col = 0; col < size; col++)
            {
                winner = CheckLine(map, startX: col, startY: 0, deltaX: 0, deltaY: 1);
                if (winner != OccupiedBy.None)
                    break;
            }
        }
        
        if (winner == OccupiedBy.None)
        {
            winner = CheckLine(map, startX: 0, startY: 0, deltaX: 1, deltaY: 1);
        }
        
        if (winner == OccupiedBy.None)
        {
            winner = CheckLine(map, startX: size - 1, startY: 0, deltaX: -1, deltaY: 1);
        }

        if (winner != OccupiedBy.None)
        {
            _gameStateMachine.EnterState<TurnState>();
        }
        else
        {
            if (map.Cells.Values.All(x => x.IsOccupied))
            {
                _gameStateMachine.EnterState<GameOverState>();
            }
            else
            {
                _gameStateMachine.EnterState<TurnState>();
            }
        }
    }

    private OccupiedBy CheckLine(Map map, int startX, int startY, int deltaX, int deltaY)
    {
        var size = map.Size.X;
        var firstCellOwner = OccupiedBy.None;

        for (var i = 0; i < size; i++)
        {
            var pos = new Godot.Vector2I(startX + i * deltaX, startY + i * deltaY);
            var cell = map.Cells[pos];

            if (cell.IsOccupied == false)
                return OccupiedBy.None;

            if (i == 0)
            {
                firstCellOwner = cell.OccupiedBy;
            }
            else if (cell.OccupiedBy != firstCellOwner)
            {
                return OccupiedBy.None;
            }
        }

        return firstCellOwner;
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using TicTacToe.Gameplay.Entities;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay.Behaviours;

public class AiChoiceBehaviour : IChoiceBehaviour
{
    private readonly GameState _gameState;

    public AiChoiceBehaviour(GameState gameState)
    {
        _gameState = gameState;
    }

    public void StartChoice(Action onChoiceComplete)
    {
        Task.Delay(TimeSpan.FromSeconds(0.5f))
            .ContinueWith(_ => { Callable.From(MakeChoiceAfterDelay).CallDeferred(); });

        void MakeChoiceAfterDelay()
        {
            var map = _gameState.Session.CurrentMatch.Map;
            var size = map.Size.X;
            var ai = _gameState.Session.CurrentMatch.CurrentPlayer;
            var player = OccupiedBy.Player1;

            var blockingMove = FindBlockingMove(map, size, player);
            Vector2I chosenMove;

            if (blockingMove.HasValue)
            {
                chosenMove = blockingMove.Value;
            }
            else
            {
                var available = map.Cells
                    .Where(pair => !pair.Value.IsOccupied)
                    .Select(pair => pair.Key)
                    .ToList();

                chosenMove = available[GD.RandRange(0, available.Count - 1)];
            }

            map.Occupy(chosenMove, ai);
            onChoiceComplete?.Invoke();
        }
    }

    private static Vector2I? FindBlockingMove(Map map, int size, OccupiedBy opponent)
    {
        for (var i = 0; i < size; i++)
        {
            var horizontal = FindCriticalCell(map, size, startX: 0, startY: i, deltaX: 1, deltaY: 0, opponent);
            if (horizontal.HasValue)
                return horizontal;

            var vertical = FindCriticalCell(map, size, startX: i, startY: 0, deltaX: 0, deltaY: 1, opponent);
            if (vertical.HasValue)
                return vertical;
        }

        var diagonal1 = FindCriticalCell(map, size, 0, 0, 1, 1, opponent);
        if (diagonal1.HasValue)
            return diagonal1;

        var diagonal2 = FindCriticalCell(map, size, size - 1, 0, -1, 1, opponent);
        return diagonal2;
    }

    private static Vector2I? FindCriticalCell(Map map, int size, int startX, int startY, int deltaX, int deltaY, OccupiedBy opponent)
    {
        var opponentCount = 0;
        Vector2I? emptyCell = null;

        for (var i = 0; i < size; i++)
        {
            var pos = new Vector2I(startX + i * deltaX, startY + i * deltaY);
            var cell = map.Cells[pos];

            if (cell.OccupiedBy == opponent)
                opponentCount++;
            else if (!cell.IsOccupied)
                emptyCell = pos;
            else
                return null;
        }

        return opponentCount == size - 1 && emptyCell.HasValue ? emptyCell : null;
    }
}
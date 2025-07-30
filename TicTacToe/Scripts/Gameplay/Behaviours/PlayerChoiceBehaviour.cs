using System;

namespace TicTacToe.Gameplay.Behaviours;

public class PlayerChoiceBehaviour : IChoiceBehaviour
{
    private GameState _gameState;
    private readonly MapViewController _mapViewController;
    private Action _onChoiceComplete;

    public PlayerChoiceBehaviour(GameState gameState, MapViewController mapViewController)
    {
        _gameState = gameState;
        _mapViewController = mapViewController;
    }

    public void StartChoice(Action onChoiceComplete)
    {
        _onChoiceComplete = onChoiceComplete;
        _gameState.Session.CurrentMatch.Map.OnCellOccupied += OnCellOccupied;
        _mapViewController.SetInputLocked(false);
    }

    private void OnCellOccupied()
    {
        _gameState.Session.CurrentMatch.Map.OnCellOccupied -= OnCellOccupied;
        _mapViewController.SetInputLocked(true);
        _onChoiceComplete?.Invoke();
    }
}
using System;
using System.Collections.Generic;
using Godot;
using TicTacToe.Gameplay.Enums;
using TicTacToe.Gameplay.Factory;

namespace TicTacToe.Gameplay;

public class MapViewController : IDisposable
{
    private readonly GameState _gameState;
    private readonly CellViewFactory _cellViewFactory;
    private bool _isInputLocked = true;
    
    public Dictionary<Vector2I, Button> Buttons { get; private set; }

    public MapViewController(GameState gameState, CellViewFactory cellViewFactory)
    {
        _gameState = gameState;
        _cellViewFactory = cellViewFactory;
    }

    public void Init()
    {
        _gameState.Session.CurrentMatch.Map.OnCellOccupied += Update;
        Buttons = _cellViewFactory.CreateButtons(new Vector2I(3, 3));
        SubscribeCells();
    }

    private void SubscribeCells()
    {
        foreach (var (position, button) in Buttons)
        {
            button.Pressed += OnPressed;
            continue;
            
            void OnPressed()
            {
                if (_gameState.Session.CurrentMatch.Map.Cells[position].IsOccupied)
                {
                    return;
                }

                if (_isInputLocked)
                {
                    return;
                }
                
                _gameState.Session.CurrentMatch.OccupyByCurrentPlayer(position);
            }
        }
    }

    public void SetInputLocked(bool isLocked)
    {
        _isInputLocked = isLocked;
    }
    
    public void Dispose()
    {
        _gameState.Session.CurrentMatch.Map.OnCellOccupied -= Update;
    }

    public void Update()
    {
        foreach (var (position, cell) in _gameState.Session.CurrentMatch.Map.Cells)
        {
            var button = Buttons[position];
            var actualText = cell.OccupiedBy switch
            {
                OccupiedBy.None => string.Empty,
                OccupiedBy.Player1 => "X",
                OccupiedBy.Player2 => "O"
            };

            if (button.Text != actualText)
            {
                //todo: animation
                button.Text = actualText;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Godot;
using TicTacToe.Gameplay.Enums;
using TicTacToe.Gameplay.Factory;
using TicTacToe.Gameplay.View;

namespace TicTacToe.Gameplay;

public class MapViewController : IDisposable
{
    private readonly GameState _gameState;
    private readonly CellViewFactory _cellViewFactory;
    private readonly GameScreen _gameScreen;
    private bool _isInputLocked = true;

    private StringBuilder _localizedTextBuilder = new();
    
    public Dictionary<Vector2I, Button> Buttons { get; private set; }

    public MapViewController(GameState gameState, CellViewFactory cellViewFactory, GameScreen gameScreen)
    {
        _gameState = gameState;
        _cellViewFactory = cellViewFactory;
        _gameScreen = gameScreen;
    }

    public void Init()
    {
        _gameState.Session.CurrentMatch.Map.OnCellOccupied += Update;
        Buttons = _cellViewFactory.CreateButtons(new Vector2I(3, 3));
        
        _gameScreen.Player1Label.Text = GetCounterTextForPlayer(OccupiedBy.Player1);
        _gameScreen.Player2Label.Text = GetCounterTextForPlayer(OccupiedBy.Player2);
        
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
            var actualText = TranslationServer.Translate(cell.OccupiedBy.ToString());

            if (button.Text != actualText)
            {
                //todo: animation
                button.Text = actualText;
            }
        }
    }

    private string GetCounterTextForPlayer(OccupiedBy player)
    {
        var player2Wins = _gameState.Session.Wins.GetValueOrDefault(player, 0);
        _localizedTextBuilder.Clear();
        _localizedTextBuilder.Append(TranslationServer.Translate("PlayerHint"));
        _localizedTextBuilder.Append(' ');
        _localizedTextBuilder.Append(TranslationServer.Translate(player.ToString()));
        _localizedTextBuilder.Append(": ");
        _localizedTextBuilder.Append(player2Wins);

        return _localizedTextBuilder.ToString();
    }
}
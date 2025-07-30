using System.Collections.Generic;
using Godot;
using TicTacToe.Gameplay.View;

namespace TicTacToe.Gameplay.Factory;

public class CellViewFactory
{
    private readonly GameScreen _gameScreen;

    public CellViewFactory(GameScreen gameScreen)
    {
        _gameScreen = gameScreen;
    }

    public Dictionary<Vector2I, Button> CreateButtons(Vector2I size)
    {
        var buttons = new Dictionary<Vector2I, Button>();
        var cellPrefab = ResourceLoader.Load<PackedScene>("res://Scenes/CellView.tscn");
        for (var i = 0; i < size.X; i++)
        {
            for (var j = 0; j < size.Y; j++)
            {
                var cellView = cellPrefab.Instantiate<Button>();
                cellView.Text = string.Empty;
                var cellPosition = new Vector2I(i, j);
                buttons[cellPosition] = cellView;
                _gameScreen.CellsContainer.AddChild(cellView);
            }
        }

        return buttons;
    }
}
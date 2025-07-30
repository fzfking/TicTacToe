using System;
using System.Collections.Generic;
using Godot;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay.Entities;

public class Map
{
    public Vector2I Size { get; }
    public readonly Dictionary<Vector2I, Cell> Cells;
    public event Action OnCellOccupied;

    public Map(Vector2I size)
    {
        Size = size;
        Cells = new Dictionary<Vector2I, Cell>(size.X * size.Y);
        
        for (var i = 0; i < size.X; i++)
        {
            for (var j = 0; j < size.Y; j++)
            {
                Cells[new Vector2I(i, j)] = new Cell();
            }
        }
    }

    public bool CanOccupy(Vector2I position)
    {
        return !Cells[position].IsOccupied;
    }

    public void Occupy(Vector2I position, OccupiedBy occupied)
    {
        Cells[position].OccupiedBy = occupied;
        OnCellOccupied?.Invoke();
    }
}
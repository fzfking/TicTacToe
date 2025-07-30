using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay.Entities;

public class Cell
{
    public OccupiedBy OccupiedBy { get; set; }
    public bool IsOccupied => OccupiedBy != OccupiedBy.None;
}
using Godot;
using TicTacToe.Gameplay.Entities;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay;

public class Match
{
    public Map Map { get; private set; }
    public OccupiedBy CurrentPlayer;

    public Match()
    {
        Map = new Map(new Vector2I(3, 3));
        CurrentPlayer = OccupiedBy.Player1;
    }
    
    public void OccupyByCurrentPlayer(Vector2I position)
    {
        Map.Occupy(position, CurrentPlayer);
    }
}
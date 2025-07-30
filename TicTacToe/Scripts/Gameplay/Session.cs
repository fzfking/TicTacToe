using System.Collections.Generic;
using Godot;
using TicTacToe.Gameplay.Entities;
using TicTacToe.Gameplay.Enums;

namespace TicTacToe.Gameplay;

public class Session
{
    public bool Pvp { get; }
    public Dictionary<OccupiedBy, int> Wins { get; }
    public Match CurrentMatch { get; private set; }
    

    public Session(bool pvp)
    {
        Pvp = pvp;
        Wins = new();
    }

    public void StartMatch()
    {
        CurrentMatch = new Match();
    }
}
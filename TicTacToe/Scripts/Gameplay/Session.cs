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

    public void EndMatch(bool hasWinner, OccupiedBy winner = OccupiedBy.None)
    {
        if (hasWinner)
        {
            CurrentMatch.Complete(winner);
            if (!Wins.TryAdd(winner, 1)) 
                Wins[winner]++;
        }
        else
        {
            CurrentMatch.SetGameDraw();
        }
    }
}
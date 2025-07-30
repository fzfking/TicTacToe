using System.Collections.Generic;
using Autofac;
using AutofacGodotDi;
using Godot;
using TicTacToe.Gameplay;
using TicTacToe.Gameplay.Factory;
using TicTacToe.Gameplay.View;
using TicTacToe.Interfaces;

namespace TicTacToe.Scopes;

public partial class GameSceneContext : SceneContext
{
    [Export] public GameScreen GameScreen { get; set; }
    
    public override void InstallBindings(ContainerBuilder builder)
    {
        builder
            .RegisterInstance(GameScreen);
        
        builder
            .RegisterType<GameController>()
            .AsImplementedInterfaces();

        builder
            .RegisterType<MapViewController>();

        builder
            .RegisterType<CellViewFactory>();
    }
    
    public override void _Ready()
    {
        var entryPoints = LifetimeScope.Resolve<IEnumerable<IEntryPoint>>();
        foreach (var entryPoint in entryPoints) 
            entryPoint.Start();
    }
}
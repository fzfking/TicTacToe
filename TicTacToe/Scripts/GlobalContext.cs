using Autofac;
using AutofacGodotDi;
using TicTacToe.Presentation;

namespace TicTacToe.Scripts;

public partial class GlobalContext :SceneContext
{
    public override void InstallBindings(ContainerBuilder builder)
    {
        builder
            .RegisterType<NavigationController>()
            .SingleInstance();
    }
}
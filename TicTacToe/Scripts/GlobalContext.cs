using Autofac;
using AutofacGodotDi;
using TicTacToe.Presentation;

namespace TicTacToe;

public partial class GlobalContext :SceneContext
{
    public override void InstallBindings(ContainerBuilder builder)
    {
        builder
            .RegisterType<LoadingScreenController>()
            .SingleInstance();
    }
}
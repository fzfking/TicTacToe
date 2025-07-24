using System;
using System.Threading.Tasks;
using AutofacGodotDi;
using Godot;

namespace TicTacToe.Presentation;

public class NavigationController
{
    private ISceneController _sceneController;

    public NavigationController(ISceneController sceneController)
    {
        _sceneController = sceneController;
    }

    public void GoTo(string scenePath)
    {
        _sceneController.ChangeScene("res://Scenes/LoadingScene.tscn");
        Task.Delay(TimeSpan.FromSeconds(2))
            .ContinueWith(_ =>
            {
                Callable.From(() => (_sceneController.GetCurrentScene() as LoadingScreen)?.HideAnimated()).CallDeferred();
                Task.Delay(TimeSpan.FromSeconds(1))
                    .ContinueWith(_ =>
                    {
                        _sceneController.ChangeScene(scenePath);
                    });
            });
    }
}
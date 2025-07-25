using System;
using Godot;

namespace TicTacToe.Presentation;

public class LoadingScreenController
{
    private LoadingScreen _loadingScreen;
    private Window _root;

    public LoadingScreenController()
    {
        _root = (Engine.Singleton.GetMainLoop() as SceneTree)?.Root;
    }

    public void Show(Action onComplete)
    {
        Callable.From(() =>
        {
            if (_loadingScreen == null)
            {
                var packedScene = ResourceLoader.Load<PackedScene>("res://Scenes/LoadingScene.tscn");
                _loadingScreen = packedScene.Instantiate<LoadingScreen>();
            }

            _loadingScreen.HideInstant();
            _root?.AddChild(_loadingScreen);
            _loadingScreen.ShowAnimated(onComplete);
        }).CallDeferred();
    }

    public void Reorder()
    {
        if (_loadingScreen != null)
        {
            Callable.From(() =>
            {
                _root?.MoveChild(_loadingScreen, -1);
            }).CallDeferred();
        }
    }

    public void Hide()
    {
        Callable.From(() =>
        {
            if (_loadingScreen == null)
            {
                return;
            }

            _loadingScreen.HideAnimated(() =>
            {
                _root?.RemoveChild(_loadingScreen);
                _loadingScreen.QueueFree();
                _loadingScreen = null;
            });
        }).CallDeferred();
    }
}
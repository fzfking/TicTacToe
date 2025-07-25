using System;
using Godot;

namespace TicTacToe.Presentation;

public partial class LoadingScreen : Control
{
    [Export] public TextureRect RollingIcon;

    public void ShowInstant()
    {
        Modulate = new Color(1, 1, 1, 1);
        Visible = true;
    }

    public void ShowAnimated(Action onComplete = null)
    {
        var tween = CreateTween();
        Visible = true;
        tween.TweenProperty(this, "modulate", new Color(1, 1, 1, 1), 2).From(new Color(1, 1, 1, 0));
        tween.TweenCallback(Callable.From(() => onComplete?.Invoke()));
    }

    public void HideInstant()
    {
        Modulate = new Color(1, 1, 1, 0);
        Visible = false;
    }

    public void HideAnimated(Action onComplete = null)
    {
        var tween = CreateTween();
        tween.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), 1).From(new Color(1, 1, 1, 1));
        tween.TweenCallback(Callable.From(() => onComplete?.Invoke()));
    }

    private void RemoveFromHierarchy()
    {
        GetParent().RemoveChild(this);
    }
}
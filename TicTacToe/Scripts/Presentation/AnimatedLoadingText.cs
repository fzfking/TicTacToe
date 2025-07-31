using Godot;

namespace TicTacToe.Presentation;

public partial class AnimatedLoadingText : RichTextLabel
{
    private int _dotCount;
    private Timer _timer;
    
    public override void _Ready()
    {
        base._Ready();
        _timer = new Timer();
        _timer.WaitTime = 0.5f;
        _timer.Timeout += UpdateText;
        AddChild(_timer);
        _timer.Start();
    }

    private void UpdateText()
    {
        _dotCount++;
        if (_dotCount > 3)
        {
            _dotCount = 0;
        }
        
        Text = TranslationServer.Translate("Loading") + new string('.', _dotCount);
    }
}
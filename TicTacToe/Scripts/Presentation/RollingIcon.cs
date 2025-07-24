using Godot;

namespace TicTacToe.Presentation;

public partial class RollingIcon : TextureRect
{
    [Export] public float RotationSpeed = 180f; // градусов в секунду
    
    private float _currentRotation;
    
    public override void _Process(double delta)
    {
        base._Process(delta);

        _currentRotation += (float)(RotationSpeed * delta);
        RotationDegrees = _currentRotation;
    }
}
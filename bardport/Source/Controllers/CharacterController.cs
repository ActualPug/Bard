using Godot;
using System;

public partial class CharacterController : Node
{
    [Export]
    public KinematicCharacter CharacterBody { get; set; }
    [Export]
    public AnimatedSprite2D CharacterSprite { get; set; }
    [Export]
    public double SwingSpeed { get; set; } = 0.5d;
    [Export]
    public DamageArea Area { get; set; }

    private PathingAgent _pathingInput;
    private string _curAnim = "idle";

    public override void _Ready()
    {
        _pathingInput = GetChild<PathingAgent>(0);
        Area.DamageDone += DamageDone;
    }

    public override void _Process(double delta)
    {
        CharacterBody.Direction = _pathingInput.MoveDirection;

        if (_pathingInput.MoveDirection.Length() > 0.05f)
        {
            _curAnim = "walking";
        }
        else
        {
            _curAnim = "idle";
        }

        CharacterSprite.Play(_curAnim);
    }

    public void DamageDone()
    {
        Area.SetDeferred("Monitoring", false);
        GetTree().CreateTimer(SwingSpeed).Timeout += () => Area.SetDeferred("Monitoring", true);
    }
}

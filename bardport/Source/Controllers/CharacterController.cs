using Godot;
using System;

public partial class CharacterController : Node
{
    [Export]
    public KinematicCharacter CharacterBody { get; set; }
    [Export]
    public AnimatedSprite2D CharacterSprite { get; set; }
    [Export]
    public Health ChracterHealth { get; set; }
    [Export]
    public double SwingSpeed { get; set; } = 0.5d;
    [Export]
    public DamageArea Area { get; set; }
    [Export]
    public uint Score { get; set; } = 1;

    private PathingAgent _pathingInput;
    private string _curAnim = "idle";

    public override void _Ready()
    {
        _pathingInput = GetChild<PathingAgent>(0);
        Area.DamageDone += DamageDone;
        ChracterHealth.HealthDepleted += OnDeath;
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

    public void OnDeath()
    {
        ScoreBoard.Score += Score;
        GetParent().QueueFree();
    }
}

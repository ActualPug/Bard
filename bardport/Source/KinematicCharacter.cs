using Godot;
using System;

public partial class KinematicCharacter : CharacterBody2D
{
    [Export]
    public float Speed { get; set; } = 300.0f;
    public Vector2 Direction { get; set; } = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (Direction != Vector2.Zero)
        {
            velocity = Direction * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}

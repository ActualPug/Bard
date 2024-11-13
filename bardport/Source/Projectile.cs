using Godot;
using System;

public partial class Projectile : Area2D
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public double LifeTime { get; set; } = 15;

    public override void _Ready()
    {
        BodyEntered += LocalBodyEntered;
        GetTree().CreateTimer(LifeTime).Timeout += QueueFree;
    }

    private void LocalBodyEntered(Node2D body)
    {
        if (body.IsInGroup("HasHealth"))
        {

        }

        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GlobalPosition += Direction * Speed * (float)delta;
    }
}

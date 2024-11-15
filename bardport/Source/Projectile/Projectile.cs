using Godot;
using Godot.Collections;
using System;

public partial class Projectile : Node2D
{
    [Export]
    public Vector2 Direction { get; set; }
    [Export]
    public float Speed { get; set; }
    [Export]
    public double LifeTime { get; set; } = 15;
    [Export]
    public int Damage { get; set; } = 1;
    [Export]
    public DamageArea Area { get; set; }
    [Export]
    public uint Layer { get; set; } = 1;
    [Export]
    public uint Mask { get; set; } = 1;

    public override void _Ready()
    {
        GetTree().CreateTimer(LifeTime).Timeout += QueueFree;
        Area.CollisionLayer = Layer;
        Area.CollisionMask = Mask;
        Area.DamageDone += QueueFree;
        Area.WasDestroyed += QueueFree;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GlobalPosition += Direction * Speed * (float)delta;
    }
}

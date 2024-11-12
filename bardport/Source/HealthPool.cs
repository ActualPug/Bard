using Godot;
using System;

public partial class HealthPool : Node
{
    [Export]
    public CollisionObject2D HitBox { get; set; }
    public int Health { get; set; }
}

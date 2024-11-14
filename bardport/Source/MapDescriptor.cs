using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class MapDescriptor : Node2D
{
    [Export]
    public KinematicCharacter Player { get; set; }
    [Export]
    public EnemyManager Manager { get; set; }

    private Node2D _randomPosition = new();

    public override void _Ready()
    {
        RandomNumberGenerator random = new();
        Node2D randPos = new()
        {
            GlobalPosition = new(random.RandfRange(512, 1024), random.RandfRange(512, 1024))
        };
        AddChild(randPos);
    }

    public override void _Process(double delta)
    {
        if (IsInstanceValid(Player))
        {
            Manager.Target = Player;
        }
        else
        {
            Manager.Target = _randomPosition;
        }
    }
}

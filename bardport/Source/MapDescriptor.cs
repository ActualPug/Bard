using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class MapDescriptor : Node2D
{
    [Export]
    public KinematicCharacter Player { get; set; }
    [Export]
    public PathManager Pathing { get; set; }

    public override void _Process(double delta)
    {
        if (IsInstanceValid(Player))
        {
            Pathing.TargetPos = Player.Position;
        }
    }
}

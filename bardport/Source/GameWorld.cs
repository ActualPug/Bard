using Godot;
using Godot.Collections;
using System;

public partial class GameWorld : SubViewport
{
    [Export]
    public KinematicCharacter Player { get; set; }
	[Export]
	public MapDescriptor CurrentMap { get; set; }

    public override void _Ready()
    {
        Player.Position = CurrentMap.PlayerSpawnPoint;
    }
}

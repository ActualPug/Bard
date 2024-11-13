using Godot;
using System;
using static System.Formats.Asn1.AsnWriter;

public partial class EnemySpawner : Node2D, ISpawner
{
	[Export]
	public PackedScene EntityToSpawn { get; set; }
	[Export]
	public double SpawnRate { get; set; } = 1d;
	[Export]
    public Node2D Parent { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetTree().CreateTimer(5).Timeout += SpawnEntity;
    }

    public void SpawnEntity()
	{
		CharacterBody2D instance = (CharacterBody2D) EntityToSpawn.Instantiate();
		instance.GlobalPosition = GlobalPosition;

        Parent.AddChild(instance);
		GetTree().CreateTimer(SpawnRate).Timeout += SpawnEntity;
	}
}

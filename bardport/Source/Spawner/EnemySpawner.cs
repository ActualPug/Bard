using Godot;
using System;
using static System.Formats.Asn1.AsnWriter;

public partial class EnemySpawner : Node2D, ISpawner
{
	[Export]
	public int SpawnerID { get; set; } = 0;
	[Export]
	public PackedScene EntityToSpawn { get; set; }
	[Export]
	public double GraceTime { get; set; } = 5d;
    [Export]
	public double SpawnRate { get; set; } = 1d;
	[Export]
    public Node2D Parent { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetTree().CreateTimer(GraceTime).Timeout += SpawnEntity;
    }

    public void SpawnEntity()
	{
		CharacterBody2D instance = (CharacterBody2D) EntityToSpawn.Instantiate();
		instance.GlobalPosition = GlobalPosition;

		foreach (Node child in instance.GetChildren())
		{
			if (child.GetChildCount() > 0 && child.GetChild(0) is PathingAgent agent)
			{
				agent.SpawnID = SpawnerID;
			}
		}

        Parent.AddChild(instance);
		GetTree().CreateTimer(SpawnRate).Timeout += SpawnEntity;
	}
}

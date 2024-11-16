using Godot;
using Godot.Collections;
using System;
using static System.Formats.Asn1.AsnWriter;

public partial class EnemySpawner : Node2D, ISpawner
{
    [Export]
    public Array<PackedScene> EntitiesToSpawn { get; set; }
    [Export]
    public Array<float> ChancePerEntity { get; set; }
    [Export]
    public float SpawnChance { get; set; } = 1f;
    [Export]
    public double GraceTime { get; set; } = 5d;
    [Export]
    public double SpawnRate { get; set; } = 1d;
    [Export]
    public Node2D Parent { get; set; }

    private RandomNumberGenerator rng = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetTree().CreateTimer(GraceTime).Timeout += SpawnEntity;
    }

    public void SpawnEntity()
    {
        CharacterBody2D instance;
        long enemy;

        if (rng.Randf() > SpawnChance || GetTree().Paused)
        {
            GetTree().CreateTimer(SpawnRate).Timeout += SpawnEntity;
            return;
        }

        enemy = rng.RandWeighted(GodotCollection2CSharp.Array2Array(ChancePerEntity));

        for (int i = 0; i < EntitiesToSpawn.Count; ++i)
        {
            if (i == enemy)
            {
                instance = (CharacterBody2D)EntitiesToSpawn[i].Instantiate();
                instance.GlobalPosition = GlobalPosition;
                Parent.AddChild(instance);
            }
        }

        GetTree().CreateTimer(SpawnRate).Timeout += SpawnEntity;
    }
}

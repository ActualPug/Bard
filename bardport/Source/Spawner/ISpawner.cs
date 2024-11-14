using Godot;
using Godot.Collections;
using System;

public interface ISpawner
{
    Array<PackedScene> EntitiesToSpawn { get; set; }
    Array<float> ChancePerEntity { get; set; }
    public double GraceTime { get; set; }
    double SpawnRate { get; set; }
    Node2D Parent { get; set; }

    void SpawnEntity();
}

using Godot;
using System;

public interface ISpawner
{
    PackedScene EntityToSpawn { get; set; }
    public double GraceTime { get; set; }
    double SpawnRate { get; set; }
    Node2D Parent { get; set; }

    void SpawnEntity();
}

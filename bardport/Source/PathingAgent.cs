using Godot;
using System;
using System.Collections.Generic;

public partial class PathingAgent : Node2D
{
    [Export]
    public int SpawnID = 0;
    [Export]
    public float Speed = 100f;
    [Export]
    public PathManager Pathing;

    private readonly Queue<Vector2> _pathQueue = [];

    public override void _Ready()
    {
        GetTree().CreateTimer(0.5f).Timeout += AcquirePath;
    }

    public override void _Process(double delta)
    {
        if (_pathQueue.Count == 0)
        {
            AcquirePath();
            return;
        }

        Vector2 curPathNode = _pathQueue.Peek();
        Vector2 dir = (curPathNode - GlobalPosition).Normalized();
        GlobalPosition += dir * Speed * (float)delta;

        if ((curPathNode - GlobalPosition).Length() < 5f)
        {
            _pathQueue.Dequeue();
        }
    }

    public void AcquirePath()
    {
        Vector2[] path = Pathing.GetPointPath(SpawnID);

        foreach (Vector2 p in path)
        {
            _pathQueue.Enqueue(p);
        }
    }
}

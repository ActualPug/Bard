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
    public PathManager PathManager;

    private Queue<Vector2> _pathQueue = [];

    public override void _Ready()
    {
        Vector2[] path = PathManager.GetPointPath(SpawnID);

        foreach (Vector2 p in path)
        {
            _pathQueue.Enqueue(p);
        }
    }

    public override void _Process(double delta)
    {
        if (_pathQueue.Count == 0)
        {
            return;
        }

        Vector2 curPathNode = _pathQueue.Peek();
        Vector2 dir = (curPathNode - GlobalPosition).Normalized();
        GlobalPosition += dir * 100f * (float)delta;

        if ((curPathNode - GlobalPosition).Length() < 5f)
        {
            _pathQueue.Dequeue();
        }
    }
}

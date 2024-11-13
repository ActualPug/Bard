using Godot;
using System;
using System.Collections.Generic;

public partial class PathingAgent : Node2D
{
    [Export]
    public int SpawnID { get; set; } = 0;
    [Export]
    public PathManager Pathing { get; set; }
    [Export]
    public Vector2 Offset { get; set; }
    public Vector2 MoveDirection { get; private set; }

    private readonly Queue<Vector2> _pathQueue = [];

    public override void _Ready()
    {
        Pathing = PathManager.CurrentManager;
        AcquirePath();
    }

    public override void _Process(double delta)
    {
        if (_pathQueue.Count == 0)
        {
            AcquirePath();
            MoveDirection = Vector2.Zero;
            return;
        }

        Vector2 curPathNode = _pathQueue.Peek();
        MoveDirection = (curPathNode - GlobalPosition).Normalized();

        if ((curPathNode - GlobalPosition).Length() < 1f)
        {
            _pathQueue.Dequeue();
        }
    }

    public void AcquirePath()
    {
        Vector2[] path = Pathing.GetPointPath(SpawnID);
        int closest = ClosestPointInPath(path);

        for (int i = closest; i < path.Length; ++i)
        {
            _pathQueue.Enqueue(path[i] + Offset);
        }
    }

    private int ClosestPointInPath(Vector2[] path)
    {
        int closest = 0;
        float closestDist = float.PositiveInfinity;
        float currentDist;

        for (int i = 0; i < path.Length; ++i)
        {
            currentDist = (path[i] - GlobalPosition).Length();
            if (currentDist < closestDist)
            {
                closest = i;
                closestDist = currentDist;
            }
        }

        return closest;
    }
}

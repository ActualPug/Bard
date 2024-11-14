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
    public WorldAStar2DGrid Grid { get; set; }
    [Export]
    public Vector2 Offset { get; set; }
    [Export]
    public double PathChangeDelta { get; set; } = 0.1d;
    public Vector2 MoveDirection { get; private set; }

    private readonly Queue<Vector2> _pathQueue = [];
    private bool _pathInvalidated = false;

    public override void _Ready()
    {
        Pathing = PathManager.CurrentManager;
        Grid = WorldAStar2DGrid.CurrentWorldGrid;
        AcquirePath();
    }

    public override void _Process(double delta)
    {
        if (_pathQueue.Count == 0 || _pathInvalidated)
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
        //Vector2[] pathToClosest;
        //Vector2I from, to;
        int closest = ClosestPointInPath(path);

        _pathInvalidated = false;
        GetTree().CreateTimer(PathChangeDelta).Timeout += () => _pathInvalidated = true;

        if (closest == -1)
        {
            return;
        }

        //from = Grid.GetPositionID(path[closest]);
        //to = Grid.GetPositionID(Pathing.TargetPos);

        //pathToClosest = Grid.Grid.GetPointPath(from, to);

        //for (int i = 0; i < pathToClosest.Length; ++i)
        //{
        //    _pathQueue.Enqueue(pathToClosest[i] + Offset);
        //}

        for (int i = closest; i < path.Length; ++i)
        {
            GD.Print(path[i]);
            _pathQueue.Enqueue(path[i] + Offset);
        }
    }

    private int ClosestPointInPath(Vector2[] path)
    {
        int closest = -1;
        float closestDist = float.PositiveInfinity;
        float currentDist;

        for (int i = 0; i < path.Length; ++i)
        {
            currentDist = (path[i] + Offset - GlobalPosition).Length();
            if (currentDist < closestDist)
            {
                closest = i;
                closestDist = currentDist;
            }
        }

        return closest;
    }
}

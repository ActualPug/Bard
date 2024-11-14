using Godot;
using System;
using System.Collections.Generic;

public partial class PathingAgent : Node2D
{
    [Export]
    public EnemyManager Manager { get; set; } = null;
    [Export]
    public WorldAStar2DGrid Grid { get; set; } = null;
    [Export]
    public Vector2 Offset { get; set; }
    [Export]
    public double PathChangeDelta { get; set; } = 1;
    [Export]
    public Node2D Target { get; set; } = null;
    public Vector2 MoveDirection { get; private set; }

    private readonly Queue<Vector2> _pathQueue = [];
    private bool _pathInvalidated = false;

    public override void _Ready()
    {
        Manager ??= EnemyManager.CurrentManager;
        Grid ??= WorldAStar2DGrid.CurrentWorldGrid;

        Target ??= Manager.Target;

        AcquirePath();
    }

    public override void _Process(double delta)
    {
        if (_pathQueue.Count == 0 || _pathInvalidated)
        {
            MoveDirection = Vector2.Zero;
            AcquirePath();
            return;
        }

        Vector2 curPathNode = _pathQueue.Peek();
        MoveDirection = (curPathNode - GlobalPosition).Normalized();

        if ((curPathNode - GlobalPosition).Length() < 8f)
        {
            _pathQueue.Dequeue();
        }
    }

    public void AcquirePath()
    {
        Vector2[] path;
        
        if (!IsInstanceValid(Target))
        {
            return;
        }

        path = Grid.GetPointPath(GlobalPosition, Target.GlobalPosition);

        _pathInvalidated = false;
        GetTree().CreateTimer(PathChangeDelta).Timeout += () => _pathInvalidated = true;

        _pathQueue.Clear();
        for (int i = 0; i < path.Length; ++i)
        {
            _pathQueue.Enqueue(path[i] + Offset);
        }
    }
}

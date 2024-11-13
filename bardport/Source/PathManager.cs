using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;

public partial class PathManager : Node2D
{
    public static PathManager CurrentManager { get; private set; }
    [Export]
    public WorldAStar2DGrid Grid { get; set; }
    [Export]
    public double RecalcDelta { get; set; } = 0.5d;
    [Export]
    public Vector2 TargetPos { get; set; } = Vector2.Zero;

    private Vector2I[] _pathOrigins;
    private Vector2[][] _pathPoints;

    public PathManager() : base()
    {
        CurrentManager = this;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Array<Node> children = GetChildren();
        _pathOrigins = new Vector2I[children.Count];
        _pathPoints = new Vector2[children.Count][];

        for (int i = 0; i < children.Count; ++i)
        {
            _pathOrigins[i] = Grid.GetPositionID(((Node2D)children[i]).GlobalPosition);
        }

        CaclulatePaths();
    }

    private void CaclulatePaths()
    {
        Vector2I origin;

        for (int i = 0; i < _pathOrigins.Length; i++)
        {
            origin = _pathOrigins[i];
            _pathPoints[i] = Grid.Grid.GetPointPath(origin, Grid.GetPositionID(TargetPos));
        }

        GetTree().CreateTimer(RecalcDelta).Timeout += CaclulatePaths;
    }

    public Vector2[] GetPointPath(int id)
    {
        return _pathPoints[id];
    }
}

using Godot;
using Godot.Collections;

public partial class PathManager : Node2D
{
	[Export]
	public Node2D Map { get; set; }
	[Export]
	public double RecalcDelta { get; set; } = 0.5d;
	[Export]
	public Vector2 TargetPos { get; set; }
	public Vector2I[] Paths { get; private set; }
	public Vector2I Walls { get; set; }

	private AStarGrid2D _grid2D = new();
	private Vector2I[] _pathOrigins;
	private Vector2[][] _pathPoints;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Array<Node> children = GetChildren();
		_pathOrigins = new Vector2I[children.Count];
		_pathPoints = new Vector2[children.Count][];

        _grid2D.Region = new(-32, -32, 64, 64);
        _grid2D.CellSize = new(16, 16);
        _grid2D.Update();

        for (int i = 0; i < children.Count; ++i)
        {
            _pathOrigins[i] = Point2ID(((Node2D)children[i]).GlobalPosition);
        }

		CutWallsFromGrid();
		CaclulatePaths();
	}

	public Vector2[] GetPointPath(int id)
	{
		return _pathPoints[id];
	}

	private Vector2I Point2ID(Vector2 point)
	{
        Vector2I id = new()
        {
            X = Float2Id(point.X),
			Y = Float2Id(point.Y)
        };

        return id;
	}

	private int Float2Id(float f)
	{
		return (int) Mathf.Abs(f / _grid2D.CellSize.X);
    }

	private void CutWallsFromGrid()
	{
        Array<Node> children = Map.GetChildren();
		Vector2I[] tiles;
		TileMapLayer layer;
		Rect2I cellRegion;

        for (int i = 0; i < children.Count; ++i)
        {
			if (children[i] is not TileMapLayer)
			{
				continue;
			}

			layer = (TileMapLayer)children[i];

			if (layer.IsInGroup("WallLayer"))
			{
				tiles = GodotCollection2CSharp.Array2Array(layer.GetUsedCells());

				for (int j = 0; j < tiles.Length; ++j)
				{
					cellRegion = new(tiles[i] - new Vector2I(8, 8), new(16, 16));
					_grid2D.FillSolidRegion(cellRegion);
					//_grid2D.SetPointSolid(tiles[j]);
                }
			}
        }
    }

	private void CaclulatePaths()
	{
		Vector2I origin;

        for (int i = 0; i < _pathOrigins.Length; i++)
		{
            origin = _pathOrigins[i];
			_pathPoints[i] = _grid2D.GetPointPath(origin, Point2ID(TargetPos));
		}

        GetTree().CreateTimer(RecalcDelta).Timeout += CaclulatePaths;
    }
}

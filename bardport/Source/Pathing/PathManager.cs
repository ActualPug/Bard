using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class PathManager : Node2D
{
    public static PathManager CurrentManager { get; private set; }
    [Export]
    public WorldAStar2DGrid Grid { get; set; }
    [Export]
    public double RecalcDelta { get; set; } = 0.5d;
    [Export]
    public Vector2 TargetPos { get; set; } = Vector2.Zero;

    [ExportGroup("Spawner Description")]
    [Export]
    public PackedScene EntityToSpawn { get; set; }
    [Export]
    public double GraceTime { get; set; }
    [Export]
    public double SpawnRate { get; set; }
    [Export]
    public Node2D Parent { get; set; }
    [Export]
    public Texture2D DBGTile { get; set; }


    private Vector2I[] _pathOrigins;
    private Vector2[][] _pathPoints;
    private EnemySpawner[] _spawners;
    private readonly List<Sprite2D> _dbgSprites = [];

    public PathManager() : base()
    {
        CurrentManager = this;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddSpawners();
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

        ClearDebugSprites();

        for (int i = 0; i < _pathOrigins.Length; i++)
        {
            origin = _pathOrigins[i];
            _pathPoints[i] = Grid.Grid.GetPointPath(origin, Grid.GetPositionID(TargetPos));

            foreach (Vector2 point in _pathPoints[i])
            {
                Sprite2D sprite = new()
                {
                    Name = point.ToString(),
                    Texture = DBGTile,
                    GlobalPosition = point + new Vector2(8,8)
                };

                _dbgSprites.Add(sprite);
            }
        }

        GetTree().CreateTimer(RecalcDelta).Timeout += CaclulatePaths;
        
        AddDebugSprites();
    }

    private void AddDebugSprites()
    {
        foreach (Sprite2D sprite in _dbgSprites)
        {
            if (sprite.GetParent() != GetParent())
                GetParent().CallDeferred("add_child", sprite);
        }
    }

    private void ClearDebugSprites()
    {
        for (int i = 0; i < _dbgSprites.Count; i++)
        {
            Sprite2D sprite = _dbgSprites[i];
            _dbgSprites.RemoveAt(i);
            sprite.QueueFree();
        }
    }

    private void AddSpawners()
    {
        Vector2[] spawnTiles = Grid.GetPositionsOfTiles(CollectSpawnTiles(Grid.GetSpawnLayers()));
        EnemySpawner[] spawners = new EnemySpawner[spawnTiles.Length];

        for (int i = 0; i < spawnTiles.Length; ++i)
        {
            spawners[i] = new()
            {
                SpawnerID = i,
                EntityToSpawn = EntityToSpawn,
                GraceTime = GraceTime,
                SpawnRate = SpawnRate,
                Parent = Parent,
                GlobalPosition = spawnTiles[i]
            };

            AddChild(spawners[i]);
        }
    }

    private static Vector2I[] CollectSpawnTiles(TileMapLayer[] spawnLayers)
    {
        List<Vector2I> spawnerTiles = [];

        for (int i = 0; i < spawnLayers.Length; ++i)
        {
            spawnerTiles.AddRange(GodotCollection2CSharp.Array2Array(spawnLayers[i].GetUsedCells()));
        }

        return [.. spawnerTiles];
    }

    public Vector2[] GetPointPath(int id)
    {
        return _pathPoints[id];
    }
}

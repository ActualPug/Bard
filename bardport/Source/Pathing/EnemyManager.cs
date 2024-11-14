using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class EnemyManager : Node2D
{
    public static EnemyManager CurrentManager { get; private set; } = null;
    [Export]
    public WorldAStar2DGrid Grid { get; set; }
    [Export]
    public Node2D Target { get; set; }

    [ExportGroup("Spawner Description")]
    [Export]
    public Array<PackedScene> EntitiesToSpawn { get; set; }
    [Export]
    public Array<float> ChancePerEntity { get; set; }
    [Export]
    public double GraceTime { get; set; }
    [Export]
    public double SpawnRate { get; set; }
    [Export]
    public Node2D Parent { get; set; }
    [Export]
    public Texture2D DBGTile { get; set; }

    private EnemySpawner[] _spawners;

    public EnemyManager() : base()
    {
        CurrentManager = this;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddSpawners();
    }

    private void AddSpawners()
    {
        Vector2[] spawnTiles = Grid.GetPositionsOfTiles(CollectSpawnTiles(Grid.GetSpawnLayers()));
        EnemySpawner[] spawners = new EnemySpawner[spawnTiles.Length];
        float chancerPerSpanwer = 1f / spawners.Length;

        for (int i = 0; i < spawnTiles.Length; ++i)
        {
            spawners[i] = new()
            {
                EntitiesToSpawn = EntitiesToSpawn,
                ChancePerEntity = ChancePerEntity,
                SpawnChance = chancerPerSpanwer,
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
}

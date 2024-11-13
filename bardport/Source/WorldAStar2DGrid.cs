using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class WorldAStar2DGrid : Node2D
{
    [Export]
    public Node2D TileMap { get; set; }
    [Export]
    public Vector2I TileSize { get; set; } = new(16, 16);
    [Export]
    public Vector2 CellSize { get; set; } = new(16, 16);
    [Export]
    public Rect2I Region { get; set; } = new(0, 0, 64, 64);
    public AStarGrid2D Grid { get; private set; } = new();

    private readonly List<TileMapLayer> _layers = [];
    private readonly TileMapLayer _totalMap = new();

    public override void _Ready()
    {
        Array<Node> tileMapChildren = TileMap.GetChildren();

        foreach (Node node in tileMapChildren)
        {
            if (node is TileMapLayer layer)
            {
                _layers.Add(layer);
                _totalMap.TileSet = layer.TileSet;
                AddCellsToTotalMap(layer);

                if (layer.IsInGroup("SpawnerLayer"))
                {
                    layer.Hide();
                }
            }
        }

        Grid.Region = Region;
        Grid.CellSize = CellSize;
        Grid.DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never;
        Grid.Update();

        CutWallsFromGrid();
    }

    private void CutWallsFromGrid()
    {
        Vector2I[] tiles;

        foreach (TileMapLayer layer in _layers)
        {
            if (layer.IsInGroup("WallLayer"))
            {
                tiles = GodotCollection2CSharp.Array2Array(layer.GetUsedCells());

                for (int j = 0; j < tiles.Length; ++j)
                {
                    Grid.SetPointSolid(tiles[j]);
                }
            }
        }
    }

    private void AddCellsToTotalMap(TileMapLayer layer)
    {
        Vector2I[] cells = GodotCollection2CSharp.Array2Array(layer.GetUsedCells());

        foreach (Vector2I cell in cells)
        {
            _totalMap.SetCell(cell, sourceId: layer.GetCellSourceId(cell), atlasCoords: layer.GetCellAtlasCoords(cell));
        }
    }

    public Vector2I GetPositionID(Vector2 pos)
    {
        return _totalMap.LocalToMap(pos);
    }
}

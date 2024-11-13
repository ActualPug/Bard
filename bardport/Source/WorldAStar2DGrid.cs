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

    public static Vector2I Vector2VectorI(Vector2 vec)
    {
        return new((int)vec.X, (int)vec.Y);
    }

    public override void _Ready()
    {
        Array<Node> tileMapChildren = TileMap.GetChildren();

        foreach (Node node in tileMapChildren)
        {
            if (node is TileMapLayer layer)
            {
                _layers.Add(layer);
            }
        }

        Grid.Region = Region;
        Grid.CellSize = CellSize;
        Grid.Update();

        CutWallsFromGrid();
    }

    private void CutWallsFromGrid()
    {
        Vector2I[] tiles;
        Rect2I cellRegion;

        foreach (TileMapLayer layer in _layers)
        {
            if (layer.IsInGroup("WallLayer"))
            {
                tiles = GodotCollection2CSharp.Array2Array(layer.GetUsedCells());

                for (int j = 0; j < tiles.Length; ++j)
                {
                    cellRegion = new(tiles[j] - (TileSize / 2), TileSize);
                    Grid.FillSolidRegion(cellRegion);
                    //_grid2D.SetPointSolid(tiles[j]);
                }
            }
        }
    }

    public Vector2I GetPositionID(Vector2 pos)
    {
        return new()
        {
            X = Float2Id(pos.X),
            Y = Float2Id(pos.Y)
        };
    }

    public int Float2Id(float f)
    {
        return (int) (f / CellSize.X);
    }
}

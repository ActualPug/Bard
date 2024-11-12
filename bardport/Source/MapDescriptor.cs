using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class MapDescriptor : Node2D
{
	public Vector2I[] WallIDs { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Array<Node> layers = GetChildren();
		List<Vector2I> vecs = [];
		Array<Vector2I> ids;

		for (int i = 0; i < layers.Count; ++i)
		{
			ids = ((TileMapLayer)layers[i]).GetUsedCellsById();
			for (int j = 0; j < ids.Count; ++i)
			{
				vecs.Add(ids[j]);
			}
		}

		WallIDs = [.. vecs];

		AddToGroup("Map");
	}
}

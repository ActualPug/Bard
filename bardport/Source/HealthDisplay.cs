using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class HealthDisplay : Control
{
    [Export]
    public Health EntityHP { get; set; }
    [Export]
    Array<Texture2D> HeartTextures { get; set; }
    [Export]
    public Control HeartContainer { get; set; }
    [Export]
    public bool LiftToRoot { get; set; } = false;

    private readonly List<TextureRect> _healthSprites = [];
    private int _previousHP = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TextureRect newRect;
        _previousHP = EntityHP.MaxHealth;

        for (int i = 0; i < EntityHP.MaxHealth / HeartTextures.Count; ++i)
        {
            newRect = new()
            {
                Texture = HeartTextures[0],
            };

            _healthSprites.Add(newRect);
            HeartContainer.AddChild(newRect);
        }

        if (LiftToRoot)
        {
            CallDeferred("reparent", GetTree().Root);
        }
    }

    public void DisplayHearts(int curHealth)
    {
        List<int> healthPerHeart = [];

        for (int i = 0; i < curHealth; ++i)
        {
            healthPerHeart.Add(HeartTextures.Count);
        }
    }
}

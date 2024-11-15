using Godot;
using System;

public partial class PauseManager : Control
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            GetTree().Paused = !GetTree().Paused;
        }
    }
}

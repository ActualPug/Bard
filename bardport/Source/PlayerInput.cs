using Godot;
using System;

public partial class PlayerInput : Node2D
{
	public Vector2 MoveDirection { get; private set; } = new(0, 0);
	public Vector2 ShootDirection { get; private set; } = new(0, 0);

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoveDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		ShootDirection = Input.GetVector("shoot_left", "shoot_right", "shoot_up", "shoot_down");
    }
}

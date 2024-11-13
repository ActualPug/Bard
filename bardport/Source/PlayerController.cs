using Godot;
using System;

public partial class PlayerController : Node2D
{
    [Export]
    public KinematicCharacter PlayerCharacter { get; set; }
    [Export]
    public ProjectileThrower PlayerProjThrower { get; set; }

    private PlayerInput _playerInput;

    public override void _Ready()
    {
        _playerInput = GetChild<PlayerInput>(0);
    }

    public override void _Process(double delta)
    {
        PlayerCharacter.Direction = _playerInput.MoveDirection;

        if (_playerInput.ShootDirection != Vector2.Zero)
            PlayerProjThrower.ShootProjectile(_playerInput.ShootDirection, Vector2.Zero);
    }
}

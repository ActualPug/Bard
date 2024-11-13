using Godot;
using System;
using static Godot.TextServer;

public partial class PlayerController : Node2D
{
    [Export]
    public KinematicCharacter PlayerCharacter { get; set; }
    [Export]
    public ProjectileThrower PlayerProjThrower { get; set; }
    [Export]
    public AnimatedSprite2D PlayerSprite { get; set; }

    private PlayerInput _playerInput;
    private string _curAnim = "idle";

    public override void _Ready()
    {
        _playerInput = GetChild<PlayerInput>(0);
    }

    public override void _Process(double delta)
    {
        PlayerCharacter.Direction = _playerInput.MoveDirection;

        if (_playerInput.ShootDirection != Vector2.Zero)
            PlayerProjThrower.ShootProjectile(_playerInput.ShootDirection, Vector2.Zero);

        if (_playerInput.MoveDirection.Length() > 0.05f)
        {
            _curAnim = "walking";
        }
        else
        {
            _curAnim = "idle";
        }

        PlayerSprite.Play(_curAnim);
    }
}

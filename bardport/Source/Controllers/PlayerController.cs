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
    [Export]
    public Health PlayerHealth { get; set; }
    [Export]
    public double InvulnTime { get; set; } = 0.5d;

    private PlayerInput _playerInput;
    private string _curAnim = "idle";

    public override void _Ready()
    {
        _playerInput = GetChild<PlayerInput>(0);
        PlayerHealth.HealthChanged += StartInvuln;
        PlayerHealth.HealthDepleted += Die;
    }

    public override void _Process(double delta)
    {
        PlayerCharacter.Direction = _playerInput.MoveDirection;

        if (_playerInput.ShootDirection != Vector2.Zero)
            PlayerProjThrower.ShootProjectile(_playerInput.ShootDirection);

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

    public void StartInvuln(int curHealth)
    {
        PlayerHealth.Invincible = true;
        GetTree().CreateTimer(InvulnTime).Timeout += () =>
        {
            PlayerHealth.Invincible = false;
            PlayerSprite.Material.Set("shader_parameter/flash_speed", 0);
        };

        PlayerSprite.Material.Set("shader_parameter/flash_speed", 20);
    }

    public void Die()
    {
        if (PlayerSprite.Visible)
        {
            GetTree().CreateTimer(2).Timeout += () => GetTree().ChangeSceneToFile("res://UI/game_over.tscn");
            PlayerSprite.Visible = false;
        }
    }
}

using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed { get; set; } = 40f;
    [Export]
    public AnimatedSprite2D AnimSprite { get; set; } = null;
    [Export]
    public Node2D Thrower { get; set; }
    [Export]
    public double FireRate { get; set; } = 1d;

    private string _curAnim = "idle";
    private Timer _timer = new();

    public override void _Ready()
    {
        if (AnimSprite == null)
        {
            GD.PrintErr("ERROR: No animated sprite!");
            GetTree().Quit();
        }


        _timer.OneShot = true;
        AddChild(_timer);
    }

    public override void _Process(double delta)
    {
        Vector2 direction = Input.GetVector("shoot_left", "shoot_right", "shoot_up", "shoot_down");

        Texture2D cur_sprite = AnimSprite.SpriteFrames.GetFrameTexture(_curAnim, AnimSprite.Frame);
        Vector2 cur_sprite_size = cur_sprite.GetSize();

        if (direction.Length() > 0.05f && _timer.TimeLeft <= 0)
        {
            _timer.Start(FireRate);
            Thrower.Call("ShootProjectile", direction, CalcSpritePos(direction, cur_sprite_size));
        }
    }

    private static Vector2 CalcSpritePos(Vector2 dir, Vector2 spriteSize)
    {
        if (dir.Length() < 0.05f)
            return Vector2.Zero;

        Vector2 pos = new(0, 0);

        if (dir.X > 0)
            pos.X += spriteSize.X - 1;
        else if (dir.X < 0)
            pos.X -= spriteSize.X - 1;


        if (dir.Y > 0)
            pos.Y += spriteSize.Y - 1;
        else if (dir.Y < 0)
            pos.Y -= spriteSize.Y - 1;

        return pos;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

        if (direction.Length() > 0.05f)
        {
            velocity = direction * Speed;
            _curAnim = "walking";
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
            _curAnim = "idle";
        }

        AnimSprite.Play(_curAnim);

        Velocity = velocity;
        MoveAndSlide();
    }
}

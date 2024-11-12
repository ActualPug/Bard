using Godot;
using System;
using static Godot.TextServer;

public partial class ProjectileThrower : Node2D
{
    [Export]
    public Texture2D ProjectileTexture { get; set; }

    [Export]
    public float Speed { get; set; } = 0f;

    [Export]
    public double LifeTime { get; set; } = 15d;

    private int _poolSize = 1000;

    private Projectile[] _projectilePool;
    private int _curProj = 0;

    public override void _Ready()
    {
        _projectilePool = new Projectile[_poolSize];
        PoolProjectiles();
    }

    public void ShootProjectile(Vector2 dir, Vector2 offset)
    {
        Projectile proj = GetFromPool();

        proj.Speed = Speed;
        proj.Direction =  dir;
        proj.GlobalPosition = GlobalPosition + offset;
        GetParent().GetParent().AddChild(proj);
    }

    private Projectile GetFromPool()
    {
        return _projectilePool[_curProj++];
    }

    private void PoolProjectiles()
    {
        _curProj = 0;

        for (int i = 0; i < _poolSize; ++i)
        {
            _projectilePool[i] = CreateProjectile();
        }
    }

    private Projectile CreateProjectile()
    {
        Projectile a2d;

        RectangleShape2D rect = new()
        {
            Size = ProjectileTexture.GetSize()
        };

        CollisionShape2D shape = new()
        {
            Shape = rect
        };

        Sprite2D sprite = new()
        {
            Texture = ProjectileTexture
        };

        a2d = new()
        {
            CollisionLayer = 2,
            CollisionMask = 1,
        };

        a2d.AddChild(shape);
        a2d.AddChild(sprite);

        a2d.LifeTime = LifeTime;

        return a2d;
    }
}

using Godot;
using System;
using static Godot.TextServer;

public partial class ProjectileThrower : Node2D
{
    [Export]
    public PackedScene ProjectileScene { get; set; }
    [Export]
    public double FireRate { get; set; } = 1d;
    [Export]
    public uint Layer { get; set; } = 1;
    [Export]
    public uint Mask { get; set; } = 1;

    private int _poolSize = 1000;
    private Projectile[] _projectilePool;
    private int _curProj = 0;
    private bool _ready = true;

    public override void _Ready()
    {
        _projectilePool = new Projectile[_poolSize];
        PoolProjectiles();
    }

    public void ShootProjectile(Vector2 dir)
    {
        Projectile proj;
        
        if (!_ready)
            return;

        _ready = false;

        proj = GetFromPool();
        proj.Direction =  dir;
        proj.GlobalPosition = GlobalPosition;

        GetParent().GetParent().AddChild(proj);
        GetTree().CreateTimer(FireRate).Timeout += () => _ready = true;
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
        Projectile proj = ProjectileScene.Instantiate<Projectile>();

        proj.Mask = Mask;
        proj.Layer = Layer;

        return proj;
    }
}

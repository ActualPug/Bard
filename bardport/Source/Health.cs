using Godot;
using System;

public partial class Health : Node2D
{
    [Export]
    public int MaxHealth { get; set; } = 1;
    [Export]
    public bool Invincible { get; set; } = false;
    
    private int _health = 1;

    [Signal]
    public delegate void HealthChangedEventHandler(int curHealth);
    [Signal]
    public delegate void HealthDepletedEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _health = MaxHealth;
        EmitSignal(SignalName.HealthChanged, _health);
        GetParent().AddToGroup("HasHealth");
    }

    public void Heal(int hp)
    {
        _health += hp;
        EmitSignal(SignalName.HealthChanged, _health);
    }

    public void TakeDamage(int damage)
    {
        if (Invincible)
        {
            return;
        }

        _health -= damage;
        EmitSignal(SignalName.HealthChanged, _health);

        if (_health <= 0)
        {
            EmitSignal(SignalName.HealthDepleted);
        }
    }
}

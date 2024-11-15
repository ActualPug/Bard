using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class DamageArea : Area2D
{
    [Export]
    public int Damage { get; set; } = 1;
    [Export]
    public bool DestroyOnHit { get; set; } = false;

    private readonly List<Health> _target = [];

    [Signal]
    public delegate void DamageDoneEventHandler();
    [Signal]
    public delegate void WasDestroyedEventHandler();

    public override void _Ready()
    {
        BodyEntered += DamageAreaEntered;
        BodyExited += DamageAreaExited;
    }

    public override void _Process(double delta)
    {
        foreach (Health health in _target)
        {
            if (!IsInstanceValid(health))
            {
                return;
            }

            health.TakeDamage(Damage);

            EmitSignal(SignalName.DamageDone);
        }
    }

    private void DamageAreaEntered(Node body)
    {
        Array<Node> children;

        if (body.IsInGroup("HasHealth"))
        {
            children = body.GetChildren();

            for (int i = 0; i < children.Count; ++i)
            {
                if (children[i] is Health health)
                {
                    _target.Add(health);
                }
            }
        } else if (DestroyOnHit)
        {
            QueueFree();
            EmitSignal(SignalName.WasDestroyed);
        }
    }

    private void DamageAreaExited(Node body)
    {
        Array<Node> children;

        if (body.IsInGroup("HasHealth"))
        {
            children = body.GetChildren();

            for (int i = 0; i < children.Count; ++i)
            {
                if (children[i] is Health health)
                {
                    _target.Remove(health);
                }
            }
        }
    }
}

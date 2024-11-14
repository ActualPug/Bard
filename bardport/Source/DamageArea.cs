using Godot;
using Godot.Collections;
using System;

public partial class DamageArea : Area2D
{
    [Export]
    public int Damage { get; set; } = 1;

    [Signal]
    public delegate void DamageDoneEventHandler();

    public override void _Ready()
    {
        BodyEntered += DamageAreaEntered;
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
                    health.TakeDamage(Damage);
                }
            }
        }

        EmitSignal(SignalName.DamageDone);
    }
}

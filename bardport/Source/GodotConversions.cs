using Godot;
using System;

public static class GodotConversions
{
    public static Vector2I Vector2VectorI(Vector2 vec)
    {
        return new((int)vec.X, (int)vec.Y);
    }
}

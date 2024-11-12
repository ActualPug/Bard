using Godot;
using Godot.Collections;
using System;

public class GodotCollection2CSharp
{
    public static T[] Array2Array<[MustBeVariant] T>(Array<T> arr)
    {
        T[] newArr = new T[arr.Count];

        for (int i = 0; i < arr.Count; ++i)
        {
            newArr[i] = arr[i];
        }

        return newArr;
    }
}

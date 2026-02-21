

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public static class MätaDistansochmer
{
    public static List<Vector2> Vectors = new List<Vector2>();
    public static void Update()
    {
        if (MouseHelper.Click())
        {
            Vectors.Add(MouseHelper.CurretPosition());
            WriteVectors();
        }
    }
    private static void WriteVectors()
    {
        foreach(Vector2 v in Vectors)
        {
            Console.Write(v+" ");
        }
        Console.WriteLine();
    }
}

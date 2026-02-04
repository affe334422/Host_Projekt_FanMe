

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class AxeCorner
{
    public Vector2 Corner;
    public float Distance;
    public float Degrees;
    public AxeCorner(Vector2 Corner,float Distance,float Degrees)
    {
        this.Corner=Corner;
        this.Degrees=Degrees;
        this.Distance=Distance;
    }
}
public class _Axe_1
{
    private List<AxeCorner> Corners = new List<AxeCorner>();
    List<Vector2> Points;
    
    public Vector2 Centrum;

    public List<AxeCorner> corners{get=>Corners;}

    public _Axe_1(Vector2 centrum, float handleWidth, float handleHeight, float bladeWidth, float bladeHeight)
    {
        Centrum = centrum;

        // ---- 1. Areas ----
        float handleArea = handleWidth * handleHeight;
        float bladeArea  = bladeWidth  * bladeHeight;

        // ---- 2. Densities ----
        float handleDensity = 0.6f;  // wood
        float bladeDensity  = 7.8f;  // steel

        // ---- 3. Masses ----
        float handleMass = handleArea * handleDensity;
        float bladeMass  = bladeArea  * bladeDensity;

        // ---- 4. Centers ----
        float handleCenterY = handleHeight / 2f;
        float bladeCenterY  = handleHeight + bladeHeight / 2f;

        // ---- 5. Center of mass (CORRECT PHYSICS) ----
        float totalMass = handleMass + bladeMass;
        float comY = (handleMass * handleCenterY +bladeMass  * bladeCenterY) / totalMass;


        // ---- 4. Half-widths ----
        float hwHandle = handleWidth / 2f;
        float hwBlade  = bladeWidth  / 2f;

        // ---- 5. Corners ----

        // Blade
        AddCorner(hwBlade,  handleHeight + bladeHeight - comY);
        AddCorner(-hwBlade, handleHeight + bladeHeight - comY);
        AddCorner(hwBlade,  handleHeight - comY);
        AddCorner(-hwBlade, handleHeight - comY);

        // Handle
        AddCorner(hwHandle,  handleHeight - comY);
        AddCorner(-hwHandle, handleHeight - comY);
        AddCorner(hwHandle,  -comY);
        AddCorner(-hwHandle, -comY);

    }
    private void AddCorner(float x, float y)
    {
        Vector2 newPoint = Centrum + new Vector2(x, y);
        Corners.Add(new AxeCorner( newPoint,new Vector2(x,y).Length(),(float)Math.Atan2(newPoint.Y-Centrum.Y,newPoint.X-Centrum.X)));
    }
    
    public void Update()
    {
        foreach(AxeCorner corn in corners)
        {
            corn.Corner = new Vector2(Centrum.X-corn.Distance*(float)Math.Cos(corn.Degrees),Centrum.Y-corn.Distance*(float)Math.Sin(corn.Degrees));
        }
    }


    private List<Vector2> GetAxes(List<Vector2> points)
    {
        List<Vector2> axes = new List<Vector2>();

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 p1 = points[i];
            Vector2 p2 = points[(i + 1) % points.Count];

            Vector2 edge = p2 - p1;
            Vector2 normal = new Vector2(-edge.Y, edge.X);
            normal.Normalize();

            axes.Add(normal);
        }

        return axes;
    }
    private void Project(List<Vector2> points, Vector2 axis, out float min, out float max)
    {
        float dot = Vector2.Dot(points[0], axis);
        min = max = dot;

        foreach (Vector2 p in points)
        {
            dot = Vector2.Dot(p, axis);
            if (dot < min) min = dot;
            if (dot > max) max = dot;
        }
    }
    private void Project(Vector2 points, Vector2 axis, out float min, out float max)
    {
        float dot = Vector2.Dot(points, axis);
        min = max = dot;

        dot = Vector2.Dot(points, axis);
        if (dot < min) min = dot;
        if (dot > max) max = dot;
        
    }
    public bool Intersects(List<Vector2> other)
    {
        List<Vector2> a = corners.Select(c => c.Corner).ToList();
        List<Vector2> b = other;

        List<Vector2> axes = GetAxes(a);
        axes.AddRange(GetAxes(b));

        foreach (Vector2 axis in axes)
        {
            Project(a, axis, out float minA, out float maxA);
            Project(b, axis, out float minB, out float maxB);

            if (maxA < minB || maxB < minA)
                return false; // separating axis found
        }

        return true;
    }
    public bool Intersects(Vector2 other)
    {
        List<Vector2> a = corners.Select(c => c.Corner).ToList();

        List<Vector2> axes = GetAxes(a);
        axes.Add(other);

        foreach (Vector2 axis in axes)
        {
            Project(a, axis, out float minA, out float maxA);
            Project(other, axis, out float minB, out float maxB);

            if (maxA < minB || maxB < minA)
                return false; // separating axis found
        }

        return true;
    }

    // Optional: angle debug
    public void WhatDegrees()
    {
        for (int i = 0; i < Corners.Count; i++)
        {
            Console.WriteLine($"Corner {i}: {Corners[i].Degrees}°, r={Corners[i].Distance}");
        }
    }
    public List<MinRectangle> ForDraw
    {
        get
        {
            List<MinRectangle> mins = new List<MinRectangle>();
            mins.Add(new MinRectangle(Centrum, 10, 10));
            foreach (AxeCorner v in Corners)
                mins.Add(new MinRectangle(v.Corner, 10, 10));
            return mins;
        }
    }

}

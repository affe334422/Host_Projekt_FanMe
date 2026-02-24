
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class _NyaBotar
{
    private Vector2 Position;
    private float Rotation; // till Kortleken eller till centrum.
    public _HandMedKort MinaKort;
    public void Add(_MinaNyaKort kort)
    {
        if (kort != null)
        {
            MinaKort.Add(kort);
            PlaseraKorten();
        }
    }
    public void AddRange(List<_MinaNyaKort> korts)
    {
        if (korts != null)
        {
            MinaKort.Listmedkort.AddRange(korts);
            PlaseraKorten();
        }
    }
    public _NyaBotar(Vector2 Position, Vector2 CentrumAvPlan)
    {
        this.Position=Position;
        Rotation = (float)Math.Atan2(CentrumAvPlan.Y-Position.Y,CentrumAvPlan.X-Position.X);
        MinaKort=new _HandMedKort();
    }

    private int Distans = 400;

    public void PlaseraKorten()
    {
        int count = MinaKort.Listmedkort.Count; // antal kort

        if (count == 0){return;}

        // avstånd mellan kort
        float spacing = Distans / Math.Max(count - 1, 1);

        // begränsa spacing så det inte blir för stort
        spacing = Math.Min(spacing, MinaKort.Listmedkort[0].width); // t.ex kortbredd

        float totalLength = spacing * (count - 1);

        for (int i = 0; i < count; i++)
        {
            float offset = -totalLength / 2 + i * spacing;

            Vector2 localPos = new Vector2(offset, 0);

            Vector2 rotatedPos =
                new Vector2(
                    (float)(localPos.X * Math.Cos(Rotation+MathHelper.ToRadians(90))),
                    (float)(localPos.X * Math.Sin(Rotation+MathHelper.ToRadians(90)))
                );

            Vector2 finalPos = Position + rotatedPos;
            _MinaNyaKort min = MinaKort.Listmedkort[i];
            if (min.ismoving)
            {
                //min.MoveTo(finalPos,min.TimeitShouldTake-min.ElapsedTime);
            }
            else
            {
                min.MoveTo(finalPos,Rotation);
            }
        }
    }

    public Vector2 position{get=>Position;}
    public float rotation{get=>Rotation;}

    public void Update()
    {
        MinaKort.Update();
    }
    public play_kort BotOchKort;
    public play_kort botochkort{get=>BotOchKort;}
    // kan skapa en stopwatch för att göra så att de "tänker" längsammare. göra det en random int när man skappar boten.
    private int Tankesätt = 0; // ändra den så tanke sättet sätts när den skappas eller beroende på svåroghets graden.
    public int TankeSätt{get=>Tankesätt;}
}

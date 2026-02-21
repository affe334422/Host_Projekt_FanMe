
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class _NyaBotar
{
    private Vector2 Position;
    private float Rotation; // till Kortleken eller till centrum.
    public _HandMedKort MinaKort;
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
                min.MoveTo(finalPos,min.TimeitShouldTake-min.ElapsedTime);
            }
            else
            {
                min.MoveTo(finalPos);
            }
        }
    }

    public Vector2 position{get=>Position;}
    public float rotation{get=>Rotation;}

    public void Update()
    {
        foreach(_MinaNyaKort K in MinaKort.Listmedkort)
        {
            if(K.ismoving){K.MoveToNewPos();}
        }
    }
    private _NyaBotar VemDufrågar = null;
    private _MinaNyaKort DuFrågarefter= null;
    // kan skapa en stopwatch för att göra så att de "tänker" längsammare. göra det en random int när man skappar boten.
    private int TankeSätt = 0; // ändra den så tanke sättet sätts när den skappas eller beroende på svåroghets graden.
    private int[] KortsVärde = {2,3,4,5,6,7,8,9,10,11,12,13,14};
    public play_kort FrågaEfterKort() // ändra den så play_kort kan användas bättre som exempel en extern fråga efter.
    {
        if (TankeSätt == 1) // fråga efter den du har mäst av alltid
        {
            DuFrågarefter = VilkenHarDuMestAV();
            return new play_kort(null,DuFrågarefter);
        }
        if(TankeSätt == 2)
        {
            return HarNågonDetDuHar();
        }
        return new play_kort(null,MinaKort.Listmedkort[SmåHjälpmedel.ran.Next(0,MinaKort.Listmedkort.Count)]);
    }
    private _MinaNyaKort VilkenHarDuMestAV() // fråga efter den du har mest av
    {
        _MinaNyaKort frågaefter = null;
        int antalavdenmedhögst = 0;
        foreach(int i in KortsVärde)
        {
            int b = MinaKort.HurMångaLika(i);
            if (antalavdenmedhögst < b)
            {
                antalavdenmedhögst = b;
                frågaefter = MinaKort.GetRefkort(i);
            }
        }
        return frågaefter;
    } 
    private play_kort HarNågonDetDuHar()
    {
        play_kort p = HiveMinnet.VemHarEttLikakortSomDU(this,MinaKort);
        if (p == null)
        {
            return new play_kort(null,VilkenHarDuMestAV());
        }
        VemDufrågar = p.VemÄgerKortet;
        DuFrågarefter = p.KortetDeÄger;
        return p;
    }
}

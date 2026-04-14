
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class _NyaBotar
{
    public _NyaBotar(Vector2 Position, Vector2 CentrumAvPlan)
    {
        this.Position=Position;
        Rotation = (float)Math.Atan2(CentrumAvPlan.Y-Position.Y,CentrumAvPlan.X-Position.X);
        FyraHögarnaRot+=Rotation;
        FyraHögarPos=Position+new Vector2((float)Math.Cos(FyraHögarnaRot)*DistansFrånPos,(float)Math.Sin(FyraHögarnaRot)*DistansFrånPos);
        InteraktWithbot = new MinRotRect(Rotation,Position,200,Distans+200);
        MinaKort=new _HandMedKort();
    }
    private MinRotRect InteraktWithbot;
    public MinRotRect InteraktWithBot{get=>InteraktWithbot;}
    public bool Contains(Vector2 Punkt)
    {
        return InteraktWithbot.Contains(Punkt);
    }
    
    private Vector2 Position;
    private float Rotation; // till Kortleken eller till centrum.
    public Vector2 position{get=>Position;}
    public float rotation{get=>Rotation;}
    public _HandMedKort MinaKort; // spara vilka kort
    public _HandMedKort FyraHögarna = new _HandMedKort(); // ska sparas också.
    public Vector2 FyraHögarPos;
    private float FyraHögarnaRot = MathHelper.ToRadians(20);
    private int DistansFrånPos = 200;

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
    
    private int Distans = 400; // Settings
    public int Poäng = 0; // ska vara sparad
    private bool DennaSpelareVald = false;
    public bool DennaSpelVald{get=>DennaSpelareVald;}
    public void DennaspelareVald()
    {
        if (InteraktWithbot.Contains(MouseHelper.CurretPosition()))
        {
            if(MouseHelper.Click()){
                DennaSpelareVald = true;
                UsedAsKonstants.DuHarValtSpelare=true;
            }
            return;
        }
        DennaSpelareVald=false;
    }
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
    public void Update()
    {
        if(!UsedAsKonstants.DuHarValtSpelare){DennaSpelareVald=false;}
        MinaKort.Update();
        FyraHögarna.Update();
    }
    public play_kort BotOchKort;
    public play_kort botochkort{get=>BotOchKort;}
    // kan skapa en stopwatch för att göra så att de "tänker" längsammare. göra det en random int när man skappar boten.
    private int Tankesätt = 0; // ändra den så tanke sättet sätts när den skappas eller beroende på svåroghets graden.
    public int TankeSätt{get=>Tankesätt;}



}

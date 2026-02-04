
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class _stick
{
    private Vector2 Start;
    private Vector2 End;
    public Vector2 Centrum;
    private float MassCentrum;
    private float Leangth;
    private float Width;
    public float rotation = 0;
    public _stick(float Leangth, float Width,Vector2 Centrum,float MassCentrum)
    {
        this.Centrum=Centrum;
        this.MassCentrum = MassCentrum;
        this.Leangth=Leangth;
        this.Width=Width;
        End=new Vector2(Centrum.X,Centrum.Y+Leangth*MassCentrum);
        Start=new Vector2(Centrum.X,Centrum.Y-Leangth*(1-MassCentrum));
    }

    public List<MinRectangle> ForDraw()
    {
        List<MinRectangle> mins = new List<MinRectangle>();
        int Hurmånga = (int)(Leangth/Width+0.5f);
        Start=new Vector2(Centrum.X-Leangth*MassCentrum*(float)Math.Cos(MathHelper.ToRadians(rotation)),Centrum.Y-Leangth*MassCentrum*(float)Math.Sin(MathHelper.ToRadians(rotation)));
        End=new Vector2(Centrum.X+Leangth*(1-MassCentrum)*(float)Math.Cos(MathHelper.ToRadians(rotation)),Centrum.Y+Leangth*(1-MassCentrum)*(float)Math.Sin(MathHelper.ToRadians(rotation)));
        Vector2 vinkel = new Vector2((float)Math.Cos(MathHelper.ToRadians(rotation)),(float)Math.Sin(MathHelper.ToRadians(rotation)));
        for(int i = 0; i < Hurmånga; i++)
        {
            mins.Add(new MinRectangle(Start+(Width*vinkel*i),(int)Width,(int)Width));
        }
        return mins;
    }
}

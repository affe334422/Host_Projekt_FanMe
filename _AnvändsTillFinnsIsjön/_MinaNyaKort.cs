
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class _MinaNyaKort : MinRotRect
{
    public _MinaNyaKort(Texture2D Fram, Texture2D Bak, int Vadförkort, int x, int y, int width,int height) : base(x,y,width,height)
    {
        this.Fram = Fram;
        this.Bak = Bak;
        this.VadFörKort = Vadförkort;
    }
    public _MinaNyaKort(Texture2D Fram, Texture2D Bak, int Vadförkort, Vector2 xy,int width,int height) : base(xy,width,height)
    {
        this.Fram = Fram;
        this.Bak = Bak;
        this.VadFörKort = Vadförkort;
    }
    public _MinaNyaKort(Texture2D Fram, Texture2D Bak, int Vadförkort, int x, int y, int width,int height,float TimeItShouldTake) : base(x,y,width,height)
    {
        this.Fram = Fram;
        this.Bak = Bak;
        this.VadFörKort = Vadförkort;
        this.TimeItShouldTake = TimeItShouldTake;
    }
    public _MinaNyaKort(Texture2D Fram, Texture2D Bak, int Vadförkort, Vector2 xy,int width,int height,float TimeItShouldTake) : base(xy,width,height)
    {
        this.Fram = Fram;
        this.Bak = Bak;
        this.VadFörKort = Vadförkort;
        this.TimeItShouldTake = TimeItShouldTake;
    }
    public _MinaNyaKort(int Vadförkort, int x, int y, int width,int height) : base(x,y,width,height)
    {
        this.VadFörKort = Vadförkort;
    }
    public _MinaNyaKort(int Vadförkort, Vector2 xy,int width,int height) : base(xy,width,height)
    {
        this.VadFörKort = Vadförkort;
    }
    public _MinaNyaKort(int Vadförkort, int x, int y, int width,int height,float TimeItShouldTake) : base(x,y,width,height)
    {
        this.VadFörKort = Vadförkort;
        this.TimeItShouldTake = TimeItShouldTake;
    }
    public _MinaNyaKort(int Vadförkort, Vector2 xy,int width,int height,float TimeItShouldTake) : base(xy,width,height)
    {
        this.VadFörKort = Vadförkort;
        this.TimeItShouldTake = TimeItShouldTake;
    }

    // tar bort några av dem sen just nu är de för fel sökning.

    private Texture2D Fram;
    private Texture2D Bak;
    public bool VilkenTexture = true;
    public Texture2D Texture
    {
        get
        {
            if (VilkenTexture)
            {
                return Fram;
            }
            return Bak;
        }
    }
    private int VadFörKort; // ex spader 2 = 2 och hjärter dam = 12
    public int vadförkort{get=>VadFörKort;}


    // Flytta kort när musen rör den.
    public bool MouseRörKort = false; // Använd den för att välja kort också.
    private int KortOffset = 20;
    public Vector2 NärMusRör
    {
        get
        {
            if(!MouseRörKort){return Vector2.Zero;}
            float r = MathHelper.ToRadians(90);
            return new Vector2((float)Math.Cos(Rotation-r)*KortOffset,(float)Math.Sin(Rotation-r)*KortOffset);
        }
    }



    private bool IsMoving = false; // kolla på den om du ska göra movetonewpos().
    public bool ismoving{get=>IsMoving;}
    public bool stopMoving{set=>IsMoving=value;}
    private float TimeItShouldTake = 1f;
    private float TempTimeItShouldTake;
    private bool TempTimeGo = false;
    private float elapsedTime;
    public float ElapsedTime{get=>elapsedTime;}
    public float TimeitShouldTake{get=>TimeItShouldTake;set=>TimeItShouldTake=value;}
    private float StartRotation;
    private float EndRotation;
    private Vector2 startPos;
    private Vector2 endPos;
    public void MoveTo(Vector2 NewPos,float NewRot)
    {
        StartRotation = Rotation;
        EndRotation = NewRot+MathHelper.ToRadians(90);
        IsMoving=true;
        startPos = centrum;
        endPos=NewPos;
        elapsedTime = 0;
    }
    public void MoveTo(Vector2 NewPos,float NewRot,float TempTid)
    {
        StartRotation = Rotation;
        EndRotation = NewRot+MathHelper.ToRadians(90);
        IsMoving=true;
        startPos = centrum;
        endPos=NewPos;
        TempTimeItShouldTake=TempTid;
        TempTimeGo=true;
        elapsedTime = 0;
    }
    public void MoveToNewPos()
    {
        elapsedTime += 0.01666f;
        if (TempTimeGo)
        {
            if (elapsedTime > TempTimeItShouldTake)
            {
                IsMoving=false;
                centrum = endPos;
                Rotation=EndRotation;
                TempTimeGo=false;
                return;
            }
        }
        else if (elapsedTime > TimeItShouldTake)
        {
            IsMoving=false;
            centrum = endPos;
            Rotation=EndRotation;
            return;
        }

        // Clamp elapsedTime to totalTime
        float t;
        if (TempTimeGo)
        {
            t = elapsedTime / TempTimeItShouldTake;
        }
        else{
            t = elapsedTime / TimeItShouldTake;
        }

        // Cubic ease in/out: smooth start and stop
        float smoothT = 3*t*t - 2*t*t*t;
        // Calculate current position
        Rotation = StartRotation + (EndRotation - StartRotation) * smoothT;
        centrum = startPos + (endPos - startPos) * smoothT;
    }
}



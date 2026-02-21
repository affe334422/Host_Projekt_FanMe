
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
    // ändrar den till en array av texture2d för att kunna bestämma lättare vilken av dem som ska vissas. med en int 0 eller 1.
    private int VadFörKort; // ex spader 2 = 2 och hjärter dam = 12
    public int vadförkort{get=>VadFörKort;}




    private bool IsMoving = false; // kolla på den om du ska göra movetonewpos().
    public bool ismoving{get=>IsMoving;}
    private float TimeItShouldTake = 5f;
    private float TempTimeItShouldTake;
    private bool TempTimeGo = false;
    private float elapsedTime;
    public float ElapsedTime{get=>elapsedTime;}
    public float TimeitShouldTake{get=>TimeItShouldTake;set=>TimeItShouldTake=value;}
    private Vector2 startPos;
    private Vector2 endPos;
    public void MoveTo(Vector2 NewPos)
    {
        IsMoving=true;
        startPos = centrum;
        endPos=NewPos;
        elapsedTime = 0;
    }
    public void MoveTo(Vector2 NewPos,float TempTid)
    {
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
                TempTimeGo=false;
                return;
            }
        }
        else if (elapsedTime > TimeItShouldTake)
        {
            IsMoving=false;
            centrum = endPos;
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
        centrum = startPos + (endPos - startPos) * smoothT;
    }
}




using Microsoft.Xna.Framework;

public class _MinaNyaKort : MinRectangle
{
    public _MinaNyaKort(int x, int y, int width,int height) : base(x,y,width,height)
    {
        
    }
    public _MinaNyaKort(Vector2 xy,int width,int height) : base(xy,width,height)
    {
        
    }
    public _MinaNyaKort(int x, int y, int width,int height,float TimeItShouldTake) : base(x,y,width,height)
    {
        this.TimeItShouldTake = TimeItShouldTake;
    }
    public _MinaNyaKort(Vector2 xy,int width,int height,float TimeItShouldTake) : base(xy,width,height)
    {
        this.TimeItShouldTake = TimeItShouldTake;
    }



    private bool IsMoving = false;
    public bool ismoving{get=>IsMoving;}
    private float TimeItShouldTake = 5f;
    private float elapsedTime;
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
    public void MoveToNewPos()
    {
        elapsedTime += 0.01666f;
        if (elapsedTime > TimeItShouldTake)
        {
            IsMoving=false;
            centrum = endPos;
            return;
        }

        // Clamp elapsedTime to totalTime
        float t = elapsedTime / TimeItShouldTake;

        // Cubic ease in/out: smooth start and stop
        float smoothT = 3*t*t - 2*t*t*t;
        // Calculate current position
        centrum = startPos + (endPos - startPos) * smoothT;
    }
}

public class _FromAtoB 
{
    // använd denna för att animera korten.
    private float TimeItShouldTake;   // seconds it should take to move
    private float elapsedTime = 0;  // keep track of time 
    private Vector2 startPos;
    private Vector2 endPos;
    

    public _FromAtoB(Vector2 startPos, Vector2 endPos, float TimeItShouldTake)
    {
        this.TimeItShouldTake = TimeItShouldTake;
        this.startPos = startPos;
        this.endPos = endPos;
    }
    public Vector2 NewPosition()
    {
    // Update elapsed time
        elapsedTime += 0.01666f;
        if (elapsedTime > TimeItShouldTake)
        {
            
        }

        // Clamp elapsedTime to totalTime
        float t = elapsedTime / TimeItShouldTake;

        // Cubic ease in/out: smooth start and stop
        float smoothT = 3*t*t - 2*t*t*t;
        // Calculate current position
        return startPos + (endPos - startPos) * smoothT;
        // Move your block to currentPos
    }
}

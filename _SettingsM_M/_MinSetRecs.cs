
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class _MinSetRecs : MinRectangle
{
    public bool TrueFalse;
    public Color Color
    {
        get
        {
            if (TrueFalse)
            {
                return Color.Green;
            }
            else
            {
                return Color.Red;
            }
        }
    }
    public _MinSetRecs(bool bol, int x, int y, int width,int height) : base(x,y,width,height)
    {
        TrueFalse=bol;
    }
    public _MinSetRecs(bool bol, MinRectangle a) : base(a.centrum, a.rec.Width, a.rec.Height)
    {
        TrueFalse=bol;
    }
    
    
    bool hasntChanged = true;
    public bool Update()
    {
        
        if (MouseHelper.IsHolding(rec))
        {
            if(hasntChanged){
                hasntChanged = false;
                TrueFalse = !TrueFalse;
            }
        }
        else
        {
            hasntChanged = true;
        }

        if (MouseHelper.IsHovering(rec))
        {
            return true;
        }
        return false;
    }
    

}

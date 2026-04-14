
using Microsoft.Xna.Framework;

public class _MinGenSetRecs : MinRectangle
{
    public int Value;
    public _MinGenSetRecs(int Value, int x, int y, int width,int height) : base(x,y,width,height)
    {
        this.Value = Value;
    }
    public _MinGenSetRecs(int Value, MinRectangle a) : base(a.centrum, a.rec.Width, a.rec.Height)
    {
        this.Value = Value;
    }
    public Color Color = Color.LightBlue;
    private bool hasntChanged = true;
    private float TidsGräns = 500;
    public bool Update()
    {
        if (rec.Contains(MouseHelper.CurretPosition()))
        {
            if(hasntChanged||MouseHelper.TimePressed().ElapsedMilliseconds>TidsGräns){
                Value += centrum.X<MouseHelper.CurretPosition().X ? 1:-1;
            }
            hasntChanged = false;
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

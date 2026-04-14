
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class _FinnsISjön : _GameRunSetup
{
    public _FinnsISjön(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        Vector2 v = new Vector2(400,0);
        P1 = new _NyaBotar(CentrumAvPlan-v,CentrumAvPlan);
        P2 = new _NyaBotar(CentrumAvPlan+v,CentrumAvPlan);
    }
    _HandMedKort KortLek;
    Vector2 CentrumAvPlan = new Vector2(900,500);
    _NyaBotar P1;
    _NyaBotar P2;
    public override void Update(GameTime gameTime)
    {
        
    }
    public override void Draw()
    {
        
    }
}

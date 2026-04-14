using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Gamebeta_0_1 : _GameRunSetup
{
    public Gamebeta_0_1(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        
    }
    _GeneriskSetingsRecs Settings = new _GeneriskSetingsRecs("_TestTextFil_");
    
    bool keypress = true;
    Moment Momen = Moment.Run;
    enum Moment
    {
        Run,
        Settings,
        Mäta
    }
    public override void Update(GameTime gameTime)
    {
        kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.Escape))
        {
            Settings.Save();
            Exit = true;
        }

        if (kstate.IsKeyDown(Keys.S)&&keypress)
        {
            keypress=false;
            Momen=Moment.Settings;
        }
        if (kstate.IsKeyDown(Keys.R)&&keypress)
        {
            keypress=false;
            Settings.Save();
            Momen=Moment.Run;
        }
        if (kstate.IsKeyDown(Keys.M) && keypress)
        {
            keypress=false;
            Momen=Moment.Mäta;
        }
        if (Momen == Moment.Settings)
        {
            Settings.Update();
        }
        if (Momen == Moment.Mäta)
        {
            
        }








        if (kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.R))
        {
            keypress=true;
        }
    }
    public override void Draw()
    {
        _spriteBatch.Begin();
            if(Momen == Moment.Settings||Momen == Moment.Mäta){
                _spriteBatch.Draw(texture,Settings.rec,Color.Gray);
                foreach(_MinGenSetRecs setrec in Settings.Settings){
                    _spriteBatch.Draw(texture,setrec.rec,setrec.Color);
                }
            }
        _spriteBatch.End();
    }
}

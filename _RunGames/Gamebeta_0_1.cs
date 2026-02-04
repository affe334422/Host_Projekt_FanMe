using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Gamebeta_0_1 : _GameRunSetup
{
    public Gamebeta_0_1(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        
    }
    _SettingBeta Settings = new _SettingBeta("_Settings0_1.txt");
    MinRectangle mmouse = new MinRectangle(0,0,10,10);
    bool mousepress = true;
    bool keypress = true;
    Moment Momen = Moment.Run;
    enum Moment
    {
        Run,
        Settings
    }
    public override void Update(GameTime gameTime)
    {
        kstate=Keyboard.GetState();
        mstate=Mouse.GetState();
        mmouse.centrum=mstate.Position.ToVector2();
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

        if (Momen == Moment.Settings)
        {
            foreach(_MinSetRecs setRecs in Settings.Settings)
            {
                if (setRecs.rec.Intersects(mmouse.rec) && mstate.LeftButton == ButtonState.Pressed && mousepress)
                {
                    mousepress=false;
                    if (setRecs.TrueFalse)
                    {
                        setRecs.TrueFalse=false;
                    }
                    else
                    {
                        setRecs.TrueFalse=true;
                    }
                }
            }
        }








        if (kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.R))
        {
            keypress=true;
        }
        if (mstate.LeftButton == ButtonState.Released)
        {
            mousepress=true;
        }
    }
    public override void Draw()
    {
        _spriteBatch.Begin();
            if(Momen == Moment.Settings){
                foreach(_MinSetRecs setrec in Settings.Settings){
                    _spriteBatch.Draw(texture,setrec.rec,setrec.Color);
                }
            }
        _spriteBatch.End();
    }
}

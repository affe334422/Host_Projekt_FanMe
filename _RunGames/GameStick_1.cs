
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameStick_1 : _GameRunSetup
{
    public GameStick_1(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture) : base(_graphics, _spriteBatch, camera2D, texture)
    {
        
    }
    List<_stick> sticks = new List<_stick>();
    Random ran = new Random();
    public override void Update(GameTime gameTime)
    {
        kstate=Keyboard.GetState();
        mstate=Mouse.GetState();
        if (kstate.IsKeyDown(Keys.Space))
        {
            sticks.Add(new _stick(1000,10,Vector2.Zero,0.3f));
        }
        foreach(_stick stick in sticks){
            if (kstate.IsKeyDown(Keys.R))
            {
                stick.rotation++;
            }
            stick.Centrum=mstate.Position.ToVector2();
        }
    }
    public override void Draw()
    {
        _spriteBatch.Begin();
            foreach(_stick stick in sticks){
                foreach(MinRectangle min in stick.ForDraw())
                {
                    _spriteBatch.Draw(texture,min.rec,Color.Blue);
                }
            }
        _spriteBatch.End();
    }
}

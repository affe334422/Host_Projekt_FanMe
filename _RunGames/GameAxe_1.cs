
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameAxe_1 : _GameRunSetup
{
    public GameAxe_1(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        
    }
    _Axe_1 axe = new _Axe_1(Vector2.Zero,30,200,200,70);
    bool keypress = true;
    public override void Update(GameTime gameTime)
    {
        kstate = Keyboard.GetState();
        mstate = Mouse.GetState();
        if (kstate.IsKeyDown(Keys.A) && keypress && axe.Intersects(mstate.Position.ToVector2()))
        {
            axe.Centrum=mstate.Position.ToVector2();
            axe.Update();
            keypress=true;
        }
        if (kstate.IsKeyDown(Keys.Space) && keypress)
        {
            axe.WhatDegrees();
            keypress=false;
        }
        if (kstate.IsKeyUp(Keys.A)&&kstate.IsKeyUp(Keys.Space))
        {
            keypress=true;
        }
    }
    public override void Draw()
    {
        _spriteBatch.Begin();
            foreach(MinRectangle min in axe.ForDraw)
            {
                _spriteBatch.Draw(texture,min.rec,Color.Blue);
            }



        _spriteBatch.End();
    }
}

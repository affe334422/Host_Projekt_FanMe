
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameQuickTest : _GameRunSetup
{
    public GameQuickTest(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        for(int i = 0; i < 10; i++)
        {
            Cards.Add(new _MinaNyaKort(1,Vector2.Zero,10,10,ran.Next(30,100)/10));
        }
    }
    List<_MinaNyaKort> Cards = new List<_MinaNyaKort>(); 
    
    Random ran = new Random();
    public override void Update(GameTime gameTime)
    {
        kstate = Keyboard.GetState();
        if (kstate.IsKeyDown(Keys.Space))
        {
            Cards.Add(new _MinaNyaKort(1,Vector2.Zero,10,10,ran.Next(30,100)/10));
        }
        foreach(_MinaNyaKort kort in Cards)
        {
            if (!kort.ismoving)
            {
                kort.MoveTo(new Vector2(ran.Next(0,1801),ran.Next(0,1001)),0);
            }
            else
            {
                kort.MoveToNewPos();
            }
        }
        
        
    }

    public override void Draw()
    {
        _spriteBatch.Begin();

            foreach(_MinaNyaKort kort in Cards)
            {
                _spriteBatch.Draw(texture,kort.centrum,Color.Blue);
            }

        _spriteBatch.End();
    }
}

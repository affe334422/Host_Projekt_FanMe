
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Camera2D camera2D;
    private Texture2D texture;
    private KeyboardState kstate;
    private MouseState mstate;
    _GameRunSetup _GameRun;
    bool start = true;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight=1000;
        _graphics.PreferredBackBufferWidth=1800;
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        camera2D = new Camera2D(GraphicsDevice);
        base.Initialize();
    }
    List<_MinaNyaKort> ListMedKort = new List<_MinaNyaKort>();
    string[] kläd = {"jack","queen","king","ace"};
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] {Color.White});
        int fan = 50;
        for(int i=2;i<11;i++){
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(i+"_of_clubs"),Content.Load<Texture2D>("card back red"),i,Vector2.Zero,2*fan,3*fan));
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(i+"_of_diamonds"),Content.Load<Texture2D>("card back red"),i,Vector2.Zero,2*fan,3*fan));
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(i+"_of_hearts"),Content.Load<Texture2D>("card back red"),i,Vector2.Zero,2*fan,3*fan));
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(i+"_of_spades"),Content.Load<Texture2D>("card back red"),i,Vector2.Zero,2*fan,3*fan));
        }
        int k = 11;
        foreach(string s in kläd){
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(s+"_of_clubs"),Content.Load<Texture2D>("card back red"),k,Vector2.Zero,2*fan,3*fan));
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(s+"_of_diamonds"),Content.Load<Texture2D>("card back red"),k,Vector2.Zero,2*fan,3*fan));
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(s+"_of_hearts"),Content.Load<Texture2D>("card back red"),k,Vector2.Zero,2*fan,3*fan));
            ListMedKort.Add(new _MinaNyaKort(Content.Load<Texture2D>(s+"_of_spades"),Content.Load<Texture2D>("card back red"),k,Vector2.Zero,2*fan,3*fan));
            k++;
        }
        // TODO: use this.Content to load your game content here
    }
    protected override void Update(GameTime gameTime)
    {
        MouseHelper.Update();
        kstate = Keyboard.GetState();
        if (start)
        {
            start=false;
            _GameRun = new _GameTestTänkandeRobotar(ListMedKort,_graphics,_spriteBatch,camera2D,texture);
        }
        _GameRun.Update(gameTime);

        
        if (_GameRun.Exit||SmåHjälpmedel.Exit){
            Exit();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _GameRun.Draw();

        _spriteBatch.Begin(transformMatrix:camera2D.get_transformation());
            /*foreach(BasFiender r in background.recs)
            {
                _spriteBatch.Draw(texture,r.rec,Color.LightYellow);
            }*/

            












            
            //_spriteBatch.Draw(texture,DuTillKamera.rec,Color.Gray);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

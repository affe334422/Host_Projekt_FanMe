
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class _GameTestTänkandeRobotar : _GameRunSetup
{
    public _GameTestTänkandeRobotar(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        
    }
    _HandMedKort Kortlek = new _HandMedKort(52,MittenPlan);
    static Vector2 MittenPlan = new Vector2(900,500);
    _NyaBotar P1 = new _NyaBotar(new Vector2(900,100),MittenPlan);
    _NyaBotar P2 = new _NyaBotar(new Vector2(900,900),MittenPlan);
    bool gameBegin = true;
    bool KeyPress = true;
    public override void Update(GameTime gameTime)
    {
        MouseHelper.Update();
        kstate = Keyboard.GetState();
        if (gameBegin)
        {
            gameBegin = false;
            P1.MinaKort.Listmedkort.AddRange(Kortlek.TaVissMängdKort(25));
            P1.PlaseraKorten();
            P1.MinaKort.WriteVilkaKort();

            P2.MinaKort.Listmedkort.AddRange(Kortlek.TaVissMängdKort(30));
            P2.PlaseraKorten();
            P2.MinaKort.WriteVilkaKort();
        }
        if (kstate.IsKeyDown(Keys.Space))
        {
            
        }
        if (kstate.IsKeyDown(Keys.A)&&KeyPress)
        {
            KeyPress=false;
            HjälpFrågaEfterKort(P2,P2.FrågaEfterKort());
            
        }
        if (kstate.IsKeyDown(Keys.P))
        {
                
        }
        
        P1.Update();
        Kortlek.Update();    
        P2.Update();
        
        if (kstate.IsKeyUp(Keys.A))
        {
            KeyPress=true;
        }
    }

    public override void Draw()
    {
        _spriteBatch.Begin();
            foreach(_MinaNyaKort kort in Kortlek.Listmedkort)
            {
                _spriteBatch.Draw(texture,kort.centrum,Color.Blue);
            }
            foreach(_MinaNyaKort kort in P1.MinaKort.Listmedkort)
            {
                _spriteBatch.Draw(texture,kort.centrum,Color.Blue);
            }
            if(P2!=null){
                foreach(_MinaNyaKort kort in P2.MinaKort.Listmedkort)
                {
                    _spriteBatch.Draw(texture,kort.centrum,Color.Blue);
                }
            }
            
        _spriteBatch.End();
    }

    public void HjälpFrågaEfterKort(_NyaBotar VemFrågar, play_kort pk)
    {
        if (pk.VemÄgerKortet == null) // ändra så den tar en random spelare från en lista av spelare senare när jag skapar en
        {
            VemFrågar.MinaKort.Listmedkort.AddRange(P1.MinaKort.TaAllaLika(pk.KortetDeÄger));
            VemFrågar.PlaseraKorten();
            P1.PlaseraKorten();
            HiveMinnet.NågonFrågarEfter(VemFrågar,pk.KortetDeÄger);
            HiveMinnet.NågonTarFrånNågon(P1,pk.KortetDeÄger);
            return;
        }
        HiveMinnet.NågonFrågarEfter(VemFrågar,pk.KortetDeÄger);
        HiveMinnet.NågonTarFrånNågon(pk.VemÄgerKortet,pk.KortetDeÄger);
        VemFrågar.MinaKort.Listmedkort.AddRange(pk.VemÄgerKortet.MinaKort.TaAllaLika(pk.KortetDeÄger));
        VemFrågar.PlaseraKorten();
        pk.VemÄgerKortet.PlaseraKorten();
    }
}





using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class _GameTestTänkandeRobotar : _GameRunSetup
{
    public _GameTestTänkandeRobotar(List<_MinaNyaKort> SS,GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, Camera2D camera2D, Texture2D texture):base(_graphics, _spriteBatch, camera2D, texture)
    {
        Kortlek = new _HandMedKort(MittenPlan,SS);
    }
    _HandMedKort Kortlek;
    static Vector2 MittenPlan = new Vector2(900,500);
    List<_NyaBotar> PB = new List<_NyaBotar>
    {
        new _NyaBotar(new Vector2(900,900),MittenPlan),
        new _NyaBotar(new Vector2(900,100),MittenPlan),
        new _NyaBotar(new Vector2(500,500),MittenPlan)
    };
    int VilkensTur=0;
    bool Initsilaze = true;
    BotSekvens sekvens;
    Stopwatch Wait = new Stopwatch();
    Stopwatch framesperSec = new Stopwatch();
    float TimeToWait=900;
    enum BotSekvens
    {
        HarBotenKort,
        FinnsDetIsjönNästaspelare,
        FinnsDetIsjönDuIgen,
        HarDenFrågadeKortet,
    }
    public override void Update(GameTime gameTime)
    {
        kstate = Keyboard.GetState();
        if (Initsilaze)
        {
            Initsilaze = false;
            Kortlek.Blanda();
            foreach(_NyaBotar botar in PB)
            {
                botar.MinaKort.Listmedkort.AddRange(Kortlek.TaVissMängdKort(7));
                botar.MinaKort.Sortera();
                botar.PlaseraKorten();
            }
            Wait.Start();
            framesperSec.Start();
            sekvens = BotSekvens.HarBotenKort;
        }


        



        // spel sekvens som också har animationen det är derför den har en tidsgräns
        if(Wait.ElapsedMilliseconds>TimeToWait){
            Wait.Restart();
            if (sekvens == BotSekvens.HarBotenKort)
            {
                if (PB[VilkensTur].MinaKort.Count > 0)
                {
                    VäljSpelareOchKortAttFråga(PB[VilkensTur]);
                    sekvens= BotSekvens.HarDenFrågadeKortet;
                }
                else
                {
                    sekvens= BotSekvens.FinnsDetIsjönDuIgen;
                }
            }
            if (sekvens == BotSekvens.FinnsDetIsjönNästaspelare)
            {
                PB[VilkensTur].Add(Kortlek.TaRandomKort());
                //kolla fyra;
                NästaSpelare();
                sekvens = BotSekvens.HarBotenKort;
            }
            if (sekvens == BotSekvens.FinnsDetIsjönDuIgen)
            {
                PB[VilkensTur].Add(Kortlek.TaRandomKort());
                sekvens = BotSekvens.HarBotenKort;
            }
            if(sekvens == BotSekvens.HarDenFrågadeKortet)
            {
                if (HjälpFrågaEfterKort(PB[VilkensTur], PB[VilkensTur].botochkort))
                {
                    //kolla fyra;
                    sekvens = BotSekvens.HarBotenKort;
                }
                else
                {
                    sekvens = BotSekvens.FinnsDetIsjönNästaspelare;
                }
            }
        }


        // sätta animation och allat till 0 så det inte är nån vänte tid.
        if (MouseHelper.Click())
        {
            TimeToWait=0f;
            foreach(_NyaBotar NB in PB)
            {
                foreach(_MinaNyaKort kort in NB.MinaKort.Listmedkort)
                {
                    kort.TimeitShouldTake=0f;
                }
            }
            foreach(_MinaNyaKort kort in Kortlek.Listmedkort)
            {
                kort.TimeitShouldTake=0f;
            }
        }
        

        // för animationen, de flyttar korten och inget mer. änsålänge
        Kortlek.Update();
        foreach(_NyaBotar PP in PB)
        {
            PP.Update();
            PP.MinaKort.Sortera();
            PP.PlaseraKorten();
        }
        if (kstate.IsKeyDown(Keys.Escape))
        {
            SmåHjälpmedel.Exit=true;
        }
    }

    public override void Draw()
    {
        _spriteBatch.Begin();
            _spriteBatch.Draw(texture,MouseHelper.CurretPosition(),new Rectangle(0,0,1,1),Color.White,2f,new Vector2(0.5f,0.5f),(float)100/texture.Width,SpriteEffects.None,1f);
            foreach(_MinaNyaKort kort in Kortlek.Listmedkort)
            {
                _spriteBatch.Draw(kort.Texture,kort.centrum,null,Color.White,kort.rotation,new Vector2(kort.Texture.Width/2,kort.Texture.Height/2),(float)kort.width/kort.Texture.Width,SpriteEffects.None,1f);
            }
            foreach(_NyaBotar NB in PB)
            {
                foreach(_MinaNyaKort kort in NB.MinaKort.Listmedkort)
                {
                    _spriteBatch.Draw(kort.Texture,kort.centrum+kort.NärMusRör,null,Color.White,kort.rotation,new Vector2(kort.Texture.Width/2,kort.Texture.Height/2),(float)kort.width/kort.Texture.Width,SpriteEffects.None,1f);
                }
            }
            
        _spriteBatch.End();
    }

    public void NästaSpelare()
    {
        VilkensTur++;
        if (VilkensTur >= PB.Count)
        {
            VilkensTur=0;
        }
    }
    public _NyaBotar VäljRandomSpelareFörutomDigSjälv(_NyaBotar du)
    {
        _NyaBotar a = PB[SmåHjälpmedel.ran.Next(0,PB.Count)];
        if (a == du)
        {
            return VäljRandomSpelareFörutomDigSjälv(du);
        }
        if (a.MinaKort.Count == 0)
        {
            return VäljRandomSpelareFörutomDigSjälv(du);
        } 
        return a;
    }
    private int[] KortsVärde = {2,3,4,5,6,7,8,9,10,11,12,13,14};
    public void VäljSpelareOchKortAttFråga(_NyaBotar Frågar) // ändra den så play_kort kan användas bättre som exempel en extern fråga efter.
    {
        if (Frågar.TankeSätt == 1) // fråga efter den du har mäst av alltid
        {
            Frågar.BotOchKort=  new play_kort(null,VilkenHarDuMestAV(Frågar));
            return;
        }
        if(Frågar.TankeSätt == 2)
        {
            Frågar.BotOchKort = HarNågonDetDuHar(Frågar);
            return;
        }
        Frågar.BotOchKort = new play_kort(null,Frågar.MinaKort.Listmedkort[SmåHjälpmedel.ran.Next(0,Frågar.MinaKort.Listmedkort.Count)]);
        return;
    }
    private _MinaNyaKort VilkenHarDuMestAV(_NyaBotar Frågar) // fråga efter den du har mest av
    {
        _MinaNyaKort frågaefter = null;
        int antalavdenmedhögst = 0;
        foreach(int i in KortsVärde)
        {
            int b = Frågar.MinaKort.HurMångaLika(i);
            if (antalavdenmedhögst < b)
            {
                antalavdenmedhögst = b;
                frågaefter = Frågar.MinaKort.GetRefkort(i);
            }
        }
        return frågaefter;
    } 
    private play_kort HarNågonDetDuHar(_NyaBotar Frågar)
    {
        play_kort p = HiveMinnet.VemHarEttLikakortSomDU(Frågar,Frågar.MinaKort);
        if (p == null)
        {
            return new play_kort(null,VilkenHarDuMestAV(Frågar));
        }
        return p;
    }
    public bool HjälpFrågaEfterKort(_NyaBotar VemFrågar, play_kort pk)
    {
        List<_MinaNyaKort> Vemfrågartar;
        if (pk.VemÄgerKortet == null) // ändra så den tar en random spelare från en lista av spelare senare när jag skapar en
        {
            _NyaBotar annanBot = VäljRandomSpelareFörutomDigSjälv(VemFrågar);
            Vemfrågartar = annanBot.MinaKort.TaAllaLika(pk.KortetDeÄger);
            VemFrågar.MinaKort.Listmedkort.AddRange(Vemfrågartar);
            foreach(_MinaNyaKort kort in VemFrågar.MinaKort.Listmedkort)
            {
                kort.stopMoving=false;
            }
            VemFrågar.PlaseraKorten();
            foreach(_MinaNyaKort kort in annanBot.MinaKort.Listmedkort)
            {
                kort.stopMoving=false;
            }
            annanBot.PlaseraKorten();
            HiveMinnet.NågonFrågarEfter(VemFrågar,pk.KortetDeÄger);
            HiveMinnet.NågonTarFrånNågon(annanBot,pk.KortetDeÄger);
            if (Vemfrågartar.Count == 0)
            {
                return false;
            }
            return true;
        }
        Vemfrågartar = pk.VemÄgerKortet.MinaKort.TaAllaLika(pk.KortetDeÄger);
        VemFrågar.MinaKort.Listmedkort.AddRange(Vemfrågartar);
        foreach(_MinaNyaKort kort in VemFrågar.MinaKort.Listmedkort)
        {
            kort.stopMoving=false;
        }
        VemFrågar.PlaseraKorten();
        foreach(_MinaNyaKort kort in pk.VemÄgerKortet.MinaKort.Listmedkort)
        {
            kort.stopMoving=false;
        }
        pk.VemÄgerKortet.PlaseraKorten();
        HiveMinnet.NågonFrågarEfter(VemFrågar,pk.KortetDeÄger);
        HiveMinnet.NågonTarFrånNågon(pk.VemÄgerKortet,pk.KortetDeÄger);
        if (Vemfrågartar.Count == 0)
        {
            return false;
        }
        return true;
    }
}




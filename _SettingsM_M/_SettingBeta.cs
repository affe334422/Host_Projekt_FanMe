

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class _SettingBeta
{
    // true false settings

    private MinRectangle startpungt = new MinRectangle(new Vector2(200,300),1,1);
    // var de skrivs
    public List<_MinSetRecs> Settings;
    private string Filnamn;
    MinRectangle SizeOfSettings = new MinRectangle(0,0,200,40);
    // (40 + 10) * settings.count 12st = hieght
    public _SettingBeta(string Filnamn)
    {
        this.Filnamn = Filnamn;
        Settings = Settingsrec(Filnamn);
        SettingsBox();
    }

    bool interactingWithSetting = false;
    bool Interactingwithsettingsbox = false;
    public void Update()
    {
        if (MouseHelper.isReleased())
        {
            interactingWithSetting = false;
            Interactingwithsettingsbox = false;
        }
        if(!Interactingwithsettingsbox){
            foreach (_MinSetRecs setRecs in Settings)
            {
                if (setRecs.Update())
                {
                    interactingWithSetting = true;
                }
            }
        }

        if (!interactingWithSetting)
        {   
            Interactingwithsettingsbox = true;
            HandleDrag();
        }
    }

    private void HandleDrag()
    {

        if (MouseHelper.isPressed())
        {
            Vector2 delta = MouseHelper.CurretPosition() - MouseHelper.PreviousPosition();

            startpungt.centrum += delta;

            foreach (_MinSetRecs setrecs in Settings)
                setrecs.centrum += delta;

        }
    }
    public Rectangle rec{get=>startpungt.rec;}
    private void SettingsBox()
    {
        startpungt.centrum = Settings.Last().centrum-Settings.First().centrum;
        startpungt.centrum_x = 200;
        startpungt.centrum_y +=25;
        startpungt.ChangeSize(SizeOfSettings.rec.Width+20,(SizeOfSettings.rec.Height+10)*Settings.Count+20);
        Console.WriteLine(rec+"");
    }
    public void Save()
    {
        List<string> bools = new List<string>();
        foreach(_MinSetRecs recs in Settings)
        {
            bools.Add(recs.TrueFalse+"");
        }
        ListTillFil(Filnamn,bools);
    }
    private List<_MinSetRecs> Settingsrec(string namnpåfil)
    {
        List<bool> bools = FilTillBool(namnpåfil);
        List<_MinSetRecs> setRecs = new List<_MinSetRecs>();
        foreach(bool bo in bools)
        {
            setRecs.Add(new _MinSetRecs(bo,SizeOfSettings));
        }
        for(int i = 0; i < setRecs.Count; i++)
        {
            setRecs[i].centrum=startpungt.centrum+new Vector2(0,i*(SizeOfSettings.rec.Height+10));
        }
        return setRecs;
    }
    private List<bool> FilTillBool(string namnpåfil)
    {
        List<string> Text = FilTillList(namnpåfil);
        List<bool> bools = new List<bool>();
        foreach(string st in Text)
        {
            if (st == "True")
            {
                bools.Add(true);
            }
            else if (st == "False")
            {
                bools.Add(false);
            }
            else
            {
                Console.WriteLine(st);
            }
        }
        return bools;
    }
    private List<string> FilTillList(string namnpåfil)
    {
        StreamReader TextFil = new StreamReader(namnpåfil);
        List<string> Text = new List<string>();
        string line = "";
        while((line = TextFil.ReadLine()) != null){
            Text.Add(line);
        }
        TextFil.Close();
        return Text;
    }
    private void ListTillFil(string namnpåfil, List<string> inmatning)
    {
        StreamWriter textfil = new StreamWriter(namnpåfil);
        inmatning.ForEach(a=>textfil.WriteLine(a));
        textfil.Close();
        return;
    }
}

// en färg och en position kanske en storlek
// kan göra en egen lista så jag kan spara till exempel 
//mer saker i än vad en vanlig list string kan göra. 
//göra det lättare för mig själv att nå dem

   
    


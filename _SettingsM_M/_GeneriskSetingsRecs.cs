
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;

public class _GeneriskSetingsRecs
{
    private string Filnamn;
    private MinRectangle startpungt = new MinRectangle(new Vector2(200,300),1,1);
    public Rectangle rec{get=>startpungt.rec;}
    private MinRectangle SizeOfSettings = new MinRectangle(0,0,200,40);
    public List<_MinGenSetRecs> Settings;
    public _GeneriskSetingsRecs(string Filnamn)
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
            foreach (_MinGenSetRecs setRecs in Settings)
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

            foreach (_MinGenSetRecs setrecs in Settings)
                setrecs.centrum += delta;

        }
    }
    private void SettingsBox()
    {
        startpungt.centrum = Settings.Last().centrum-Settings.First().centrum;
        startpungt.centrum_x = 200;
        startpungt.centrum_y +=25;
        startpungt.ChangeSize(SizeOfSettings.rec.Width+20,(SizeOfSettings.rec.Height+10)*Settings.Count+20);
        Console.WriteLine(rec+"");
    }
    private List<_MinGenSetRecs> Settingsrec(string namnpåfil)
    {
        List<int> ints = FilTillInt(namnpåfil);
        List<_MinGenSetRecs> setRecs = new List<_MinGenSetRecs>();
        foreach(int i in ints)
        {
            setRecs.Add(new _MinGenSetRecs(i,SizeOfSettings));
        }
        for(int i = 0; i < setRecs.Count; i++)
        {
            setRecs[i].centrum=startpungt.centrum+new Vector2(0,i*(SizeOfSettings.rec.Height+10));
        }
        return setRecs;
    }
    private List<int> FilTillInt(string namnpåfil)
    {
        List<string> Text = FilTillList(namnpåfil);
        List<int> ints = new List<int>(); // fel ska ny vara generisk
        foreach(string st in Text)
        {
            try{ints.Add(int.Parse(st));}
            catch{Console.WriteLine(st);}
        }
        return ints;
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
    public void Save()
    {
        List<string> bools = new List<string>();
        foreach(_MinGenSetRecs recs in Settings)
        {
            bools.Add(recs.Value+"");
        }
        ListTillFil(Filnamn,bools);
    }

}



using System.Collections.Generic;
using System.IO;

public class _SettingBeta
{
    private List<string> Settings;
    private string Filnamn;
    public _SettingBeta(string Filnamn)
    {
        this.Filnamn = Filnamn;
        Settings = FilTillList(Filnamn);
    }

    List<string> FilTillList(string namnpåfil)
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
    void ListTillFil(string namnpåfil, List<string> inmatning)
    {
        StreamWriter textfil = new StreamWriter(namnpåfil);
        inmatning.ForEach(a=>textfil.WriteLine(a));
        textfil.Close();
        return;
    }
}

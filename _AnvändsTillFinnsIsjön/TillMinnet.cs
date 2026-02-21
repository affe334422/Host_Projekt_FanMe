
using System.Collections.Generic;

public static class HiveMinnet
{
    public static List<play_kort> Minnet = new List<play_kort>();

    public static void NågonFrågarEfter(_NyaBotar Frågar, _MinaNyaKort kort)
    {
        if(ÄrRedanIminnet(Frågar,kort)){return;}
        Minnet.Add(new play_kort(Frågar,kort));
    }
    public static void NågonTarFrånNågon(_NyaBotar ger, _MinaNyaKort kort)
    {
        List<play_kort> deskabort = new List<play_kort>();
        foreach(play_kort pko in Minnet)
        {
            if (pko.VemÄgerKortet == ger&& pko.KortetDeÄger == kort)
            {
                deskabort.Add(pko);
            }
        }
        foreach(play_kort pko in deskabort)
        {
            Minnet.Remove(pko);
        }
    }
    public static bool ÄrRedanIminnet(_NyaBotar Frågar, _MinaNyaKort kort)
    {
        foreach(play_kort pko in Minnet)
        {
            if (pko.VemÄgerKortet == Frågar&&pko.KortetDeÄger==kort)
            {
                return true;
            }
        }
        return false;
        
    }
    public static play_kort VemHarEttLikakortSomDU(_NyaBotar Frågar, _HandMedKort korts)
    {
        foreach(play_kort pko in Minnet)
        {
            if(pko.VemÄgerKortet==Frågar){continue;}
            if (korts.HurMångaLika(pko.KortetDeÄger.vadförkort) > 0)
            {
                return pko;
            }
        }
        return null;
    }
}
public class play_kort
{
    public play_kort(_NyaBotar vemägerKOrtet, _MinaNyaKort kortetdeöger)
    {
        VemÄgerKortet=vemägerKOrtet;
        KortetDeÄger = kortetdeöger;
    }
    public _NyaBotar VemÄgerKortet;
    public _MinaNyaKort KortetDeÄger;
}
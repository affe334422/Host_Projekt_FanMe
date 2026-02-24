
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class _HandMedKort
{
    public _HandMedKort(Vector2 Position,List<_MinaNyaKort> ListMedKort)
    {
        foreach(_MinaNyaKort Kort in ListMedKort)
        {
            Kort.centrum=Position;
        }
        this.ListMedKort = ListMedKort;
        
    }
    public _HandMedKort()
    {
        
    }
    public _HandMedKort(int AntalKort, Vector2 Position)
    {
        int s = 4; // kanske funkar vem vet.
        int b = 2;
        for(int i = 0; i < AntalKort; i++)
        {
            ListMedKort.Add(new _MinaNyaKort(b,Position,10*2,10*3));
            s--;
            if (s == 0)
            {
                b++;
                s=4;
            }
        }
    }
    public _HandMedKort(List<Texture2D> Frams, Texture2D Bak, Vector2 Position)
    {
        int s = 4; // kanske funkar vem vet.
        int b = 2;
        for(int i = 0; i < Frams.Count; i++)
        {
            ListMedKort.Add(new _MinaNyaKort(Frams[i],Bak,b,Position,10*2,10*3));
            s--;
            if (s == 0)
            {
                b++;
                s=4;
            }
        }
    }
    
    private List<_MinaNyaKort> ListMedKort = new List<_MinaNyaKort>();
    public List<_MinaNyaKort> Listmedkort{get=>ListMedKort;}
    public int Count{get=>ListMedKort.Count;}

    public void Update()
    {
        ListMedKort.ForEach(K=>K.MouseRörKort=false);
        for(int i = ListMedKort.Count-1;i>=0;i--)
        {
            if (ListMedKort[i].Contains(MouseHelper.CurretPosition()))//för att flytta kortet om musen rör det
            {
                ListMedKort[i].MouseRörKort=true;
                break;
            }
        }
        foreach(_MinaNyaKort kort in ListMedKort)
        {
            if (kort.ismoving)
            {
                kort.MoveToNewPos();
            }
        }
    }
    public void WriteVilkaKort()
    {
        foreach(_MinaNyaKort k in ListMedKort)
        {
            Console.Write(k.vadförkort+" ");
        }
        Console.WriteLine();
    }
    public void Add(_MinaNyaKort nyKort)
    {
        if(nyKort == null){return;}
        ListMedKort.Add(nyKort);
    }
    public _MinaNyaKort TaRandomKort()
    {
        if(Listmedkort.Count==0){return null;}
        int a = SmåHjälpmedel.ran.Next(0,ListMedKort.Count);
        _MinaNyaKort kort = ListMedKort[a];
        ListMedKort.RemoveAt(a);
        return kort;
    }
    public _MinaNyaKort TaToppenAvHand()
    {
        if (ListMedKort.Count == 0){return null;}
        _MinaNyaKort Kort = ListMedKort[0];
        ListMedKort.RemoveAt(0);
        return Kort;
    }
    public bool HarJagDet(_MinaNyaKort KollaEfter)
    {
        foreach(_MinaNyaKort mina in ListMedKort)
        {
            if (mina.vadförkort == KollaEfter.vadförkort)
            {
                return true;
            }
        }
        return false;
    }
    public _MinaNyaKort GetRefkort(int vadförkort)
    {
        foreach(_MinaNyaKort mina in ListMedKort)
        {
            if (mina.vadförkort == vadförkort)
            {
                return mina;
            }
        }
        return null;
    }
    public List<_MinaNyaKort> TaAllaLika(_MinaNyaKort KollaEfter)
    {
        List<_MinaNyaKort> nyaKort = new List<_MinaNyaKort>();
        foreach(_MinaNyaKort mina in ListMedKort)
        {
            if (mina.vadförkort == KollaEfter.vadförkort)
            {
                nyaKort.Add(mina);
            }
        }
        foreach(_MinaNyaKort nya in nyaKort)
        {
            ListMedKort.Remove(nya);
        }
        return nyaKort; // kan försvinna kort här om något är fel.
    }
    public List<_MinaNyaKort> TaAllaLika(int KollaEfterVärde)
    {
        List<_MinaNyaKort> nyaKort = new List<_MinaNyaKort>();
        foreach(_MinaNyaKort mina in ListMedKort)
        {
            if (mina.vadförkort == KollaEfterVärde)
            {
                nyaKort.Add(mina);
            }
        }
        foreach(_MinaNyaKort nya in nyaKort)
        {
            ListMedKort.Remove(nya);
        }
        return nyaKort; // kan försvinna kort här om något är fel.
    }
    public int HurMångaLika(int vadförkort)
    {
        int count = 0;
        foreach(_MinaNyaKort mina in ListMedKort)
        {
            if (mina.vadförkort == vadförkort)
            {
                count++;
            }
        }
        return count;
    }
    public void Blanda()
    {
        List<_MinaNyaKort> BlandadHög = new List<_MinaNyaKort>();
        Random ran = new Random();
        while (ListMedKort.Count > 0)
        {
            int a = ran.Next(0,ListMedKort.Count);
            BlandadHög.Add(ListMedKort[a]);
            ListMedKort.RemoveAt(a);
        }
        ListMedKort = BlandadHög; // kan försvinna kort här om något är fel.
    }
    public List<_MinaNyaKort> TaVissMängdKort(int antal)
    {
        if (Listmedkort.Count < antal)
        {
            antal = Listmedkort.Count;
            if(antal==0){return null;}
        }
        List<_MinaNyaKort> d = new List<_MinaNyaKort>();
        for(int i = 0; i < antal; i++)
        {
            int a = SmåHjälpmedel.ran.Next(0,ListMedKort.Count);
            d.Add(ListMedKort[a]);
            Listmedkort.RemoveAt(a);
        }
        return d;
    }
    
    
    public void Sortera()
    {
        if(ListMedKort.Count<=1){return;}
        MergeSort(ListMedKort);
    } 
    private void MergeSort(List<_MinaNyaKort> list)
    {
        if (list.Count <= 1)
        {
            return;
        }

        int mid = list.Count / 2;

        List<_MinaNyaKort> left = list.GetRange(0, mid);
        List<_MinaNyaKort> right = list.GetRange(mid, list.Count - mid);

        MergeSort(left);
        MergeSort(right);

        Merge(list, left, right);
    }
    private void Merge(List<_MinaNyaKort> list, List<_MinaNyaKort> left, List<_MinaNyaKort> right)
    {
        int i = 0, j = 0, k = 0;

        while (i < left.Count && j < right.Count)
        {
            if (left[i].vadförkort <= right[j].vadförkort)
            {
                list[k++] = left[i++];
            }
            else
            {
                list[k++] = right[j++];
            }
        }

        while (i < left.Count)
        {
            list[k++] = left[i++];
        }

        while (j < right.Count)
        {
            list[k++] = right[j++];
        }
    }
}

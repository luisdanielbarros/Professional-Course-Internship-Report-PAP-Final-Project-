using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ListadeItensdoCarrodeCompras
/// </summary>
public class ListadeItensdoCarrodeCompras
{
    public static ListadeItensdoCarrodeCompras GetCurrent
    {
        get
        {
            if (HttpContext.Current.Session["CurrentCart"] == null)
            {
                HttpContext.Current.Session["CurrentCart"] = new ListadeItensdoCarrodeCompras();
            }
            return HttpContext.Current.Session["CurrentCart"] as ListadeItensdoCarrodeCompras;
        }
    }

    List<ItemdoCarrodeCompras> ListadeCompras = new List<ItemdoCarrodeCompras>();
    protected float precototaldalista;
    protected int unidadestotaisdalista;
    public ListadeItensdoCarrodeCompras()
    {
    
    }
    //adicionar
    public void additem(ItemdoCarrodeCompras item)
    {
        bool itsanewitem = true;
        foreach (ItemdoCarrodeCompras itemnalista in ListadeCompras)
        {

                if (itemnalista.getid() == item.getid() && itemnalista.getidv() == item.getidv())
                {
                itsanewitem = false;
                    itemnalista.setunidades(itemnalista.getunidades() + item.getunidades());
                }
        }
        if (itsanewitem == true) this.ListadeCompras.Add(item);
        updateunidadestotaisdalista();
        updateprecototaldalista();
    }
    //apagar
    public void deleteitem(ItemdoCarrodeCompras item)
    {
        ListadeCompras.Remove(item);
    }
    public void deleteitemat(int position)
    {
        this.ListadeCompras.RemoveAt(position);
        updateunidadestotaisdalista();
        updateprecototaldalista();
    }
    public void deleteitembyidv(int idv)
    {
        for (int i = 0; i < ListadeCompras.Count; i++)
        {
            if (ListadeCompras.ElementAt(i).getidv() == idv)
            {
                ListadeCompras.RemoveAt(i);
            }
        }
    }
    public void clearlist()
    {
        this.ListadeCompras.Clear();
        updateunidadestotaisdalista();
        updateprecototaldalista();
    }
    //fazer update à lista depois de moficicações
    public void updateprecototaldalista()
    {
        float tempprecototaldalista = 0;
        foreach (ItemdoCarrodeCompras item in ListadeCompras)
        {
            tempprecototaldalista = tempprecototaldalista + item.getprecototal();
        }
        this.precototaldalista = tempprecototaldalista;
    }
    public void updateunidadestotaisdalista()
    {
        int tempunidadestotaisdalista = 0;
        foreach (ItemdoCarrodeCompras item in ListadeCompras)
        {
            tempunidadestotaisdalista = tempunidadestotaisdalista + item.getunidades();
        }
        this.unidadestotaisdalista = tempunidadestotaisdalista;
    }
    //verificar se a lista tem determinado elemento antes de o tentar devolver
    public bool contains(int idv)
    {
        bool contains = false;
        foreach (ItemdoCarrodeCompras itemnalista in ListadeCompras)
        {
            if (itemnalista.getidv() == idv)
            {
                contains = true;
            }
        }
        return contains;
    }
    //devolver elementos da lista
    public ItemdoCarrodeCompras getitemat(int position)
    {
        return ListadeCompras.ElementAt(position);
    }
    public ItemdoCarrodeCompras getitematidv(int idv)
    {
        int position = -1;
        for (int i = 0; i < ListadeCompras.Count; i++)
        {
            if (ListadeCompras.ElementAt(i).getidv() == idv)
            {
                position = i;
            }
        }
        if (position != -1) return ListadeCompras.ElementAt(position);
        else return new ItemdoCarrodeCompras(0, 0, "", "", "", 0, 0);
    }
    public IEnumerator<ItemdoCarrodeCompras> GetEnumerator()
    {
        return ListadeCompras.GetEnumerator();
    }
    //devolver valores da lista
    public float getprecototaldalista()
    {
        return this.precototaldalista;
    }
    public int getunidadestotaisdalista()
    {
        return this.unidadestotaisdalista;
    }
    //devolver tamanho da lista
    public int getlength()
    {
        return ListadeCompras.Count;
    }
}
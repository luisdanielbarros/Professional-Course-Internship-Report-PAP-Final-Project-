using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ItemdoCarrodeCompras
/// </summary>
public class ItemdoCarrodeCompras
{
    string nome, marca, quantidade;
    int id, idv, unidades;
    float precoporunidade, precototal;
    public ItemdoCarrodeCompras(int id, int idv, string nome, string marca, string quantidade, float precoporunidade, int unidades)
    {
        this.id = id;
        this.idv = idv;
        this.nome = nome;
        this.marca = marca;
        this.quantidade = quantidade;
        this.precoporunidade = precoporunidade;
        this.unidades = unidades;
        updatepreçototal();
    }
    public int getunidades()
    {
        return this.unidades;
    }
    public void setunidades(int a)
    {
        this.unidades = a;
        updatepreçototal();
    }
    public int getid()
    {
        return this.id;
    }
    public int getidv()
    {
        return this.idv;
    }
    public string getnome()
    {
        return this.nome;
    }
    public string getmarca()
    {
        return this.marca;
    }
    public string getquantidade()
    {
        return this.quantidade;
    }
    public float getprecoporunidade()
    {
        return this.precoporunidade;
    }
    public float getprecototal()
    {
        return this.precototal;
    }
    private void updatepreçototal()
    {
        this.precototal = this.precoporunidade * this.unidades;
    }
}
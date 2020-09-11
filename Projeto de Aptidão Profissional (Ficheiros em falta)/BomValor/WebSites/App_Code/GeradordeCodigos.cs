using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GeradordeCodigos
/// </summary>
public class GeradordeCodigos
{
    private Random rnd = new Random();
    private char[] Caracteres = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
    public GeradordeCodigos()
    {

    }
    public string GerarCodigo(int length)
    {
        string Codigo = "";
        for (int i = 0; i < length; i++)
        {
            Codigo += Caracteres[rnd.Next(0, Caracteres.Length - 1)];   
        }
        return Codigo;
    }
}
    

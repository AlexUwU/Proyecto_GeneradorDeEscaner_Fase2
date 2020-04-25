using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Token
    {
        List<string> expresion;

        public Token()
        {
            expresion = new List<string>();
        }

        public List<string> getExpresion()
        {
            return expresion;
        }

        public void agregarExpresion(string elemento)
        {
            expresion.Add(elemento);
        }

        public string listaToString()
        {
            string lista = "";

            for (int i = 0; i < expresion.Count; i++)
            {
                if (expresion.ElementAt(i).Equals("'''"))
                {
                    lista += "'";
                }
                else if (expresion.ElementAt(i).Equals("'\"'"))
                {
                    lista += "\\\"";
                }
                else
                {
                    lista += expresion.ElementAt(i).Replace("'", String.Empty).Trim();
                }                

                if (i != expresion.Count - 1)
                    lista += "$°$";
            }

            return lista;
        }
    }
}

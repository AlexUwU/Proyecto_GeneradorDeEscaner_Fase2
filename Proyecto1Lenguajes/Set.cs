using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Set
    {
        private List<string> elementos;

        public List<string> getElementos()
        {
            return elementos;
        }

        public Set()
        {
            elementos = new List<string>();
        }

        public void AgregarElemento(string elemento)
        {
            if (!elementos.Contains(elemento))
            {
                elementos.Add(elemento);
            }            
        }

        public string listaToString()
        {
            string lista = "";

            for (int i = 0; i < elementos.Count; i++)
            {
                if(elementos.ElementAt(i).Equals("\"") || elementos.ElementAt(i).Equals("\\"))
                {
                    lista += "\\";
                }

                string b = "" + (char)133;                
                if (!(elementos.ElementAt(i) == b))
                {
                    lista += elementos.ElementAt(i);
                    if (i != elementos.Count - 1)
                        lista += "$°$\" +" + Environment.NewLine + "\"";
                }                                
            }

            return lista;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Estado
    {
        string elementos;
        string nombre;
        List<int> elementosLista = new List<int>();
        bool estadoAceptacion;

        public Estado(List<int> lista, string nombre)
        {
            elementosLista = lista;
            this.nombre = nombre;
            elementos = "";
            generarString(lista);
        }

        public Estado()
        {

        }

        public bool getEstadoAceptacion()
        {
            return estadoAceptacion;
        }

        public void setEstadoAceptacion(bool resultado)
        {
            this.estadoAceptacion = resultado;
        }

        public void generarString(List<int> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                elementos += lista.ElementAt(i);

                if (i != lista.Count - 1)
                    elementos += ",";
            }
        }

        public static string generarRetornarString(List<int> lista)
        {
            string elemento = "";
            for (int i = 0; i < lista.Count; i++)
            {
                elemento += lista.ElementAt(i);

                if (i != lista.Count - 1)
                    elemento += ",";
            }

            return elemento;
        }

        public string getElementosString()
        {
            return elementos;
        }

        public string getNombre()
        {
            return nombre;
        }

        public List<int> getLista()
        {
            return elementosLista;
        }

        public bool verSiIgual(Estado estado)
        {
            bool resultado = false;

            if (this.getElementosString().Equals(estado.getElementosString()))
                resultado = true;

            return resultado;
        }
    }
}

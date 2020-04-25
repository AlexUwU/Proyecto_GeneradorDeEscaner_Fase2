using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Nodo
    {
        string elemento;
        int numero;
        bool nulable;
        List<int> first = new List<int>();
        List<int> last = new List<int>();        

        public Nodo(string elemento)
        {
            this.elemento = elemento;
            numero = 0;            
        }

        public Nodo(string elemento, int num, int first, int last, bool nulable)
        {
            this.elemento = elemento;
            numero = num;
            this.first.Add(first);
            this.last.Add(last);
            this.nulable = nulable;
        }

        public bool getNulabilidad()
        {
            return nulable;
        }

        public void setNulabilidad(bool nulable)
        {
            this.nulable = nulable;
        }
        public int getNumero()
        {
            return numero;
        }

        public string getElemento()
        {
            return elemento;
        }

        public List<int> getFirst()
        {
            return first;
        }

        public List<int> getLast()
        {
            return last;
        }        

        public void agregarFirst(int num)
        {
            first.Add(num);
        }

        public void agregarLast(int num)
        {
            last.Add(num);
        }                  
    }
}

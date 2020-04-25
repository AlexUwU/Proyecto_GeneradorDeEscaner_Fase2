using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Lector
    {
        public string LeerArchivo(string archivo)
        {
            bool error = false;
            Scanner scanner = new Scanner();
            StreamReader lector = new StreamReader(archivo);
            int contadorLineas = 0;

            string linea;

            while((linea = lector.ReadLine()) != null)
            {
                contadorLineas++;

                if(!scanner.Verificar(contadorLineas, linea))                                
                {
                    error = true;
                    break;
                }
                
            }

            if (Scanner.contadorEncabezados != 4)
                error = true;

            lector.Close();

            if (error)
            {
                return "ERROR 54 en la linea " + contadorLineas; ;
            }
            else
            {
                scanner.generarAutomata();
                return"Autómata generado con éxito";
            }
        }
    }
}

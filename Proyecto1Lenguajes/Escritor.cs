using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Escritor
    {
        public static void escribir(string contenido, string ruta)
        {
            FileInfo archivo = new FileInfo(ruta);
            if (archivo.Exists)
            {
                archivo.Delete();
            }

            StreamWriter escritor = new StreamWriter(ruta, false);
            
            escritor.WriteLine(contenido);            

            escritor.Close();
        }
    }
}

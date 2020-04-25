using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Action
    {
        string accion;

        public Action(string accion)
        {
            this.accion = accion;
        }

        public string getString()
        {
            return accion;
        }
    }
}

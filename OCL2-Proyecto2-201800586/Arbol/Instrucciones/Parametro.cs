using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Parametro
    {
        public String id;
        public Constante.Type type;

        public Parametro(String id, Constante.Type type)
        {
            this.id = id;
            this.type = type;
        }
    }
}

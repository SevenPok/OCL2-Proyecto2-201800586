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
        public int liena;
        public int columna;
        public Parametro(String id, Constante.Type type, int liena, int columna)
        {
            this.id = id;
            this.type = type;
            this.liena = liena + 1;
            this.columna = columna + 1;
        }
    }
}

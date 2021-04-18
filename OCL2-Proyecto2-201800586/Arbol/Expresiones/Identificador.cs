using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Identificador : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private string id;

        public Identificador(String id, int linea, int columna)
        {
            this.id = id;
            this.linea = linea;
            this.columna = columna;
        }

        public object traducir(Entorno ts)
        {
            return null;
        }
    }
}

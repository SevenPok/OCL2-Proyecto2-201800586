using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Relacional : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private Expresion left, right;
        private Constante.RelacionalSigno type;

        public Relacional(Expresion left, Expresion right, Constante.RelacionalSigno type, int linea, int columna)
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.linea = linea;
            this.columna = columna;
        }

        public object traducir(Entorno ts)
        {
            return null;
        }
    }
}

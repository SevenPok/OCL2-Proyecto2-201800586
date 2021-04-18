using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Print : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private LinkedList<Expresion> expresiones;
        private bool salto;

        public Print(LinkedList<Expresion> expresiones, bool salto, int linea, int columna)
        {
            this.expresiones = expresiones;
            this.salto = salto;
            this.linea = linea;
            this.columna = columna;
        }
        public object traducir(Entorno ts)
        {
            return null;
        }
    }
}

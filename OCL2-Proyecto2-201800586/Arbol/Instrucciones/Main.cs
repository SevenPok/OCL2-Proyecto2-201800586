using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Main : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private LinkedList<Instruccion> instrucciones;

        public Main(LinkedList<Instruccion> instrucciones, int linea, int columna)
        {
            this.instrucciones = instrucciones;
            this.linea = linea;
            this.columna = columna;
        }
        public object traducir(Entorno ts)
        {
            return null;
        }
    }
}

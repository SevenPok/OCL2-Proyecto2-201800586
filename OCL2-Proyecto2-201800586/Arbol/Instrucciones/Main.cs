using OCL2_Proyecto2_201800586.Arbol.Interfaces;
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
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private LinkedList<Instruccion> instrucciones;

        public Main(LinkedList<Instruccion> instrucciones, int linea, int columna)
        {
            this.instrucciones = instrucciones;
            this.linea = linea;
            this.columna = columna;
        }
        public Return traducir(Entorno ts)
        {
            foreach(Instruccion i in instrucciones)
            {
                i.traducir(ts);
            }
            return null;
        }
    }
}

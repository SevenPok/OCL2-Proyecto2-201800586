using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Else : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private LinkedList<Instruccion> instruccions;

        public Else(LinkedList<Instruccion> instruccions, int linea, int columna)
        {
            this.instruccions = instruccions;
            this.linea = linea;
            this.columna = columna;
        }
        public Return traducir(Entorno ts)
        {
            foreach(Instruccion i in instruccions)
            {
                i.traducir(ts);
            }
            return null;
        }
    }
}

using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Break : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        public Break(int linea, int columna)
        {
            this.linea = linea + 1;
            this.columna = columna + 1;
        }

        public Return traducir(Entorno ts)
        {
            if (ts._break == null) throw new Error(this.linea, this.columna, "Semantico", "Un 'break' La declaración solo se puede usar dentro de una declaración de iteración adjunta");
            Generator.getInstance().addGoto(ts._break);
            return null;
        }
    }
}

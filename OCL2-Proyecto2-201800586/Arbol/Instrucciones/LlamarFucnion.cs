using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class LlamarFucnion : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion valor;

        public LlamarFucnion(Expresion valor)
        {
            this.valor = valor;
        }
        public Return traducir(Entorno ts)
        {
            return valor.traducir(ts);
        }
    }
}

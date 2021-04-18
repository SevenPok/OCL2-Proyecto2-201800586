using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Primitivo : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private object valor;
        public Primitivo(object valor, int fila, int columna)
        {
            this.valor = valor;
            this.linea = fila;
            this.columna = columna;
        }
        public object traducir(Entorno ts)
        {
            return valor;
        }
    }
}

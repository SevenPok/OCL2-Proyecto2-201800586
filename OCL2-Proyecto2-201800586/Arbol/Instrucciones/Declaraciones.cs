using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Declaraciones : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private LinkedList<Declaracion> declaraciones;

        public Declaraciones(LinkedList<Declaracion> declaraciones)
        {
            this.declaraciones = declaraciones;
        }
        public Return traducir(Entorno ts)
        {
            foreach (Instruccion declaracion in declaraciones)
            {
                declaracion.traducir(ts);
            }
            return null;
        }
    }
}

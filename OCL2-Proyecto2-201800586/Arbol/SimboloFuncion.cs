using OCL2_Proyecto2_201800586.Arbol.Instrucciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class SimboloFuncion
    {
        public Constante.Type type;
        public String id;
        public int size;
        public LinkedList<Parametro> parametros;
        public int linea;
        public int columna;

        public SimboloFuncion(DeclaracionFuncion funcion)
        {
            this.type = funcion.type;
            this.id = funcion.id;
            this.size = funcion.parametros.Count;
            this.parametros = funcion.parametros;
            this.columna = funcion.columna;
            this.linea = funcion.linea;
        }
    }
}

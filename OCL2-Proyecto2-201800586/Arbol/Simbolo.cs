using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class Simbolo
    {
        public Constante.Type tipo;
        public String identificador;
        public int posiscion;
        public bool isConst;
        public bool isHeap;

        public Simbolo(Constante.Type tipo, String identificador, int posiscion, bool isConst, bool isHeap)
        {
            this.tipo = tipo;
            this.identificador = identificador;
            this.posiscion = posiscion;
            this.isConst = isConst;
            this.isHeap = isHeap;
        }
    }
}

using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class Simbolo
    {
        public Constante.Type type;
        public String id;
        public int position;
        public bool isConst;
        public bool isGlobal;
        public bool isHeap;
        public int linea;
        public int columna;

        public Simbolo(Constante.Type type, String id, int position, bool isConst, bool isGlobal, bool isHeap, int linea, int columna)
        {
            this.type = type;
            this.id = id;
            this.position = position;
            this.isConst = isConst;
            this.isGlobal = isGlobal;
            this.isHeap = isHeap;
            this.linea = linea;
            this.columna = columna;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    interface Instruccion
    {
        int linea { get; set; }
        int columna { get; set; }

        Object traducir(Entorno ts);
    }
}

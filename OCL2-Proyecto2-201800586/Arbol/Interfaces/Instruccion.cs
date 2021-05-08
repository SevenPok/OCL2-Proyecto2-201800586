using OCL2_Proyecto2_201800586.Arbol.Interfaces;
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
        String trueLabel { get; set; }
        String falseLabel { get; set; }
        Return traducir(Entorno ts);
    }
}

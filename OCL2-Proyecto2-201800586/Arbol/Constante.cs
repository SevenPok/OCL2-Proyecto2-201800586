using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class Constante
    {
        public enum Type
        {
            STRING,
            INT,
            DOUBLE,
            BOOLEAN,
            OBJETO,
            ARRAY
        }
        
        public enum AritmeticaSigno
        {
            MAS,
            MENOS,
            POR,
            DIV,
            MOD
        }

        public enum RelacionalSigno
        {
            MENOR,
            MAYOR,
            MENORIGUAL,
            MAYORIGUAL,
            IGUAL,
            DIFERENTE
        }

        public enum LogicaSigno
        {
            AND,
            OR,
            NOT
        }

        public Type getTipo(object o)
        {
            if (o is bool)
            {
                return Type.BOOLEAN;
            }
            else if (o is int)
            {
                return Type.INT;
            }
            else if (o is double)
            {
                return Type.DOUBLE;
            }
            else
            {
                return Type.STRING;
            }
        }
    }
}

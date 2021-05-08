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
            ARRAY,
            VOID
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

        public static Type getTipo(String o)
        {
            if (o == "boolean")
            {
                return Type.BOOLEAN;
            }
            else if (o == "integer")
            {
                return Type.INT;
            }
            else if (o == "real")
            {
                return Type.DOUBLE;
            }
            else if(o == "string")
            {
                return Type.STRING;
            }
            else
            {
                return Type.VOID;
            }
        }

        public static bool sameType(Constante.Type left, Constante.Type right)
        {
            if (left == right) return true;
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class Error : Exception
    {
        public int line, column;
        public String type, msg;

        public Error(int line, int column, String type, String msg)
        {
            this.line = line;
            this.column = column;
            this.type = type;
            this.msg = msg;
        }
    }
}

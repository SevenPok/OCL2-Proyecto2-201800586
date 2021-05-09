using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Optimizacion
{
    class GramaticaOp : Grammar
    {
        public GramaticaOp(): base(caseSensitive : false)
        {
            #region ER
            StringLiteral CADENA = new StringLiteral("Cadena", "'");
            var NUMERO = new NumberLiteral("Numero");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            CommentTerminal comentarioBloque2 = new CommentTerminal("comentarioBloque", "{", "}");
            #endregion
        }
    }
}

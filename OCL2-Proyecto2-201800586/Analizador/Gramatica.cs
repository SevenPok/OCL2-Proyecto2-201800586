using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace OCL2_Proyecto2_201800586.Analizador
{
    class Gramatica: Grammar
    {
        public Gramatica(): base (caseSensitive: false) {
            #region ER
            StringLiteral CADENA = new StringLiteral("Cadena", "'");
            var ENTERO = new NumberLiteral("Entero");
            var DECIMAL = new RegexBasedTerminal("Decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            CommentTerminal comentarioBloque2 = new CommentTerminal("comentarioBloque", "{", "}");
            #endregion

            #region Terminales

            #endregion

            #region No Terminales

            #endregion

            #region Gramatica

            #endregion

        }
    }
}

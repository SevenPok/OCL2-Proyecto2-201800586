using Irony.Parsing;
using OCL2_Proyecto2_201800586.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Analizador
{
    class ErrorHandler
    {
        private ParseTree tree;
        private ParseTreeNode root;
        public LinkedList<Error> errores;
        public ErrorHandler(ParseTree tree, ParseTreeNode root)
        {
            this.tree = tree;
            this.root = root;
            errores = new LinkedList<Error>();
        }

        public bool hasErrors()
        {
            if (tree.ParserMessages.Count > 0 || root == null)
            {
                foreach (var error in tree.ParserMessages)
                {
                    if (error.Message.Contains("Sintax"))
                    {
                        errores.AddLast(new Error(error.Location.Line + 1, error.Location.Column + 1, "Sintactico", error.Message));
                    }
                    else
                    {
                        errores.AddLast(new Error(error.Location.Line + 1, error.Location.Column + 1, "Lexico", error.Message));
                    }
                        
                }
                return true;
            }
            return false;
        }
    }
}

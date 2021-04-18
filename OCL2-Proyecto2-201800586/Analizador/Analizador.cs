using Irony.Parsing;
using OCL2_Proyecto2_201800586.Arbol;
using OCL2_Proyecto2_201800586.Arbol.Expresiones;
using OCL2_Proyecto2_201800586.Arbol.Instrucciones;
using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OCL2_Proyecto2_201800586.Analizador
{
    class Analizador
    {
        private ParseTreeNode raiz;
        
        public Analizador(ParseTreeNode raiz)
        {
            this.raiz = raiz;
        }

        public void generar()
        {
            LinkedList<Instruccion> AST = instrucciones(raiz.ChildNodes.ElementAt(0));
            Entorno global = new Entorno();
            foreach (Instruccion ins in AST)
            {
                ins.traducir(global);
            }

        }

        private LinkedList<Instruccion> instrucciones(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast(instruccion(nodo));
            }
            return lista;
        }

        private Instruccion instruccion(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            switch (token)
            {
                default:
                    return main(actual.ChildNodes.ElementAt(0));
            }
        }

        private Instruccion main(ParseTreeNode actual)
        {
            return new Main(sentencias(actual.ChildNodes.ElementAt(0)), 0, 0);
        }

        private LinkedList<Instruccion> sentencias(ParseTreeNode actual)
        {
            LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast(sentencia(nodo));
            }
            return lista;
        }

        private Instruccion sentencia(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            switch (token)
            {
                default:
                    return imprimir(actual.ChildNodes.ElementAt(0));
            }
        }

        private Instruccion imprimir(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            switch (token)
            {
                case "write":
                    return new Print(expresiones(actual.ChildNodes.ElementAt(1)), false, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                default:
                    return new Print(expresiones(actual.ChildNodes.ElementAt(1)), true, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
        }

        private LinkedList<Expresion> expresiones(ParseTreeNode actual)
        {
            LinkedList<Expresion> lista = new LinkedList<Expresion>();
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                lista.AddLast(expresion(nodo));
            }
            return lista;
        }

        private Expresion expresion(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            switch (token)
            {
                case "Expresion":
                    return expresion(actual.ChildNodes.ElementAt(0));
                case "Expresion_Unaria":
                    return expresionUnaria(actual.ChildNodes.ElementAt(0));
                case "Expresion_Numerica":
                    return aritmetica(actual.ChildNodes.ElementAt(0));
                case "Expresion_Logica":
                    return logica(actual.ChildNodes.ElementAt(0));
                case "Expresion_Relacional":
                    return relacional(actual.ChildNodes.ElementAt(0));
                default:
                    return primitivo(actual.ChildNodes.ElementAt(0));
            }
        }

        private Aritmetica aritmetica(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            switch (token)
            {
                case "mod":
                    return new Aritmetica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.AritmeticaSigno.MOD, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "/":
                    return new Aritmetica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.AritmeticaSigno.DIV, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "*":
                    return new Aritmetica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.AritmeticaSigno.POR, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "-":
                    return new Aritmetica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.AritmeticaSigno.MENOS, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                default:
                    return new Aritmetica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.AritmeticaSigno.MAS, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
            }
        }

        private Logica logica(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                string token = actual.ChildNodes.ElementAt(0).Term.ToString();
                switch (token)
                {
                    case "and":
                        return new Logica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.LogicaSigno.AND, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                    default:
                        return new Logica(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.LogicaSigno.OR, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                }
            }
            else
            {
                return new Logica(expresion(actual.ChildNodes.ElementAt(1)), Constante.LogicaSigno.NOT, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
        }

        private Relacional relacional(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            switch (token)
            {
                case ">":
                    return new Relacional(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.RelacionalSigno.MAYOR, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "<":
                    return new Relacional(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.RelacionalSigno.MENOR, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case ">=":
                    return new Relacional(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.RelacionalSigno.MAYORIGUAL, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "<=":
                    return new Relacional(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.RelacionalSigno.MENORIGUAL, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                case "=":
                    return new Relacional(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.RelacionalSigno.IGUAL, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
                default:
                    return new Relacional(expresion(actual.ChildNodes.ElementAt(0)), expresion(actual.ChildNodes.ElementAt(2)), Constante.RelacionalSigno.DIFERENTE, actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column);
            }
        }

        private Aritmetica expresionUnaria(ParseTreeNode actual)
        {
            return new Aritmetica(expresion(actual.ChildNodes.ElementAt(1)), Constante.AritmeticaSigno.MENOS, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        private Expresion primitivo(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(0).Term.ToString();
            string valor = actual.ChildNodes.ElementAt(0).Token.Text;
            //Console.WriteLine(valor.Substring(1, valor.Length - 2));
            //Console.WriteLine(valor);
            switch (token)
            {
                case "Cadena":
                    return new Primitivo(valor.Substring(1, valor.Length - 2), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                case "Entero":
                    return new Primitivo(int.Parse(valor), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                case "Decimal":
                    return new Primitivo(double.Parse(valor), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                case "true":
                    return new Primitivo(bool.Parse(valor), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                case "false":
                    return new Primitivo(bool.Parse(valor), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                default:
                    return new Identificador(valor, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
        }

    }
}

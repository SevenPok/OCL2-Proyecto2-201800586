using Irony.Parsing;
using OCL2_Proyecto2_201800586.Arbol;
using OCL2_Proyecto2_201800586.Arbol.Expresiones;
using OCL2_Proyecto2_201800586.Arbol.Instrucciones;
using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            Entorno global = new Entorno(null);
            
            Generator generator = Generator.getInstance();
            generator.clearCode();
            
                foreach (Instruccion e in AST)
                {
                    if (e is DeclaracionFuncion) e.traducir(global);
                }

                generator.addBegin("main");

                foreach (Instruccion e in AST)
                {
                    e.traducir(global);
                }
                generator.addEnd();
            
            string functions = generator.getFunctions();
            String code = generator.getCode();
            Form1.Consola.Text = code + functions;
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
                case "Funcion":
                    return funcion(actual.ChildNodes.ElementAt(0));
                case "Declaracion":
                    return declaracion(actual.ChildNodes.ElementAt(0));
                default:
                    return main(actual.ChildNodes.ElementAt(0));
            }
        }

        private Instruccion main(ParseTreeNode actual)
        {
            return new Main(sentencias(actual.ChildNodes.ElementAt(0)), 0, 0);
        }


        private Instruccion exit(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 2)
            {
                return new Exit(expresion(actual.ChildNodes[1]), actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
            }
            else
            {
                return new Exit(null, actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
            }
        }

        private Instruccion funcion(ParseTreeNode actual)
        {
            if(actual.ChildNodes.Count == 7)
            {
                string token = actual.ChildNodes[1].Token.Text;
                Constante.Type tipo = Constante.getTipo(actual.ChildNodes[4].Term.ToString());
                LinkedList<Parametro> parametros = declararParametros(actual.ChildNodes[2]);
                DeclaracionFuncion func = new DeclaracionFuncion(token, parametros, tipo, sentencias(actual.ChildNodes[6]),actual.ChildNodes[1].Token.Location.Line, actual.ChildNodes[1].Token.Location.Column);
                return func;
            }
            else
            {

            }
            return null;
        }

        private LinkedList<Parametro> declararParametros(ParseTreeNode actual)
        {
            LinkedList<Parametro> parametros = new LinkedList<Parametro>();
            foreach(ParseTreeNode hijo in actual.ChildNodes)
            {
                if (hijo.ChildNodes.Count == 3)
                {   

                    foreach (ParseTreeNode nodo in hijo.ChildNodes[0].ChildNodes)
                    {
                        Constante.Type tipo = Constante.getTipo(hijo.ChildNodes[2].Token.Text);
                        parametros.AddLast(new Parametro(nodo.Token.Text, tipo));
                    }
                }
                else
                {
                    foreach (ParseTreeNode nodo in hijo.ChildNodes[1].ChildNodes)
                    {
                        Constante.Type tipo = Constante.getTipo(hijo.ChildNodes[3].Token.Text);
                        parametros.AddLast(new Parametro(nodo.Token.Text, tipo));
                    }
                }
            }
            return parametros;
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
                case "If":
                    return IF(actual.ChildNodes.ElementAt(0));
                case "Switch":
                    return SWITCH(actual.ChildNodes.ElementAt(0));
                case "For":
                    return FOR(actual.ChildNodes.ElementAt(0));
                case "While":
                    return WHILE(actual.ChildNodes.ElementAt(0));
                case "Repeat_Until":
                    return DOWHILE(actual.ChildNodes.ElementAt(0));
                case "Asignar":
                    return asignacion(actual.ChildNodes.ElementAt(0));
                case "Exit":
                    return exit(actual.ChildNodes.ElementAt(0));
                case "Call_Funcion":
                    return llamarFucnion(actual.ChildNodes.ElementAt(0));
                default:
                    return imprimir(actual.ChildNodes.ElementAt(0));
            }
        }


        public Instruccion llamarFucnion(ParseTreeNode actual)
        {
            return new LlamarFucnion(callFucnion(actual));
        }
        private Expresion callFucnion(ParseTreeNode actual)
        {
            LinkedList<Expresion> parametros = new LinkedList<Expresion>();
            foreach (ParseTreeNode nodo in actual.ChildNodes[1].ChildNodes)
            {
                parametros.AddLast(expresion(nodo));
            }
            return new AccesoFuncion(actual.ChildNodes[0].Token.Text, parametros, actual.ChildNodes[0].Token.Location.Line, actual.ChildNodes[0].Token.Location.Column);
        }

        private Instruccion declaracion(ParseTreeNode actual)
        {
            LinkedList<Declaracion> lista = new LinkedList<Declaracion>();
            string token = "";
            string tipo = actual.ChildNodes.ElementAt(3).Term.ToString();
            if (actual.ChildNodes.Count == 4)
            {
                if (actual.ChildNodes.ElementAt(1).ChildNodes.Count > 0)
                {
                    foreach (ParseTreeNode node in actual.ChildNodes.ElementAt(1).ChildNodes)
                    {
                        token = node.Token.Text;
                        lista.AddLast(new Declaracion(false, token, Constante.getTipo(tipo), inicializarVariable(Constante.getTipo(tipo)), node.Token.Location.Line, node.Token.Location.Column));
                    }
                }
                else
                {
                    token = actual.ChildNodes.ElementAt(1).Token.Text;
                    lista.AddLast(new Declaracion(false, token, Constante.getTipo(tipo), inicializarVariable(Constante.getTipo(tipo)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column));
                }
            }
            else
            {
                token = actual.ChildNodes.ElementAt(1).Token.Text;
                lista.AddLast(new Declaracion(false, token, Constante.getTipo(tipo), expresion(actual.ChildNodes.ElementAt(4)), actual.ChildNodes.ElementAt(1).Token.Location.Line, actual.ChildNodes.ElementAt(1).Token.Location.Column));
            }
            return new Declaraciones(lista);
        }

        private Instruccion asignacion(ParseTreeNode actual)
        {
            return new Asignacion(actual.ChildNodes.ElementAt(0).Token.Text, expresion(actual.ChildNodes.ElementAt(2)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        private Expresion inicializarVariable(Constante.Type tipo)
        {
            switch (tipo)
            {
                case Constante.Type.INT:
                    return new Primitivo(0, tipo, 0, 0);
                case Constante.Type.DOUBLE:
                    return new Primitivo(0.0, Constante.Type.DOUBLE, 0, 0);
                case Constante.Type.BOOLEAN:
                    return new Primitivo(false, tipo, 0, 0);
                default:
                    return new Primitivo("", tipo, 0, 0);
            }
        }

        public Instruccion IF(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 4)
            {
                return new If(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), null, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
            else
            {
                String token = actual.ChildNodes.ElementAt(5).Term.ToString();
                if (token == "If")
                {
                    return new If(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), IF(actual.ChildNodes.ElementAt(5)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                }
                else
                {
                    if(actual.ChildNodes.ElementAt(5).ChildNodes.Count > 0)
                    {
                        return new If(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), new Else(sentencias(actual.ChildNodes.ElementAt(5)), 0, 0), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                    }
                    return new If(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), null, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                }
            }
        }

        private Instruccion WHILE(ParseTreeNode actual)
        {
            return new While(expresion(actual.ChildNodes.ElementAt(1)), sentencias(actual.ChildNodes.ElementAt(3)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        private Instruccion DOWHILE(ParseTreeNode actual)
        {
            return new DoWhile(expresion(actual.ChildNodes.ElementAt(3)), sentencias(actual.ChildNodes.ElementAt(1)), actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
        }

        private Instruccion FOR(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(2).Term.ToString();
            if(token == "to")
            {
                return new For((Asignacion)asignacion(actual.ChildNodes.ElementAt(1)), menor(actual), inc(actual), sentencias(actual.ChildNodes.ElementAt(5)), dec(actual), 0, 0);
            }
            else
            {
                return new For((Asignacion)asignacion(actual.ChildNodes.ElementAt(1)), mayor(actual), dec(actual), sentencias(actual.ChildNodes.ElementAt(5)), inc(actual), 0, 0);
            }
        }

        private Expresion mayor(ParseTreeNode actual)
        {
            Identificador id = new Identificador(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text,null,0,0);
            return new Relacional(id, expresion(actual.ChildNodes.ElementAt(3)), Constante.RelacionalSigno.MAYORIGUAL, 0, 0); 
        }

        private Expresion menor(ParseTreeNode actual)
        {
            Identificador id = new Identificador(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text, null, 0, 0);
            return new Relacional(id, expresion(actual.ChildNodes.ElementAt(3)), Constante.RelacionalSigno.MENORIGUAL, 0, 0);
        }

        private Asignacion inc(ParseTreeNode actual)
        {
            Identificador id = new Identificador(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text, null, 0, 0);
            Aritmetica incrementar = new Aritmetica(id, new Primitivo(1, Constante.Type.INT, 0, 0), Constante.AritmeticaSigno.MAS, 0, 0);
            return new Asignacion(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text, incrementar, 0, 0);
        }

        private Asignacion dec(ParseTreeNode actual)
        {
            Identificador id = new Identificador(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text, null, 0, 0);
            Aritmetica incrementar = new Aritmetica(id, new Primitivo(1, Constante.Type.INT, 0, 0), Constante.AritmeticaSigno.MENOS, 0, 0);
            return new Asignacion(actual.ChildNodes.ElementAt(1).ChildNodes.ElementAt(0).Token.Text, incrementar, 0, 0);
        }

        private Instruccion SWITCH(ParseTreeNode actual)
        {
            Expresion exp = expresion(actual.ChildNodes.ElementAt(1));
            LinkedList<If> casos = new LinkedList<If>();
            foreach(ParseTreeNode nodo in actual.ChildNodes[3].ChildNodes)
            {
                Relacional condicion = new Relacional(exp, expresion(nodo.ChildNodes[0]), Constante.RelacionalSigno.IGUAL, 0, 0);
                casos.AddLast(new If(condicion, sentencias(nodo.ChildNodes[2]), null, 0, 0));
            }
            casos.Reverse();

            if (actual.ChildNodes[4].ChildNodes.Count > 0)
            {
                casos.First().elseif = new Else(sentencias(actual.ChildNodes[4].ChildNodes[1]), 0, 0);
            }
            
            if(casos.Count > 1)
            {
                int size = actual.ChildNodes[3].ChildNodes.Count;
                for (int i = 1; i< size; i++)
                {
                    If temp = casos.First();
                    casos.RemoveFirst();
                    casos.First().elseif = temp;
                }
            }
            return casos.First();
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
                case "Call_Funcion":
                    return callFucnion(actual.ChildNodes.ElementAt(0));
                default:
                    return primitivo(actual.ChildNodes.ElementAt(0));
            }
        }

        private Aritmetica aritmetica(ParseTreeNode actual)
        {
            string token = actual.ChildNodes.ElementAt(1).Term.ToString();
            //Console.WriteLine(token);
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
                string token = actual.ChildNodes.ElementAt(1).Term.ToString();
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
            string token = actual.ChildNodes.ElementAt(1).Term.ToString();
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
                    return new Primitivo(valor.Substring(1, valor.Length - 2), Constante.Type.STRING, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                case "Numero":
                    if (Regex.IsMatch(valor, "^-?[0-9]+$"))
                    {
                        return new Primitivo(int.Parse(valor), Constante.Type.INT, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                    }
                    else
                    {
                        return new Primitivo(double.Parse(valor), Constante.Type.DOUBLE, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                    }
                
                case "true":
                    return new Primitivo(bool.Parse(valor), Constante.Type.BOOLEAN, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                case "false":
                    return new Primitivo(bool.Parse(valor), Constante.Type.BOOLEAN, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
                default:
                    return new Identificador(valor, null, actual.ChildNodes.ElementAt(0).Token.Location.Line, actual.ChildNodes.ElementAt(0).Token.Location.Column);
            }
        }

    }
}

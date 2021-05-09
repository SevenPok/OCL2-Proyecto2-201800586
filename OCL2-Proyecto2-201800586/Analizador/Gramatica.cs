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
        public Gramatica() : base(caseSensitive: false) {
            #region ER
            StringLiteral CADENA = new StringLiteral("Cadena", "'");
            var NUMERO = new NumberLiteral("Numero");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            CommentTerminal comentarioBloque2 = new CommentTerminal("comentarioBloque", "{", "}");
            #endregion

            #region Terminales
            var PTCOMA = ToTerm(";");
            var DOSPT = ToTerm(":");
            var COMA = ToTerm(",");
            var PUNTO = ToTerm(".");

            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");

            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");

            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIV = ToTerm("/");
            var MOD = ToTerm("mod");

            var AND = ToTerm("and");
            var OR = ToTerm("or");
            var NOT = ToTerm("not");

            var MAYOR = ToTerm(">");
            var MENOR = ToTerm("<");
            var MAYORIGUAL = ToTerm(">=");
            var MENORIGUAL = ToTerm("<=");
            var IGUAL = ToTerm("=");
            var DIFERENTE = ToTerm("<>");

            var ASSIGN = ToTerm(":=");
            var PUNTOS = ToTerm("..");

            var PR_PRINT = ToTerm("write");
            var PR_PRINTLN = ToTerm("writeln");

            var TRUE = ToTerm("true");
            var FALSE = ToTerm("false");

            var PR_IF = ToTerm("if");
            var PR_ELSE = ToTerm("else");
            var THEN = ToTerm("then");

            var PR_DO = ToTerm("do");
            var PR_WHILE = ToTerm("while");

            var PR_CASE = ToTerm("case");
            var PR_OF = ToTerm("of");

            var PR_FOR = ToTerm("for");
            var PR_TO = ToTerm("to");
            var PR_DOWNTO = ToTerm("downto");

            var PR_REPEAT = ToTerm("repeat");
            var PR_UNTIL = ToTerm("until");

            var PR_BREAK = ToTerm("break");
            var PR_CONTINUE = ToTerm("continue");

            var VAR = ToTerm("var");
            var BEGIN = ToTerm("begin");
            var END = ToTerm("end");
            var PROGRAM = ToTerm("program");

            var STRING = ToTerm("string");
            var INTEGER = ToTerm("integer");
            var REAL = ToTerm("real");
            var BOOLEAN = ToTerm("boolean");
            //var VOID = ToTerm("void");

            var CONST = ToTerm("const");

            var TYPE = ToTerm("type");
            var PR_OBJECT = ToTerm("object");

            var PR_PROCEDURE = ToTerm("procedure");
            var PR_FUNCTION = ToTerm("function");

            var PR_ARRAY = ToTerm("array");

            //var graficar_ts = ToTerm("graficar_ts");
            var EXIT = ToTerm("Exit");

            RegisterOperators(1, Associativity.Left, OR);
            RegisterOperators(2, Associativity.Left, AND);
            RegisterOperators(3, Associativity.Left, IGUAL, DIFERENTE);
            RegisterOperators(4, Associativity.Neutral, MAYOR, MENOR, MAYORIGUAL, MENORIGUAL);
            RegisterOperators(5, Associativity.Left, MAS, MENOS);
            RegisterOperators(6, Associativity.Left, POR, DIV, MOD);
            RegisterOperators(7, Associativity.Right, NOT);

            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            NonGrammarTerminals.Add(comentarioBloque2);
            #endregion

            #region No Terminales
            NonTerminal INI = new NonTerminal("Ini");
            NonTerminal programa = new NonTerminal("Programa");
            NonTerminal INSTRUCCION = new NonTerminal("Instrucion");
            NonTerminal INSTRUCCIONES = new NonTerminal("Instrucciones");
            NonTerminal EXPRESION = new NonTerminal("Expresion");
            NonTerminal EXPRESION_NUMERICA = new NonTerminal("Expresion_Numerica");
            NonTerminal EXPRESION_UNARIA = new NonTerminal("Expresion_Unaria");
            NonTerminal EXPRESION_LOGICA = new NonTerminal("Expresion_Logica");
            NonTerminal EXPRESION_RELACIONAL = new NonTerminal("Expresion_Relacional");
            NonTerminal PRIMITIVA = new NonTerminal("Primitiva");
            NonTerminal LISTA_EXPRESION = new NonTerminal("Lista_Expresion");
            NonTerminal IMPRIMIR = new NonTerminal("Imprimir");
            NonTerminal DECLARACION = new NonTerminal("Declaracion");
            NonTerminal LISTA_IDENTIFICADOR = new NonTerminal("Lista_Identificador");
            NonTerminal TIPO = new NonTerminal("Tipo");
            NonTerminal DECLARAR_ASIGNAR = new NonTerminal("Declarar_Asignar");
            NonTerminal ASIGNAR = new NonTerminal("Asignar");
            NonTerminal MAIN = new NonTerminal("Main");
            NonTerminal BLOQUE_SENTENCIA = new NonTerminal("Bloque_Sentencia");
            NonTerminal SENTENCIA = new NonTerminal("Sentencia");
            NonTerminal SENTENCIAS = new NonTerminal("Sentencias");
            NonTerminal IF = new NonTerminal("If");
            //NonTerminal ELSE_IF = new NonTerminal("Lista_if");
            //NonTerminal ELSEIF = new NonTerminal("Else_If");
            NonTerminal SWITCH = new NonTerminal("Switch");
            NonTerminal CASOS = new NonTerminal("Casos");
            NonTerminal CASO = new NonTerminal("Caso");
            NonTerminal DEFAULT = new NonTerminal("Default");
            NonTerminal WHILE = new NonTerminal("While");
            NonTerminal FOR = new NonTerminal("For");
            NonTerminal DOWHILE = new NonTerminal("Repeat_Until");
            NonTerminal FUNCION = new NonTerminal("Funcion");
            NonTerminal PROCEDIMIENTO = new NonTerminal("Procedimiento");
            NonTerminal PARAMETROS = new NonTerminal("Parametros");
            NonTerminal PARAMETRO = new NonTerminal("Parametro");
            NonTerminal LOCALES = new NonTerminal("Instrucciones_Locales");
            NonTerminal LOCAL = new NonTerminal("Instruccion_Local");
            NonTerminal OBJETO = new NonTerminal("Objeto");
            NonTerminal ATRIBUTOS = new NonTerminal("Atributos");
            NonTerminal ATRIBUTO = new NonTerminal("Atributo");
            NonTerminal CALL_OBJETO = new NonTerminal("Call_Objeto");
            NonTerminal CALL_FUNCION = new NonTerminal("Call_Funcion");
            NonTerminal ACCESOS = new NonTerminal("Accesos");
            NonTerminal ACCESO = new NonTerminal("Acceso");
            NonTerminal CALL_PARAMETRO = new NonTerminal("Call_Parametro");
            NonTerminal SALIR = new NonTerminal("Exit");
            NonTerminal CONSTANTE = new NonTerminal("Constante");
            NonTerminal ASIGNAR_ATRIBUTO = new NonTerminal("Asignar_Atributo");
            NonTerminal ARREGLO = new NonTerminal("Arreglo");
            NonTerminal INICIALIZAR_DIMENSIONES = new NonTerminal("Inicializar_Dimensiones");
            NonTerminal INICIALIZAR_DIMENSION = new NonTerminal("Inicializar_Dimension");
            NonTerminal CALL_ARREGLO = new NonTerminal("Call_Arreglo");
            NonTerminal DIMENSION = new NonTerminal("Dimension");
            NonTerminal ASIGNAR_ARREGLO = new NonTerminal("Asignar_Arreglo");
            #endregion

            #region Gramatica
            INI.Rule = programa + INSTRUCCIONES;

            programa.Rule = PROGRAM + IDENTIFICADOR + PTCOMA
                          | Empty;

            INSTRUCCIONES.Rule = MakePlusRule(INSTRUCCIONES, INSTRUCCION);

            INSTRUCCION.Rule = DECLARACION + PTCOMA
                             | MAIN
                             | FUNCION + PTCOMA
                             | PROCEDIMIENTO + PTCOMA
                             | OBJETO + PTCOMA
                             | ARREGLO + PTCOMA
                             | CONSTANTE + PTCOMA;

            MAIN.Rule = BLOQUE_SENTENCIA + PUNTO;

            CONSTANTE.Rule = CONST + IDENTIFICADOR + DOSPT + TIPO + IGUAL + EXPRESION;

            ARREGLO.Rule = TYPE + IDENTIFICADOR + IGUAL + PR_ARRAY + CORIZQ + INICIALIZAR_DIMENSIONES + CORDER + PR_OF + TIPO;

            ASIGNAR_ARREGLO.Rule = CALL_ARREGLO + ASSIGN + EXPRESION;

            INICIALIZAR_DIMENSION.Rule = MakePlusRule(INICIALIZAR_DIMENSIONES, COMA, INICIALIZAR_DIMENSION);

            INICIALIZAR_DIMENSION.Rule = EXPRESION + PUNTOS + EXPRESION;

            OBJETO.Rule = TYPE + IDENTIFICADOR + IGUAL + PR_OBJECT + VAR + ATRIBUTOS + END;

            ASIGNAR_ATRIBUTO.Rule = CALL_OBJETO + ASSIGN + EXPRESION;

            ATRIBUTOS.Rule = MakeStarRule(ATRIBUTOS, ATRIBUTO);

            ATRIBUTO.Rule = LISTA_IDENTIFICADOR + DOSPT + TIPO + PTCOMA;

            FUNCION.Rule = PR_FUNCTION + IDENTIFICADOR + PARIZQ + PARAMETROS + PARDER + DOSPT + TIPO + PTCOMA + LOCALES + BLOQUE_SENTENCIA;

            PROCEDIMIENTO.Rule = PR_PROCEDURE + IDENTIFICADOR + PARIZQ + PARAMETROS + PARDER + PTCOMA + LOCALES + BLOQUE_SENTENCIA;

            PARAMETROS.Rule = MakeStarRule(PARAMETROS, PTCOMA, PARAMETRO);

            PARAMETRO.Rule = VAR + LISTA_IDENTIFICADOR + DOSPT + TIPO
                           | LISTA_IDENTIFICADOR + DOSPT + TIPO;

            LOCALES.Rule = MakeStarRule(LOCALES, LOCAL);

            LOCAL.Rule = DECLARACION + PTCOMA
                       | FUNCION + PTCOMA
                       | PROCEDIMIENTO + PTCOMA
                       | OBJETO + PTCOMA
                       | ARREGLO + PTCOMA
                       | CONSTANTE + PTCOMA;

            BLOQUE_SENTENCIA.Rule = BEGIN + SENTENCIAS + END;

            SENTENCIAS.Rule = MakeStarRule(SENTENCIAS, SENTENCIA);

            SENTENCIA.Rule = IMPRIMIR + PTCOMA
                           | ASIGNAR + PTCOMA
                           | IF + PTCOMA
                           | SWITCH + PTCOMA
                           | WHILE + PTCOMA
                           | FOR + PTCOMA
                           | DOWHILE + PTCOMA
                           | SALIR + PTCOMA
                           | CALL_FUNCION + PTCOMA
                           | PR_BREAK + PTCOMA
                           | PR_CONTINUE + PTCOMA
                           | ASIGNAR_ATRIBUTO + PTCOMA
                           | ASIGNAR_ARREGLO + PTCOMA;

            DECLARACION.Rule = VAR + LISTA_IDENTIFICADOR + DOSPT + TIPO 
                             | VAR + IDENTIFICADOR + DOSPT + TIPO + DECLARAR_ASIGNAR;

            DECLARAR_ASIGNAR.Rule = IGUAL + EXPRESION
                                  | Empty;

            ASIGNAR.Rule = IDENTIFICADOR + ASSIGN + EXPRESION;

            LISTA_IDENTIFICADOR.Rule = MakePlusRule(LISTA_IDENTIFICADOR, COMA, IDENTIFICADOR);

            IMPRIMIR.Rule = PR_PRINT + PARIZQ + LISTA_EXPRESION + PARDER
                          | PR_PRINTLN + PARIZQ + LISTA_EXPRESION + PARDER;

            LISTA_EXPRESION.Rule = MakeStarRule(LISTA_EXPRESION, COMA, EXPRESION);

            IF.Rule = PR_IF + EXPRESION + THEN + BLOQUE_SENTENCIA
                    | PR_IF + EXPRESION + THEN + BLOQUE_SENTENCIA + PR_ELSE + BLOQUE_SENTENCIA
                    | PR_IF + EXPRESION + THEN + BLOQUE_SENTENCIA + PR_ELSE + IF;


            //ELSE_IF.Rule = MakePlusRule(ELSE_IF, ELSEIF);

            //ELSEIF.Rule = PR_ELSE + PR_IF + EXPRESION + THEN + BLOQUE_SENTENCIA;

            SWITCH.Rule = PR_CASE + EXPRESION + PR_OF + CASOS + DEFAULT + END;

            CASOS.Rule = MakePlusRule(CASOS, CASO);

            CASO.Rule = EXPRESION + DOSPT + BLOQUE_SENTENCIA + PTCOMA;

            DEFAULT.Rule = PR_ELSE + BLOQUE_SENTENCIA
                         | PR_ELSE + BLOQUE_SENTENCIA + PTCOMA
                         | Empty;

            WHILE.Rule = PR_WHILE + EXPRESION + PR_DO + BLOQUE_SENTENCIA;

            FOR.Rule = PR_FOR + ASIGNAR + PR_TO + EXPRESION + PR_DO + BLOQUE_SENTENCIA
                     | PR_FOR + ASIGNAR + PR_DOWNTO + EXPRESION + PR_DO + BLOQUE_SENTENCIA;

            DOWHILE.Rule = PR_REPEAT + BLOQUE_SENTENCIA + PR_UNTIL + EXPRESION
                         | PR_REPEAT + BLOQUE_SENTENCIA + PTCOMA + PR_UNTIL + EXPRESION;

            EXPRESION.Rule = PRIMITIVA
                           | CALL_OBJETO
                           | CALL_FUNCION
                           | CALL_ARREGLO
                           | EXPRESION_UNARIA
                           | EXPRESION_NUMERICA
                           | EXPRESION_RELACIONAL
                           | EXPRESION_LOGICA
                           | PARIZQ + EXPRESION + PARDER;

            EXPRESION_NUMERICA.Rule = EXPRESION + MAS + EXPRESION
                                    | EXPRESION + MENOS + EXPRESION
                                    | EXPRESION + POR + EXPRESION
                                    | EXPRESION + DIV + EXPRESION
                                    | EXPRESION + MOD + EXPRESION;

            EXPRESION_LOGICA.Rule = EXPRESION + OR + EXPRESION
                                  | EXPRESION + AND + EXPRESION
                                  | NOT + EXPRESION;

            EXPRESION_RELACIONAL.Rule = EXPRESION + MAYOR + EXPRESION
                                      | EXPRESION + MENOR + EXPRESION
                                      | EXPRESION + IGUAL + EXPRESION
                                      | EXPRESION + DIFERENTE + EXPRESION
                                      | EXPRESION + MAYORIGUAL + EXPRESION
                                      | EXPRESION + MENORIGUAL + EXPRESION;

            EXPRESION_UNARIA.Rule = MENOS + EXPRESION;

            PRIMITIVA.Rule = NUMERO
                           | CADENA
                           | TRUE
                           | FALSE
                           | IDENTIFICADOR;

            TIPO.Rule = STRING
                      | INTEGER
                      | REAL
                      | BOOLEAN
                      | IDENTIFICADOR;

            CALL_OBJETO.Rule = IDENTIFICADOR + ACCESOS;

            ACCESOS.Rule = MakePlusRule(ACCESOS, ACCESO);

            ACCESO.Rule = PUNTO + IDENTIFICADOR;

            CALL_FUNCION.Rule = IDENTIFICADOR + PARIZQ + CALL_PARAMETRO + PARDER;

            CALL_PARAMETRO.Rule = MakeStarRule(CALL_PARAMETRO, COMA, EXPRESION);

            CALL_ARREGLO.Rule = IDENTIFICADOR + CORIZQ + DIMENSION + CORDER;

            DIMENSION.Rule = MakePlusRule(DIMENSION, COMA, EXPRESION);

            SALIR.Rule = EXIT + PARIZQ + EXPRESION + PARDER
                       | EXIT + PARIZQ + PARDER;
            #endregion

            #region Preferencias
            this.Root = INI;
            this.MarkTransient(DECLARAR_ASIGNAR, BLOQUE_SENTENCIA, TIPO, ACCESO);
            this.MarkPunctuation(PTCOMA, PARDER, PARIZQ, CORDER, CORIZQ, COMA, PUNTO, BEGIN, END, programa);
            #endregion
        }
    }
}

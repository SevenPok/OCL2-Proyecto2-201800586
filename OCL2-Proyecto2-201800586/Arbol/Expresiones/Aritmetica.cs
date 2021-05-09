using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Aritmetica : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion left, right;
        private Constante.AritmeticaSigno type;

        public Aritmetica(Expresion left, Expresion right, Constante.AritmeticaSigno type, int linea, int columna)
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.linea = linea + 1;
            this.columna = columna + 1;
            trueLabel = falseLabel = "";
        }

        public Aritmetica(Expresion right, Constante.AritmeticaSigno type, int linea, int columna)
        {
            this.right = right;
            this.type = type;
            this.linea = linea + 1;
            this.columna = columna + 1;
            trueLabel = falseLabel = "";
        }
        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();
            Return leftValue = this.left != null ? this.left.traducir(ts) : new Return(0, Constante.Type.INT);
            Return rightValue = this.right.traducir(ts);

            String temp = generator.newTemp();
            

            String op = "";
            switch (this.type)
            {
                case Constante.AritmeticaSigno.MOD:
                    op = "%";
                    break;
                case Constante.AritmeticaSigno.MAS:
                    op = "+";
                    break;
                case Constante.AritmeticaSigno.MENOS:
                    op = "-";
                    break;
                case Constante.AritmeticaSigno.POR:
                    op = "*";
                    break;
                default:
                    op = "/";
                    break;
            }
            //temp = leftValue.valueC op rightValue.valueC;
            generator.AddExp(temp, leftValue.valueC, rightValue.valueC, op);
            if (leftValue.type == Constante.Type.INT && rightValue.type == Constante.Type.INT)
            {
                return new Return(temp, true, Constante.Type.INT);
            }
            else if (
               leftValue.type == Constante.Type.INT && rightValue.type == Constante.Type.DOUBLE ||
               leftValue.type == Constante.Type.DOUBLE && rightValue.type == Constante.Type.INT ||
               leftValue.type == Constante.Type.DOUBLE && rightValue.type == Constante.Type.DOUBLE)
            {
                return new Return(temp, true, Constante.Type.DOUBLE);
            }
            else if (leftValue.type == Constante.Type.STRING && rightValue.type == Constante.Type.STRING && op == "+")
            {
                String tempAux = generator.newTemp(); 
                generator.freeTemp(tempAux);
                generator.AddExp(tempAux, "P", (ts.size + 1).ToString(), op);
                generator.addSetStack("(int)" + tempAux, left.traducir(ts).getValue());
                generator.AddExp(tempAux, tempAux, "1", op);
                generator.addSetStack("(int)" + tempAux, right.traducir(ts).getValue());
                generator.addNextEnv(ts.size.ToString());
                generator.addCallFunc("native_concat"); generator.addNative(Natives.concat);
                generator.addGetStack(temp, "P");
                generator.addAntEnv(ts.size.ToString());
                return new Return(temp, true, Constante.Type.STRING);
            }
            throw new Error(this.linea, this.columna, "Semantico", "No se puede operar un " + leftValue.type + " con un " + rightValue.type);
        }
    }
}

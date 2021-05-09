using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Relacional : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion left, right;
        private Constante.RelacionalSigno type;

        public Relacional(Expresion left, Expresion right, Constante.RelacionalSigno type, int linea, int columna)
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.linea = linea + 1;
            this.columna = columna + 1;
            trueLabel = falseLabel = "";
        }

        public Return traducir(Entorno ts)
        {
            Return leftV = this.left != null ? this.left.traducir(ts) : null;
            Return rightV = this.right.traducir(ts);

            Generator generator = Generator.getInstance();
            Return result = new Return(null, false, Constante.Type.BOOLEAN);


            if(this.trueLabel == "")
            {
                this.trueLabel = generator.newLabel();
            }
            if(this.falseLabel == "")
            {
                this.falseLabel = generator.newLabel();
            }
            generator.addIf(leftV.valueC, rightV.valueC, getSymbol(), trueLabel);
            generator.addGoto(this.falseLabel);

            result.trueLabel = trueLabel;
            result.falseLabel = falseLabel;
            return result;

        }

        public String getSymbol()
        {
            switch (this.type)
            {
                case Constante.RelacionalSigno.DIFERENTE:
                    return "!=";
                case Constante.RelacionalSigno.IGUAL:
                    return "==";
                case Constante.RelacionalSigno.MAYORIGUAL:
                    return ">=";
                case Constante.RelacionalSigno.MAYOR:
                    return ">";
                case Constante.RelacionalSigno.MENOR:
                    return "<";
                case Constante.RelacionalSigno.MENORIGUAL:
                    return "<=";
            }
            return "";
        }
    }
}

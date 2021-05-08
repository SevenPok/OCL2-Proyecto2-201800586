using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Logica : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion left, right;
        private Constante.LogicaSigno type;

        public Logica(Expresion left, Expresion right, Constante.LogicaSigno type, int linea, int columna)
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.linea = linea;
            this.columna = columna;
            trueLabel = falseLabel = "";
        }

        public Logica(Expresion right, Constante.LogicaSigno type, int linea, int columna)
        {
            this.right = right;
            this.type = type;
            this.linea = linea;
            this.columna = columna;
            trueLabel = falseLabel = "";
        }
        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();
            switch (this.type)
            {
                case Constante.LogicaSigno.AND:
                    this.trueLabel = this.trueLabel == "" ? generator.newLabel() : this.trueLabel;
                    this.falseLabel = this.falseLabel == "" ? generator.newLabel() : this.falseLabel;

                    this.left.trueLabel = generator.newLabel();
                    this.right.trueLabel = this.trueLabel;
                    this.left.falseLabel = this.right.falseLabel = this.falseLabel;

                    Return left = this.left.traducir(ts);
                    generator.addLabel(this.left.trueLabel);
                    Return right = this.right.traducir(ts);
                    if (left.type == Constante.Type.BOOLEAN && right.type == Constante.Type.BOOLEAN)
                    {
                        Return auxReturn = new Return("", false, left.type);
                        auxReturn.trueLabel = this.trueLabel;
                        auxReturn.falseLabel = this.right.falseLabel;
                        return auxReturn;
                    }
                    break;
                case Constante.LogicaSigno.OR:
                    this.trueLabel = this.trueLabel == "" ? generator.newLabel() : this.trueLabel;
                    this.falseLabel = this.falseLabel == "" ? generator.newLabel() : this.falseLabel;

                    this.left.trueLabel = this.right.trueLabel = this.trueLabel;
                    this.left.falseLabel = generator.newLabel();
                    this.right.falseLabel = this.falseLabel;

                    Return izq = this.left.traducir(ts);
                    generator.addLabel(this.left.falseLabel);
                    Return der = this.right.traducir(ts);

                    if (izq.type == Constante.Type.BOOLEAN && der.type == Constante.Type.BOOLEAN)
                    {
                        Return auxReturn = new Return("", false, izq.type);
                        auxReturn.trueLabel = this.trueLabel;
                        auxReturn.falseLabel = this.right.falseLabel;
                        return auxReturn;
                    }
                    break;
                case Constante.LogicaSigno.NOT:
                    this.trueLabel = this.trueLabel == "" ? generator.newLabel() : this.trueLabel;
                    this.falseLabel = this.falseLabel == "" ? generator.newLabel() : this.falseLabel;

                    this.right.trueLabel = this.falseLabel;
                    this.right.falseLabel = this.trueLabel;

                    Return value = this.right.traducir(ts);

                    if (value.type == Constante.Type.BOOLEAN)
                    {
                        Return auxReturn = new Return("", false, value.type);
                        auxReturn.trueLabel = this.trueLabel;
                        auxReturn.falseLabel = this.falseLabel;
                        return auxReturn;
                    }
                    break;

            }

            throw new Error(this.linea, this.columna, "Semantico", "No se puede operar");
        }
    }
}

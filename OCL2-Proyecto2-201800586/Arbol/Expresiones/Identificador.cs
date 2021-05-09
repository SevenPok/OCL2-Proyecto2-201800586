using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Identificador : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private string id;
        private Expresion previous;

        // Access id
        public Identificador(String id, Expresion previous, int linea, int columna)
        {
            this.id = id;
            this.previous = previous;
            this.linea = linea + 1;
            this.columna = columna + 1;
            trueLabel = falseLabel = "";
        }

        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();
            Simbolo symbol = ts.getVariable(this.id);
            if (symbol == null) throw new Error(this.linea, this.columna, "Semantico", "No se encontro la varaible: " + id);

            String temp = generator.newTemp();
            if (symbol.isGlobal)
            {
                generator.addGetStack(temp, symbol.position.ToString());
            }
            else
            {
                String tempAux = generator.newTemp();
                generator.freeTemp(tempAux);
                generator.AddExp(tempAux, "P", symbol.position.ToString(), "+");
                generator.addGetStack(temp, tempAux);
            }
            if (symbol.type != Constante.Type.BOOLEAN) return new Return(temp, true, symbol.type, symbol);
            generator.freeTemp(temp);
            Return auxReturn = new Return("", false, symbol.type, symbol);
            this.trueLabel = this.trueLabel == "" ? generator.newLabel() : this.trueLabel;
            this.falseLabel = this.falseLabel == "" ? generator.newLabel() : this.falseLabel;
            generator.addIf(temp, "1", "==", this.trueLabel);
            generator.addGoto(this.falseLabel);
            auxReturn.trueLabel = this.trueLabel;
            auxReturn.falseLabel = this.falseLabel;
            return auxReturn;
        }

        public Return traducir(Entorno env, bool state)
        {
            Generator generator = Generator.getInstance();
            Simbolo symbol = env.getVariable(this.id);
            if (symbol == null) throw new Error(this.linea, this.columna, "Semantico", "No se encontro la varaible: " + id);
            if (state)
            {
                if (symbol.isGlobal) return new Return(symbol.position + "", false, symbol.type, symbol);
                else
                {
                    String temp = generator.newTemp();
                    generator.AddExp(temp, "P", symbol.position.ToString(), "+");
                    return new Return(temp, true, symbol.type, symbol);
                }
            }
            else
            {
                String temp = generator.newTemp();
                if (symbol.isGlobal)
                {
                    generator.addGetStack(temp, symbol.position.ToString());
                }
                else
                {
                    String tempAux = generator.newTemp();
                    generator.freeTemp(tempAux);
                    generator.AddExp(tempAux, "P", symbol.position.ToString(), "+");
                    generator.addGetStack(temp, tempAux);
                }
                if (symbol.type != Constante.Type.BOOLEAN) return new Return(temp, true, symbol.type, symbol);
                generator.freeTemp(temp);
                Return auxReturn = new Return("", false, symbol.type, symbol);
                this.trueLabel = this.trueLabel == "" ? generator.newLabel() : this.trueLabel;
                this.falseLabel = this.falseLabel == "" ? generator.newLabel() : this.falseLabel;
                generator.addIf(temp, "1", "==", this.trueLabel);
                generator.addGoto(this.falseLabel);
                auxReturn.trueLabel = this.trueLabel;
                auxReturn.falseLabel = this.falseLabel;
                return auxReturn;
            }
        }
    }
}

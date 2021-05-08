using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class AccesoFuncion : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private String id;
        private LinkedList<Expresion> parametros;

        public AccesoFuncion(String id, LinkedList<Expresion> parametros, int linea, int columna)
        {
            this.id = id;
            this.parametros = parametros;
            this.linea = linea;
            this.columna = columna;
        }
        public Return traducir(Entorno ts)
        {
            SimboloFuncion symbolFunction = ts.searchFunction(this.id);
            if (symbolFunction == null) throw new Error(this.linea, this.columna, "Semantical", "No se encontro la fucnion");
            LinkedList<Return> paramsValues = new LinkedList<Return>();
            Generator generator = Generator.getInstance();
            int size = generator.saveTemps(ts);

            int registeredLength = symbolFunction.parametros.Count;
            int incomingLength = this.parametros.Count;
            if (registeredLength != incomingLength) throw new Error(this.linea, this.columna, "Semantical", "No se esperaban esos argumentos");
            int i = 0;
            String temp;
            foreach (Expresion p in parametros)
            {
                Return compiledParam = p.traducir(ts);
                Constante.Type registeredType = symbolFunction.parametros.ElementAt(i).type;
                Constante.Type incomingType = compiledParam.type;
                if (registeredType != incomingType) throw new Error(this.linea, this.columna, "Semantical", "El tipo de dato no es el esperado");
                if (incomingType == Constante.Type.BOOLEAN)
                {
                    temp = generator.newTemp();
                    generator.freeTemp(temp);
                    String templabel = generator.newLabel();
                    generator.addLabel(compiledParam.trueLabel);
                    generator.AddExp(temp, "P", (ts.size + i + 1).ToString(), "+");
                    generator.addSetStack("(int)" + temp, "1");
                    generator.addGoto(templabel);
                    generator.addLabel(compiledParam.falseLabel);
                    generator.AddExp(temp, "P", (ts.size + i + 1).ToString(), "+");
                    generator.addSetStack(temp, "0");
                    generator.addLabel(templabel);
                }
                paramsValues.AddLast(compiledParam);
                i++;
            }
            temp = generator.newTemp();
            generator.freeTemp(temp);
            if (paramsValues.Count != 0)
            {
                generator.AddExp(temp, "P", (ts.size + 1).ToString(), "+");
                int index = 0;
                foreach(Return value in paramsValues)
                {
                    if (value.type != Constante.Type.BOOLEAN)
                        generator.addSetStack("(int)" + temp, value.getValue());
                    if (index != paramsValues.Count - 1)
                        generator.AddExp(temp, temp, "1", "+");
                    index++;
                }
            }

            generator.addNextEnv(ts.size.ToString());
            generator.addCallFunc(symbolFunction.id);
            generator.addGetStack(temp, "P");
            generator.addAntEnv(ts.size.ToString());
            generator.recoverTemps(ts, size);
            generator.addTemp(temp);

            if (symbolFunction.type != Constante.Type.BOOLEAN) return new Return(temp, true, symbolFunction.type);
            Return auxReturn = new Return("", false, symbolFunction.type);
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

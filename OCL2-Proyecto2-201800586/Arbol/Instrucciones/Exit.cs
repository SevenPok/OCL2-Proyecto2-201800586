using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Exit : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion value;

        public Exit(Expresion value, int linea, int columna)
        {
            this.value = value;
            this.linea = columna;
        }
        public Return traducir(Entorno ts)
        {
            Return value = this.value?.traducir(ts);
            if(value == null)
            {
                value = new Return("0", false, Constante.Type.VOID);
            }
            SimboloFuncion symbolFunction = ts.actualFunction;
            Generator generator = Generator.getInstance();

            if (symbolFunction == null) throw new Error(this.linea, this.columna, "Semantico", "Error con la funcion exit");

            if (!Constante.sameType(symbolFunction.type, value.type)) throw new Error(this.linea, this.columna, "Semantical", "El retorno no es del mismo tipo");

            if (symbolFunction.type == Constante.Type.BOOLEAN)
            {
                String templabel = generator.newLabel();
                generator.addLabel(value.trueLabel);
                generator.addSetStack("P", "1");
                generator.addGoto(templabel);
                generator.addLabel(value.falseLabel);
                generator.addSetStack("p", "0");
                generator.addLabel(templabel);
            }
            else if (symbolFunction.type != Constante.Type.VOID) generator.addSetStack("P", value.getValue());
            generator.addGoto(ts._return);
            return null;
        }
    }
}

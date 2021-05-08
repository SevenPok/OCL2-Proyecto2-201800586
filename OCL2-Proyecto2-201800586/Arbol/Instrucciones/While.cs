using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class While : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion condicion;
        private LinkedList<Instruccion> instrucciones;

        public While(Expresion condicion, LinkedList<Instruccion> instrucciones, int linea, int columna)
        {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.linea = linea;
            this.columna = columna;
        }
        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();
            Entorno newEnv = new Entorno(ts);
            String lblWhile = generator.newLabel();
            generator.addComment("BEGIN while");
            generator.addLabel(lblWhile);
            Return condicion = this.condicion.traducir(ts);
            if (condicion.type == Constante.Type.BOOLEAN)
            {
                //newEnv.break = condition.falseLabel;
                //newEnv.continue = lblWhile;
                generator.addLabel(condicion.trueLabel);
                foreach(Instruccion i in instrucciones)
                {
                    i.traducir(newEnv);
                }
                generator.addGoto(lblWhile);
                generator.addLabel(condicion.falseLabel);
                generator.addComment("END while");
                return null;
            }
            throw new Error(this.linea, this.columna, "Semantical", "Condition found not boolean");
        }
    }
}

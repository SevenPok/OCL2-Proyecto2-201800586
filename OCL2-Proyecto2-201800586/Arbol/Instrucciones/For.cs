using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class For : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Asignacion asignacion;
        private Expresion condicion;
        private Asignacion inc_dec;
        private LinkedList<Instruccion> instrucciones;
        private Asignacion regresar;

        public For(Asignacion asignacion, Expresion condicion, Asignacion inc_dec, LinkedList<Instruccion> instrucciones, Asignacion regresar,int linea, int columna)
        {
            this.asignacion = asignacion;
            this.condicion = condicion;
            this.inc_dec = inc_dec;
            this.instrucciones = instrucciones;
            this.regresar = regresar;
            this.linea = linea;
            this.columna = columna;
        }

        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();

            String lblWhile = generator.newLabel();
            generator.addComment("BEGIN For");
            Entorno newEnv = new Entorno(ts);
            asignacion.traducir(ts);

            generator.addLabel(lblWhile);

            Return condicion = this.condicion.traducir(ts);
            if (condicion.type == Constante.Type.BOOLEAN)
            {
                //newEnv.break = condition.falseLabel;
                //newEnv.continue = lblWhile;
                generator.addLabel(condicion.trueLabel);
                foreach (Instruccion i in instrucciones)
                {
                    i.traducir(newEnv);
                }
                inc_dec.traducir(newEnv);
                generator.addGoto(lblWhile);
                generator.addLabel(condicion.falseLabel);
                regresar.traducir(ts);
                generator.addComment("END For");
                return null;
            }
            throw new Error(this.linea, this.columna, "Semantical", "Condition found not boolean");
        }
    }
}

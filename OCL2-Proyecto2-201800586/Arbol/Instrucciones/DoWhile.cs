using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class DoWhile : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion condicion;
        private LinkedList<Instruccion> instrucciones;

        public DoWhile(Expresion condicion, LinkedList<Instruccion> instrucciones, int linea, int columna)
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
            generator.addComment("BEGIN Repeat");
            //newEnv.continue = this.condition.trueLabel = generator.newLabel();
            //newEnv.break = this.condition.falseLabel = generator.newLabel();
           
            String verdadero = condicion.falseLabel = generator.newLabel();
            String falso = condicion.trueLabel = generator.newLabel();
            generator.addLabel(verdadero);
            foreach (Instruccion i in instrucciones)
            {
                i.traducir(newEnv);
            }

            Return condition = this.condicion.traducir(ts);
            if (condition.type == Constante.Type.BOOLEAN)
            {
                generator.addLabel(falso);
                generator.addComment("END DoWhile");
                return null; 
            }
            throw new Error(this.linea, this.columna, "Semantical", "Condition found not boolean");
            
        }
    }
}

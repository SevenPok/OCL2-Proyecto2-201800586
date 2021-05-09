using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class If : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private Expresion condicion;
        private LinkedList<Instruccion> instrucciones;
        public Instruccion elseif;

        public If(Expresion condicion, LinkedList<Instruccion> instrucciones, Instruccion elseif, int linea, int columna)
        {
            this.condicion = condicion;
            this.instrucciones = instrucciones;
            this.elseif = elseif;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();
            generator.addComment("BEGIN if");
            Return condition = this.condicion?.traducir(ts); condition.getValue();
            Entorno newEnv = new Entorno(ts);
            if (condition.type == Constante.Type.BOOLEAN)
            {
                generator.addLabel(condition.trueLabel);
                foreach(Instruccion i in instrucciones)
                {
                    i.traducir(newEnv);
                }
                if (this.elseif != null)
                {
                    String tempLbl = generator.newLabel();
                    generator.addGoto(tempLbl);
                    generator.addLabel(condition.falseLabel);
                    this.elseif.traducir(ts);
                    generator.addLabel(tempLbl);
                }
                else
                {
                    generator.addLabel(condition.falseLabel);
                }

            }
            else
            {
                throw new Error(this.linea, this.columna, "Semantico", "La condicion encontrada no es booleana");
            }
            generator.addComment("END if");
            return null;
        }
    }
}

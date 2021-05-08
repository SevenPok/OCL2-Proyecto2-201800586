using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Asignacion : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private String id;
        private Expresion value;

        public Asignacion(String id, Expresion value, int linea, int columna)
        {
            this.id = id;
            this.value = value;
            this.linea = linea;
            this.columna = columna;
        }
        public Return traducir(Entorno ts)
        {
            Simbolo target = ts.getVariable(id);
            if(target != null)
            {
                if (target.isConst)
                    throw new Error(this.linea, this.columna, "Semantical", "No se puede asignar la varaible: " + id + ", ya que es una cosntante");
                Return value = this.value.traducir(ts);

                Generator generator = Generator.getInstance();

                if (target.type == Constante.Type.DOUBLE)
                {
                    if (value.type == Constante.Type.INT || value.type == Constante.Type.DOUBLE)
                    {

                    }
                    else
                    {
                        throw new Error(this.linea, this.columna, "Semantico", "No se puede operars");
                    }
                }
                else if (!Constante.sameType(target.type, value.type)) throw new Error(this.linea, this.columna, "Semantical", "No se puede asignar la varaible: " + id + ", ya que el tipo de dato no coincide");

                //const ptr = symbol?.isGlobal ? symbol.position : target.getValue()
                String ptr = target.position.ToString();
                if (target.type == Constante.Type.BOOLEAN)
                {
                    String templabel = generator.newLabel();
                    generator.addLabel(value.trueLabel);
                    generator.addSetStack(ptr, "1");
                    generator.addGoto(templabel);
                    generator.addLabel(value.falseLabel);
                    generator.addSetStack(ptr, "0");
                    generator.addLabel(templabel);
                }
                else generator.addSetStack(ptr, value.getValue());
            }
            else
            {
                throw new Error(this.linea, this.columna, "Semantical", "No se encontro la variable: " + id);
            }
            return null;
        }
    }
}

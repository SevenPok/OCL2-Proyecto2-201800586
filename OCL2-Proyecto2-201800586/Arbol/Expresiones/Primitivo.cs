using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Primitivo : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get ; set ; }
        public string falseLabel { get ; set ; }

        private object valor;
        private Constante.Type type;
        public Primitivo(object valor, Constante.Type type, int linea, int columna)
        {
            this.valor = valor;
            this.type = type;
            this.linea = linea;
            this.columna = columna;
            trueLabel = falseLabel = "";
        }
        public Return traducir(Entorno ts)
        {
            //Console.WriteLine("Aqui pase");
            Generator generator = Generator.getInstance();
            switch (this.type)
            {
                case Constante.Type.INT:
                    return new Return(this.valor.ToString(), false, Constante.Type.INT);
                case Constante.Type.DOUBLE:
                    return new Return(this.valor.ToString(), false, Constante.Type.DOUBLE);
                case Constante.Type.BOOLEAN:
                    
                    Return auxReturn = new Return("", false, this.type);
                    this.trueLabel = this.trueLabel == "" ? generator.newLabel() : this.trueLabel;
                    this.falseLabel = this.falseLabel == "" ? generator.newLabel() : this.falseLabel;
                    if (Boolean.Parse(this.valor.ToString()))
                    {
                        generator.addGoto(trueLabel);
                    }
                    else
                    {
                        generator.addGoto(falseLabel);
                    }

                    auxReturn.trueLabel = this.trueLabel;
                    auxReturn.falseLabel = this.falseLabel;
                    return auxReturn;
                case Constante.Type.STRING:
                    String temp = generator.newTemp();
                    generator.AddExp(temp, "H");
                    foreach(char c in this.valor.ToString())
                    {
                        generator.addSetHeap("H", ((int)c).ToString());
                        generator.nextHeap();
                    }
                    generator.addSetHeap("H", "-1");
                    generator.nextHeap();
                    return new Return(temp, true, Constante.Type.STRING);
                default:
                    throw new Error(this.linea, this.columna, "Semantical", "No existe el tipo de dato");
            }
        }
    }
}

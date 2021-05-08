using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{    class Print : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private LinkedList<Expresion> expresiones;
        private bool salto;

        public Print(LinkedList<Expresion> expresiones, bool salto, int linea, int columna)
        {
            this.expresiones = expresiones;
            this.salto = salto;
            this.linea = linea;
            this.columna = columna;
        }
        public Return traducir(Entorno ts)
        {
            //Console.WriteLine("Aqui pase");
            Generator generator = Generator.getInstance();
            foreach (Expresion e in expresiones)
            {
                Return val = e.traducir(ts);
                switch (val.type)
                {
                    case Constante.Type.INT:
                        generator.addPrint("d", "(int)" + val.valueC);
                        break;
                    case Constante.Type.DOUBLE:
                        generator.addPrint("f", "(float)" + val.valueC);
                        break;
                    case Constante.Type.BOOLEAN:
                        String tempLbl = generator.newLabel();
                        generator.addLabel(val.trueLabel);
                        generator.printTrue();
                        generator.addGoto(tempLbl);
                        generator.addLabel(val.falseLabel);
                        generator.printFalse();
                        generator.addLabel(tempLbl);
                        break;
                    case Constante.Type.STRING:
                        generator.addNextEnv(ts.size.ToString());
                        generator.addSetStack("P", val.getValue());
                        generator.addCallFunc("native_print_str");
                        generator.addNative(Natives.print_str);
                        generator.addAntEnv(ts.size.ToString());
                        break;
                }
            }
            if (salto)
            {
                generator.addPrint("c", "(char)10");
            }
            return null;
        }
    }
}

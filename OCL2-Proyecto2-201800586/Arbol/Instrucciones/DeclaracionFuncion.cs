using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class DeclaracionFuncion : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }

        private bool preCompile;
        private LinkedList<Instruccion> declaraciones;
        private LinkedList<Instruccion> instrucciones;
        public String id;
        public LinkedList<Parametro> parametros;
        public Constante.Type type;

        public DeclaracionFuncion(String id, LinkedList<Parametro> parametros, Constante.Type type, LinkedList<Instruccion> declaraciones, LinkedList<Instruccion> instrucciones, int linea, int columna)
        {
            this.preCompile = true;
            this.id = id;
            this.parametros = parametros;
            this.type = type;
            this.declaraciones = declaraciones;
            this.instrucciones = instrucciones;
            this.linea = linea + 1;
            this.columna = columna + 1;
        }
        public Return traducir(Entorno ts)
        {
            if (this.preCompile)
            {
                this.preCompile = false;
                this.validateParams();
                if (!ts.addFunction(this)) throw new Error(this.linea, this.columna, "Semantical", "La funcion '" + id + "' ya existe");
                return null;
            }


            Generator generator = Generator.getInstance();
            Entorno newEnv = new Entorno(ts);

            SimboloFuncion symbolFunction = ts.getFunction(this.id);
            String returnlbl = generator.newLabel();
            LinkedList<String> tempStorage = generator.getTempStorage();

            newEnv.setEnvironmentFunction(symbolFunction, returnlbl);

            foreach(Parametro param in parametros)
            {
                newEnv.addVariable(param.id, param.type, false, false, linea, columna);
            }

            generator.clearTempStorage();
            LinkedList<String> auxCode = generator.saveCode();
            generator.clearPrevious();
            generator.addBegin(symbolFunction.id);

            foreach (Instruccion i in declaraciones)
            {
                i.traducir(newEnv);
            }

            foreach (Instruccion i in instrucciones)
            {
                i.traducir(newEnv);
            }
            generator.addLabel(returnlbl);
            generator.addEnd();
            generator.addFunction();
            generator.setCode(auxCode);

            generator.setTempStorage(tempStorage);
            return null;
        }
        private void validateParams()
        {
            LinkedList<String> set = new LinkedList<string>();
            foreach(Parametro p in parametros)
            {
                if (set.Contains(p.id)) throw new Error(this.linea, this.columna, "Semantical;", "El identificador: '" + p.id + "' ya existe");
                set.AddLast(p.id);
            }
        }
    }
}

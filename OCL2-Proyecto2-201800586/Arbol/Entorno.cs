using OCL2_Proyecto2_201800586.Arbol.Instrucciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class Entorno
    {
        public Dictionary<String, Simbolo> variables;
        public Dictionary<String, SimboloFuncion> funciones;
        public Entorno prev;
        public int size;
        public string _continue;
        public string _return;
        public string _break;
        public SimboloFuncion actualFunction;
        public Entorno(Entorno prev)
        {
            this.variables = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, SimboloFuncion>();
            this.prev = prev;
            this.size = this.prev == null ? 0 : this.prev.size;
            this._break = prev?._break;
            this._return = prev?._return;
            this._continue = prev?._continue;
            this.actualFunction = prev?.actualFunction;
        }

        public void agregarVariable(String identificador, Simbolo simbolo)
        {
            if (variables.ContainsKey(identificador))
            {
                return;
            }
            variables.Add(identificador, simbolo);
        }

        public Simbolo addVariable(String identificador, Constante.Type type, bool isConst, bool isHeap, int linea, int columna)
        {
            if (this.variables.ContainsKey(identificador))
            {
                return null;
            }
            Simbolo simbolo = new Simbolo(type, identificador, this.size++, isConst, this.prev == null, isHeap, linea, columna);
            variables.Add(identificador, simbolo);
            return simbolo;
        }

        public bool existeVariable(String identificador)
        {
            if (this.variables.ContainsKey(identificador))
            {
                return true;
            }
            return false;
        }

        public Simbolo getVariable(String identificador)
        {
            Entorno env = this;
            while (env != null)
            {
                if (env.variables.ContainsKey(identificador))
                {
                    return env.variables[identificador];
                }
                env = env.prev;
            }
            return null;
        }

        public bool addFunction(DeclaracionFuncion func) {
            if (this.funciones.ContainsKey(func.id)) return false;
            this.funciones.Add(func.id, new SimboloFuncion(func));
            return true;
        }

        public SimboloFuncion getFunction(string id) {
            //@ts-ignore
            return this.funciones[id];
        }

        public SimboloFuncion searchFunction(string id) {
            
            Entorno env = this;
            while (env != null)
            {
                if (env.funciones.ContainsKey(id))
                {
                    return env.funciones[id];
                }
                
                env = env.prev;
            }
            return null;
        }

        public void setEnvironmentFunction(SimboloFuncion actualFunction, string ret)
        {
            this.size = 1;
            this._return = ret;
            this.actualFunction = actualFunction;
        }

    }
}

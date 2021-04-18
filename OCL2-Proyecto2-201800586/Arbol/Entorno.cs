using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    class Entorno
    {
        private Dictionary<String, Simbolo> variables;
        public Entorno prev;

        public Entorno()
        {
            this.variables = new Dictionary<string, Simbolo>();
            this.prev = null;
        }

        public void agregarVariable(String identificador, Simbolo simbolo)
        {
            if (variables.ContainsKey(identificador))
            {
                return;
            }
            variables.Add(identificador, simbolo);
        }

        public bool existeVariable(String identificador)
        {
            if (variables.ContainsKey(identificador))
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
    }
}

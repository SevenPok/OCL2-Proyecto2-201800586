using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Interfaces
{
    class Return
    {
        public String identificador;
        public object value;
        public Constante.Type type;
        public String valueC;
        public Boolean isTemp;
        public String trueLabel, falseLabel;
        public Simbolo simbolo;

        public Return(object value, Constante.Type type)
        {
            this.value = value;
            this.type = type;
        }
        public Return(string identificador, object value, Constante.Type type)
        {
            this.identificador = identificador;
            this.value = value;
            this.type = type;
        }

        public Return(String value, Boolean isTemp, Constante.Type type, Simbolo simbolo = null)
        {
            this.valueC = value;
            this.isTemp = isTemp;
            this.type = type;
            this.simbolo = simbolo;
            this.trueLabel = this.falseLabel = "";
        }

        public String getValue()
        {
            Generator.getInstance().freeTemp(this.valueC);
            return this.valueC;
        }
    }
}

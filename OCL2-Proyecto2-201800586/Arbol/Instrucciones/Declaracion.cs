﻿using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Instrucciones
{
    class Declaracion : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public string trueLabel { get; set; }
        public string falseLabel { get; set; }


        private bool access;
        private string id;
        private Constante.Type type;
        private Expresion value;
        public Declaracion(bool access, string id, Constante.Type type, Expresion value, int linea, int columna)
        {
            this.access = access;
            this.id = id;
            this.type = type;
            this.value = value;
            this.linea = linea;
            this.columna = columna;
        }

        public Return traducir(Entorno ts)
        {
            Generator generator = Generator.getInstance();
            Return compiled = this.value?.traducir(ts);

            if (compiled == null) throw new Error(this.linea, this.columna, "Semantico", "No se puede operars");
            if (this.type == Constante.Type.DOUBLE)
            {
                if (compiled.type == Constante.Type.INT || compiled.type == Constante.Type.DOUBLE)
                {

                }
                else
                {
                    throw new Error(this.linea, this.columna, "Semantico", "No se puede operars");
                }
            }
            else if (!Constante.sameType(this.type, compiled.type)) throw new Error(this.linea, this.columna, "Semantico", "No se puede operars");
            Simbolo newVariable = ts.addVariable(this.id, this.type, this.access, false);
            if (newVariable == null) throw new Error(this.linea, this.columna, "Semantico", "No se puede operars");
            if (newVariable.isGlobal)
            {
                if (this.type == Constante.Type.BOOLEAN)
                {
                    String templabel = generator.newLabel();
                    generator.addLabel(compiled.trueLabel);
                    generator.addSetStack(newVariable.position.ToString(), "1");
                    generator.addGoto(templabel);
                    generator.addLabel(compiled.falseLabel);
                    generator.addSetStack(newVariable.position.ToString(), "0");
                    generator.addLabel(templabel);
                }
                else generator.addSetStack(newVariable.position.ToString(), compiled.getValue());
            }
            else
            {
                String temp = generator.newTemp(); generator.freeTemp(temp);
                if (this.type == Constante.Type.BOOLEAN)
                {
                    String templabel = generator.newLabel();
                    generator.addLabel(compiled.trueLabel);
                    generator.AddExp(temp, "P", newVariable.position.ToString(), "+");
                    generator.addSetStack(temp, "1");
                    generator.addGoto(templabel);
                    generator.addLabel(compiled.falseLabel);
                    generator.AddExp(temp, "P", newVariable.position.ToString(), "+");
                    generator.addSetStack(temp, "0");
                    generator.addLabel(templabel);
                }
                else
                {
                    generator.AddExp(temp, "P", newVariable.position.ToString(), "+");
                    generator.addSetStack(temp, compiled.getValue());
                }
            }
            return null;
        }
    
    }
}

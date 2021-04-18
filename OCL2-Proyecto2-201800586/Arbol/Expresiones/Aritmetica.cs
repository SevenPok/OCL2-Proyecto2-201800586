﻿using OCL2_Proyecto2_201800586.Arbol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol.Expresiones
{
    class Aritmetica : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private Expresion left, right;
        private Constante.AritmeticaSigno type;

        public Aritmetica(Expresion left, Expresion right, Constante.AritmeticaSigno type, int linea, int columna)
        {
            this.left = left;
            this.right = right;
            this.type = type;
            this.linea = linea;
            this.columna = columna;
        }

        public Aritmetica(Expresion right, Constante.AritmeticaSigno type, int linea, int columna)
        {
            this.right = right;
            this.type = type;
            this.linea = linea;
            this.columna = columna;
        }
        public object traducir(Entorno ts)
        {
            return null;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Graficar
{
    class GenerarGrafo
    {
        String ruta;
        StringBuilder grafica;
        public GenerarGrafo()
        {
            ruta = "C:\\Compiladores2";
        }
        private void generarDot(String rdot, String rpng)
        {
            System.IO.File.WriteAllText(rdot, grafica.ToString());
            String comandoDot = "dot.exe -Tsvg " + rdot + " -o " + rpng + " ";
            var comando = String.Format(comandoDot);
            var procesoStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comando);
            var procedimiento = new System.Diagnostics.Process();
            procedimiento.StartInfo = procesoStart;
            procedimiento.Start();
            procedimiento.WaitForExit();
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\AST.svg")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public void graficar(String texto)
        {
            grafica = new StringBuilder();
            String rdot = ruta + "\\AST.dot";
            String rpng = ruta + "\\AST.svg ";
            grafica.Append(texto);
            this.generarDot(rdot, rpng);
        }
    }
}

using OCL2_Proyecto2_201800586.Arbol;
using OCL2_Proyecto2_201800586.Arbol.Instrucciones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Graficar
{
    class Reporte
    {
        public void Html_Errores(LinkedList<Error> listaError)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>ERRORES ENCONTRADOS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Tipo" +
            "</strong></td>" +

            "<td><strong>Descripcion" +
            "</strong></td>" +

            "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;

            foreach (Error e in listaError)
            {

                tempo_tokens = "";
                tempo_tokens = "<tr>" +

                "<td>" + e.type.ToString() +
                "</td>" +

                "<td>" + e.msg +
                "</td>" +

                "<td>" + e.line +
                "</td>" +

                "<td>" + e.column +
                "</td>" +

                "</tr>";
                Cad_tokens = Cad_tokens + tempo_tokens;
            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Reporte_de_Errores.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Reporte_de_Errores.html")
            {
                UseShellExecute = true
            };
            p.Start();


        }
        public void HTML_ts(Entorno env)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>Tabla de Simbolos</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Id" +
            "</strong></td>" +

             "<td><strong>Tipo Simbolo" +
            "</strong></td>" +

            "<td><strong>Ambito" +
            "</strong></td>" +

            "<td><strong>Linea" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;

            Entorno ts = env;
            while(ts != null)
            {
                if (ts.variables != null)
                {
                    foreach (var s in ts.variables)
                    {
                        tempo_tokens = "";
                        tempo_tokens = "<tr>" +

                        "<td>" + s.Value.id +
                        "</td>" +

                        "<td>" + s.Value.type.ToString() +
                        "</td>" +

                        "<td>" + "Global" +
                        "</td>" +

                        "<td>" + s.Value.linea +
                        "</td>" +

                         "<td>" + s.Value.columna +
                        "</td>" +

                        "</tr>";
                        Cad_tokens = Cad_tokens + tempo_tokens;
                    }
                }

                if (ts.funciones != null)
                {
                    foreach (var f in ts.funciones)
                    {

                        tempo_tokens = "";
                        tempo_tokens = "<tr>" +

                        "<td>" + f.Value.id +
                        "</td>" +

                        "<td>" + f.Value.type.ToString() +
                        "</td>" +

                        "<td>" + "Global" +
                        "</td>" +

                        "<td>" + f.Value.linea +
                        "</td>" +

                         "<td>" + f.Value.columna +
                        "</td>" +

                        "</tr>";
                        Cad_tokens = Cad_tokens + tempo_tokens;
                    }

                    foreach (var f in ts.funciones)
                    {


                        foreach (Parametro parametro in f.Value.parametros)
                        {
                            tempo_tokens = "";
                            tempo_tokens = "<tr>" +

                            "<td>" + parametro.id +
                            "</td>" +

                            "<td>" + parametro.type.ToString() +
                            "</td>" +

                            "<td>" + "Local en: " + f.Value.id +
                            "</td>" +

                            "<td>" + parametro.liena +
                            "</td>" +

                             "<td>" + parametro.columna +
                            "</td>" +

                            "</tr>";
                            Cad_tokens = Cad_tokens + tempo_tokens;
                        }
                    }
                }
                ts = ts.prev;
            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Tabla_Simbolos.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Tabla_Simbolos.html")
            {
                UseShellExecute = true
            };
            p.Start();


        }
    }
}

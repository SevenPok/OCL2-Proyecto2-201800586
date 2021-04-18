using Irony;
using Irony.Parsing;
using OCL2_Proyecto2_201800586.Analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCL2_Proyecto2_201800586
{
    public partial class Form1 : Form
    {
        public static RichTextBox Consola;
        public Form1()
        {
            InitializeComponent();
            Consola = richTextBox2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (codigo.Text != string.Empty)
            {
                Consola.Text = "";
                Gramatica grammar = new Gramatica();
                LanguageData lenguaje = new LanguageData(grammar);
                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(codigo.Text);
                ParseTreeNode raiz = arbol.Root;

                if (arbol.ParserMessages.Count != 0)
                {
                    MessageBox.Show("Se han encontrado errores", "Errores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    List<LogMessage> errores = arbol.ParserMessages;
                    foreach (LogMessage error in errores)
                    {
                        if (error.Message.Contains("Sintax"))
                        {
                            Consola.AppendText("Error Sintactico, " + error.Message + " Linea: " + (error.Location.Line + 1 ) + ", Columna: " + (error.Location.Column + 1));
                        }
                        else
                        {
                            Consola.AppendText("Error Lexico, " + error.Message + " Linea: " + (error.Location.Line + 1) + ", Columna: " + (error.Location.Column + 1));
                        }
                    }
                }
                else
                {
                    Analizador.Analizador ast = new Analizador.Analizador(raiz);
                    ast.generar();
                    if (MessageBox.Show("Desear graficar el AST", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        Graficar.GenerarGrafo grafo = new Graficar.GenerarGrafo();
                        grafo.graficar(Graficar.Dot.getDot(arbol.Root));
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe de escribir el codigo", "Vacio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

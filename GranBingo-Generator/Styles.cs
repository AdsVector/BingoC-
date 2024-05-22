using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GranBingo_Generator
{
    public partial class Styles : Form
    {
        public MatrixEstilo estilo { get; set; }
        private int selectTable = 0;

        public Styles()
        {         
            InitializeComponent();        
        }

        private void Styles_Load(object sender, EventArgs e)
        {
            estilo = new MatrixEstilo();
            MostrarTabla();
        }

        private void PressCheck(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string value = checkBox.Name.Remove(0, 2);
            int idx = int.Parse(value) - 1;

            estilo.SetValue(selectTable, idx, checkBox.Checked);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectTable++;

            if (selectTable > estilo.TablasCount() - 1)
                selectTable = estilo.TablasCount() - 1;

            MostrarTabla();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            selectTable--;

            if (selectTable < 0)
                selectTable = 0;

            MostrarTabla();
        }

        private void MostrarTabla()
        {
            for (int i = 0; i < 25; i++)
            {
                string control_name = "cb" + (i + 1);
                bool value = estilo.GetValue(selectTable, i);
                ((CheckBox)pMatriz.Controls.Find(control_name, false)[0]).Checked = value;
            }

            lblSelect.Text = "Tabla N.- " + (selectTable + 1);
        }

        private void btnFull_Click(object sender, EventArgs e)
        {
            estilo.SetTable(selectTable, true);
            MostrarTabla();
        }

        private void btnEmpty_Click(object sender, EventArgs e)
        {
            estilo.SetTable(selectTable, false);
            MostrarTabla();
        }

        private void btnFullAll_Click(object sender, EventArgs e)
        {
            ActionTableAll(true);
        }

        private void btnEmptyAll_Click(object sender, EventArgs e)
        {
            ActionTableAll(false);
        }

        public void ActionTableAll(bool action)
        {
            for (int i = 0; i < estilo.TablasCount(); i++)
            {
                estilo.SetTable(i, action);
            }

            MostrarTabla();
        }

        /*private void btnSave_Click(object sender, EventArgs e)
       {
           string pathFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//Gran-Bingo//styles//" + txtName.Text + ".txt";

           using (StreamWriter sw = File.CreateText(pathFile))
           {
               for (int i = 0; i < MaxTable; i++)
               {
                   for (int f = 0; f < 5; f++)
                   {
                       for (int c = 0; c < 5; c++)
                       {
                           sw.Write(Matrix.GetValue_Byte(i, f, c));
                       }
                   }
                   sw.WriteLine();
               }
           }

           Properties.Settings.Default.rutaEstilo = pathFile;

           MessageBox.Show("Se ha guardado exitosamente..!");
       }*/

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Busca el archivo para continuar...";
            open.Filter = "Archivo de Texto|*.txt|Todos los Archivos|*.*";
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<bool[]> values = new List<bool[]>();

                    using (StreamReader sr = new StreamReader(open.FileName))
                    {
                        while (!sr.EndOfStream)
                        {
                            string content = sr.ReadLine();
                            bool[] value = LeerLinea(content);
                            values.Add(value);
                        }

                        sr.Close();
                    }

                    if (values.Count == estilo.TablasCount())
                    {
                        estilo.SetTables(values);
                        MostrarTabla();
                    }
                    else
                    {
                        throw new Exception("ERR.D2 (\"Max. de tablas diferentes..!\")");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private bool[] LeerLinea(string linea)
        {
            bool[] binaries = new bool[25];
            char[] bin = linea.ToCharArray();
            bool isValid = bin.All((letter) => (letter.Equals('1') || letter.Equals('0'))) && binaries.Length == 25;

            if (isValid)
            {
                int count = 0;

                for (int i = 0; i < 25; i++)
                {
                        binaries[i] = bin[i] == '1' ? true : false ;
                        count++;
                }

                return binaries;
            }
            else
            {
                throw new Exception("ERR.D1 (\"Este documento tiene un formato invalido..!\")");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Dispose();
        }

 
    }
}

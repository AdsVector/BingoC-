using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GranBingo_Generator
{
    public partial class Form1 : Form
    {
        public const int MAXTABLES = 6;
        List<int[]> tablas = new List<int[]>();
        string pathfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTitle.Select(12, 0);
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string file = pathfolder + "\\archivo.pdf";

            Styles style = new Styles();
            if (style.ShowDialog() == DialogResult.OK)
            {
                string org = "ORGANIZADO POR: " + txtOrg.Text;

                Header encab = new Header(txtTitle.Text, org, txtAdress.Text, Convert.ToDateTime(txtDate.Text),
                    (float)numVal.Value, txtDes.Text);

                ArchivoPdf archivo = new ArchivoPdf(file);

                for (int hoja = 0; hoja < numHojas.Value; hoja++) {
                    archivo.NuevaPagina();
                    archivo.CrearEncabezado(encab);
                    
                    tablas.Clear();

                    for (int tabla = 0; tabla < MAXTABLES; tabla++)
                    {
                        bool[] _estilo = style.estilo.GetTable(tabla);
                        GenerarTabla(_estilo);

                        archivo.CrearTabla(tablas[tabla]);
                    }
                }

                archivo.Close();
            
                Process.Start(file);
                //MessageBox.Show("Hemos terminado..!");
            }
        }       

        public void GenerarTabla(bool[] estilo)
        {            
            Random rand = new Random();
            int[] nums = new int[25];
            int min = 0, max = 0;

            do {

                for (int i = 0; i < 25; i++)
                {
                    int number = 0;

                    if (estilo[i])
                    {
                        if (i == 0 || i == 5 || i == 10 || i == 15 || i == 20)
                        {
                            min = 1; max = 15;
                        }
                        else if (i == 1 || i == 6 || i == 11 || i == 16 || i == 21)
                        {
                            min = 16; max = 30;
                        }
                        else if (i == 2 || i == 7 || i == 12 || i == 17 || i == 22)
                        {
                            min = 31; max = 45;
                        }
                        else if (i == 3 || i == 8 || i == 13 || i == 18 || i == 23)
                        {
                            min = 46; max = 60;
                        }
                        else if (i == 4 || i == 9 || i == 14 || i == 19 || i == 24)
                        {
                            min = 61; max = 75;
                        }

                        do { number = rand.Next(min, max); } while (nums.Contains(number));
                    }

                    nums[i] = number;
                }

            } while(ExisteTabla(nums));

            tablas.Add(nums);
        }

        public bool ExisteTabla(int[] actual)
        {
            int count = 0;

            for (int tabla = 0; tabla < tablas.Count; tabla++)
            {
                int[] selectTable = tablas[tabla];

                for (int num = 0; num < selectTable.Length - 1; num++)
                {
                    if (selectTable[num] == actual[num] && actual[num] > 0)
                        count++;
                }                  
            }

            return count >= 3;
        }      

        private void btnFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = true;
            folder.Description = "Carpeta inicial...";
            //folder.RootFolder = Environment.SpecialFolder.MyDocuments;

            if(folder.ShowDialog() == DialogResult.OK)
            {
                pathfolder = folder.SelectedPath;
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTitle.TextLength < 12)
            {
                txtTitle.Text = "GRAN BINGO \"\"";
                txtTitle.Select(12, 0);
            }
        }

    }
}

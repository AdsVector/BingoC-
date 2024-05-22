using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GranBingo_Generator
{
    public class MatrixEstilo
    {
        private List<bool[]> tablas;

        public MatrixEstilo()
        {
            tablas = new List<bool[]>();

            for (int i = 0; i < 6; i++)
            {
                tablas.Add(FullTrue());
            }
        }

        public int TablasCount()
        {
            return tablas.Count();
        }

        public void SetValue(int table, int index, bool value)
        {
            tablas[table][index] = value;
        }

        public void SetTables(List<bool[]> values)
        {
            tablas = values;
        }

        public void SetTable(int table, bool [] values)
        {
            tablas[table] = values;
        }

        public void SetTable(int table, bool zo)
        {
            bool[] newVal = zo ? FullTrue() : FullFalse();

            tablas[table] = newVal;
        }

        public bool[] GetTable(int table)
        {
            try {
                return tablas[table];              
            } catch {
                return FullTrue();
            }
        }

        public bool GetValue(int table, int index)
        {
            try {
                return tablas[table][index];
            } catch {
                return false;
            }
        }

        private bool[] FullTrue()
        {
            return new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, 
                                true, true, true, true, true, true, true, true, true, true, true, true, true };
        }

        private bool[] FullFalse()
        {
            return new bool[] { false, false, false, false, false, false, false, false, false, false, false, false,
                                false, false, false, false, false, false, false, false, false, false, false, false, false }; ;
        }
    }
}

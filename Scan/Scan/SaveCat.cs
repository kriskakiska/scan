using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan
{
    public partial class SaveCat : Form
    {
        public SaveCat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(saveFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }
    }
}

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
    public partial class ScanUser : Form
    {
        public ScanUser()
        {
            InitializeComponent();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Syst df = new Syst();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveScan df = new SaveScan();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void ScanUser_Load(object sender, EventArgs e)
        {

        }
    }
}

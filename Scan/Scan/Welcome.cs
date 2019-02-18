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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // кнопка "о системе"
        {
            Syst df = new Syst();
            df.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e) // кнопка "начать разгадывание"
        {
            DownloadScan df = new DownloadScan();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e) // кнопка "продолжить разгадывание"
        {
            DownloadScan df = new DownloadScan();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }
    }
}

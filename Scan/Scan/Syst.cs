using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan
{
    public partial class Syst : Form
    {
        public Syst()
        {
            InitializeComponent();
            getSystemInfo(fileSystemInfo);
        }

        public static string fileSystemInfo = @"C:\Kristina\SSAU\7\ТП\ScanLastVersion\Scan\Src\SystemInfo.txt";

        public void getSystemInfo(string fileName)
        {
            string[] systemInfo = File.ReadAllLines(fileName, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i < systemInfo.Length; i++)
            {
                label1.Text += systemInfo[i] + "\n";
                label1.Visible = true;
            }
        }

        private void Syst_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

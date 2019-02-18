using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
//using WMPLib;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan
{
    public partial class AddAudio : Form
      
    {
        /*[DllImport("winmm.dll")]
        internal WindowsMediaPlayer WMP;
        public Form2()
        {
            WMP = new WindowsMediaPlayer();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            WMP.settings.volume = 100;
            WMP.URL = "music.mp3";
            WMP.controls.play();
        }*/
        public AddAudio()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}

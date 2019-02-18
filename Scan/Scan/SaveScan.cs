using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan
{
    public partial class SaveScan : Form
    {
        public static bool userScan = false;
        public SaveScan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // сохранение сканворда в файл
        {
            saveFileDialog1.DefaultExt = ".scan";
            saveFileDialog1.InitialDirectory = @"..\..\NewScan\";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Файл кроссворда (*.scan)|*.scan";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = saveFileDialog1.FileName;
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate))
                    {
                        if (userScan)
                        {
                            formatter.Serialize(fs, DownloadScan.ScanUser);
                            MessageBox.Show("Сканворд сохранен!");
                            userScan = false;
                        } else
                        {
                            formatter.Serialize(fs, ParamScan.NewScan);
                            MessageBox.Show("Сканворд сохранен!");
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка при сохранении сканворда.");
                }
            }
            this.Close();
        }
    }
}

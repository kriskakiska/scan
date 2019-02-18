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
    public partial class DownloadScan : Form
    {
        static List<Task> tasks = new List<Task>();
        static Scan scanUser = new Scan(tasks, 0, 0);        

        public DownloadScan()
        {
            InitializeComponent();
        }

        static internal Scan ScanUser { get => scanUser; set => scanUser = value; }

        private void button2_Click(object sender, EventArgs e) // кнопка "ок" на форме загрузки сканворда
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate))
            {
                scanUser = (Scan)formatter.Deserialize(fs);
            }
            MessageBox.Show("Сканворд загружен");

            int height = scanUser.getHeight();
            int width = scanUser.getWidth();
            ScanUser su = new ScanUser();

            su.dataGridView1.Visible = true;
            su.dataGridView1.ColumnCount = width;
            su.dataGridView1.RowCount = height;


            su.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            su.dataGridView1.AutoResizeColumns();
            su.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            su.dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            for (int i = 0; i < scanUser.getTasks().Count; i++)
            {
                int x = scanUser.getTask(i + 1).getX();
                int y = scanUser.getTask(i + 1).getY();
                su.dataGridView1.Rows[x].Cells[y].Value = scanUser.getTask(i + 1).getID();
                //if (scanUser.getTask(i + 1).getTaskAnswer() != "")
                //{
                //    for (int j = 0; j < scanUser.getTask(i + 1).getTaskAnswer().Length; j++)
                //    {

                //    }
                //}
            }

            this.Hide();
            su.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) // кнопка "обзор" на форме загрузки сканворда
        {
            openFileDialog1.DefaultExt = ".scan";
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Файл сканворда (*.scan)|*.scan";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}

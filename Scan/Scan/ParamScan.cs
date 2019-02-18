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
    [Serializable]
    public partial class ParamScan : Form
    {
        static List<Task> tasks = new List<Task>();
        static Scan newScan = new Scan(tasks, 0, 0);
        public static string wayDict = "";
        public static string wayGallery = "";
        public static string wayCatalog = "";

        public ParamScan()
        {
            InitializeComponent();
        }

        static internal Scan NewScan { get => newScan; set => newScan = value; }

        private void button1_Click(object sender, EventArgs e) // кнопка "обзор" при выборе файла галерии на параметрах сканворда
        {
            openFileDialog1.DefaultExt = ".pic";
            openFileDialog1.InitialDirectory = @"..\..\Gallery\";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Файл галереи (*.pic)|*.pic";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MakeScan.dictGal.Clear();
                MakeScan.listGal.Clear();

                DownloadGal.parseGal(openFileDialog1.FileName);
                MakeScan.listGal.AddRange(MakeScan.dictGal);
                textBox1.Text = openFileDialog1.FileName;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog2.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)//загрузка словаря
        {
            openFileDialog3.DefaultExt = ".dict";
            openFileDialog3.InitialDirectory = @"..\..\Dict\";
            openFileDialog3.AddExtension = true;
            openFileDialog3.FileName = "";
            openFileDialog3.Filter = "Файл словаря (*.dict)|*.dict";

            if (openFileDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MakeScan.dict.Clear();
                MakeScan.list.Clear();

                MakeScan.dgv.Rows.Clear();
                DownloadDict.parseDict(openFileDialog3.FileName);
                MakeScan.list.AddRange(MakeScan.dict);

                foreach (var item in MakeScan.list)
                    MakeScan.dgv.Rows.Add(item.Key, item.Value);

                textBox3.Text = openFileDialog3.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            newScan = new Scan(tasks, Decimal.ToInt32(numericUpDown1.Value), Decimal.ToInt32(numericUpDown2.Value));
            wayDict = textBox3.Text;
            wayGallery = textBox1.Text;
            ParamScan.ActiveForm.Close();

            int height = Convert.ToInt32(numericUpDown1.Value);
            int width = Convert.ToInt32(numericUpDown2.Value);
            MakeScan ms = new MakeScan();

            ms.dataGridView1.Visible = true;
            ms.dataGridView1.ColumnCount = width;
            ms.dataGridView1.RowCount = height;
            

            ms.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ms.dataGridView1.AutoResizeColumns();
            ms.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ms.dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);


            this.Hide();
            ms.ShowDialog();
            this.Show();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

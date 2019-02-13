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
    public partial class DownloadDict : Form
    {
        public DownloadDict()
        {
            InitializeComponent();
        }
        public static string wayDict = "";

        private void button1_Click(object sender, EventArgs e) // кнопка Обзор на форме загрузка словаря
        {
            openFileDialog1.DefaultExt = ".dict";
            openFileDialog1.InitialDirectory = @"..\..\Dict\";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Файл словаря (*.dict)|*.dict";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MakeScan.dict.Clear();
                MakeScan.list.Clear();
                // listNot.Clear();
                // listDef.Clear();
                MakeScan.dgv.Rows.Clear();

                parseDict(openFileDialog1.FileName);
                MakeScan.list.AddRange(MakeScan.dict);

                // listNot = dict.Keys.ToList();
                // listDef = dict.Values.ToList();

                foreach (var item in MakeScan.list)
                    MakeScan.dgv.Rows.Add(item.Key, item.Value);
            
            }
            textBox1.Text = openFileDialog1.FileName;
            MakeScan.dgv.Visible = true;
        }

        public static void parseDict(string filename)
        {
            string[] words = File.ReadAllLines(filename, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i < words.Length - 1; i++)
            {
                string word = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string question = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    MakeScan.dict.Add(word, question);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В словаре имеются одинаковые понятия");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//кнопка Ок на окне загрузки словаря
        {
            wayDict = openFileDialog1.FileName;
            this.Hide();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

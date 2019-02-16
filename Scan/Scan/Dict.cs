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
    public partial class Dict : Form
    {
        public static DataGridView dgv1 = new DataGridView();
        public static Dictionary<string, string> dict1 = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> list1 = new List<KeyValuePair<string, string>>();

        public Dict()
        {
            InitializeComponent();
            initDataGrid();
            downloadDictionary();
        }

        public void initDataGrid() // инициализация таблицы для заполнения словаря
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewColumn col1 = new DataGridViewColumn();
            DataGridViewColumn col2 = new DataGridViewColumn();

            row.CreateCells(dataGridView1);
            col1.CellTemplate = new DataGridViewTextBoxCell();
            col2.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Rows.Add(row);
            dgv1 = dataGridView1;
        }

        private void downloadDictionary()//загрузка словаря при добавлении задания-слова на сканворд
        {
            string wayDict = "";
            wayDict = ParamScan.wayDict;
            parseDict(wayDict);
            //MakeScan.list.AddRange(MakeScan.dict);
            list1.AddRange(dict1);

            foreach (var item in list1)
            Dict.dgv1.Rows.Add(item.Key, item.Value);
            //Dict.dgv1.Visible = true;
        }

        private void parseDict(string filename)
        {
            string[] words = File.ReadAllLines(filename, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i < words.Length - 1; i++)
            {
                string word = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string question = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    //MakeScan.dict.Add(word, question);
                    dict1.Add(word, question);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В словаре имеются одинаковые понятия");
                    return;
                }
            }
        }


        public void dgv1_SelectionChanged(object sender, EventArgs e) // функция добавления задания в выбранную клетку
        {
            if (dgv1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgv1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dgv1.Rows[selectedrowindex];
                int idTask = ParamScan.NewScan.getTasks().Count + 1;
                Task newTask = new Task(idTask, Convert.ToString(selectedRow.Cells[0].Value), Convert.ToString(selectedRow.Cells[1].Value), MakeScan.x, MakeScan.y);
                ParamScan.NewScan.addTask(newTask);
                MakeScan.lengthAnswer = newTask.getAnswer().Length;
            }            
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // кнопка "ок" при выборе слова для добавления в словаре
        {
            dgv1_SelectionChanged(sender,e);
            this.Hide();
            Dict.dgv1.Rows.Clear();
            Dict.dgv1.Refresh();
            Dict.dict1.Clear();
            Dict.list1.Clear();
        }
    }
}

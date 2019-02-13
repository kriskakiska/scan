using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

namespace Scan
{
    public partial class MakeScan : Form
    {
        public static DataGridView dgv = new DataGridView();
        public static Dictionary<string, string> dict = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        public static DataGridViewTextBoxCell selectedCell;
        public static DataGridView dgvScan = new DataGridView();
        public static bool clickGridCell = false;
        public static int x;
        public static int y;
        public static int lengthAnswer;

        public MakeScan()
        {
            InitializeComponent();
            initDataGrid();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
            dgvScan = dataGridView1;
        }
        
        public void initDataGrid() // инициализация таблицы для заполнения словаря
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewColumn col1 = new DataGridViewColumn();
            DataGridViewColumn col2 = new DataGridViewColumn();

            row.CreateCells(dataGridView2);
            col1.CellTemplate = new DataGridViewTextBoxCell();
            col2.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView2.Columns.Add(col1);
            dataGridView2.Columns.Add(col2);
            dataGridView2.Rows.Add(row);
            dgv = dataGridView2;
        }

        public void tabPage1_Click(object sender, EventArgs e)
        {

        }

        public void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ParamScan df = new ParamScan();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DownloadScan df = new DownloadScan();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e) //кнопка сохранения сканворда
        {
            SaveScan df = new SaveScan();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e) // загрузка словаря
        {
            DownloadDict df = new DownloadDict();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e) // кнопка сохранения словаря
        {
            /*SaveDict df = new SaveDict();
            this.Hide();
            df.ShowDialog();
            this.Show();*/

            saveFileDialog1.DefaultExt = ".dict";
            saveFileDialog1.InitialDirectory = @"..\..\Dict\";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.FileName = "Vocabulary";
            saveFileDialog1.Filter = "Файл словаря (*.dict)|*.dict";
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    dict.Clear();
                    try
                    {
                        for (int i = 0; i < dgv.RowCount - 1; i++)
                        {
                            dict.Add(dgv.Rows[i].Cells[0].Value.ToString(), dgv.Rows[i].Cells[1].Value.ToString());
                        }
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Вы не ввели понятие или определение");
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Добавление в словарь одинаковых понятий невозможно");
                        return;
                    }
                    list.Clear();
                    list.AddRange(dict);

                    string s = "";
                    foreach (var item in list)
                    {
                        string def = item.Value;
                        s += item.Key.ToUpper() + " " + def + "\n";
                    }

                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(s);
                    }
                    MessageBox.Show("Словарь успешно создан");
                }
            }
            catch (Exception) { MessageBox.Show("Выйдите из режима редактирования"); }
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // обратчик нажатия ячейки поля
        {
            clickGridCell = true;
            selectedCell = (DataGridViewTextBoxCell) dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            selectedCell.Value = ParamScan.NewScan.getTasks().Count + 1;
            x = selectedCell.RowIndex;
            y = selectedCell.ColumnIndex;
            //dgv1_SelectionChanged(sender, e);
        }

        public static void setDirection(int x, int y, int lengthAnswer)
        {
            if (x == 0)
            {
                if (y == 0)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x].Cells[y+i].Value == null) // проверка свободных клеток вправо
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x].Cells[y + j].Style.BackColor = System.Drawing.Color.Aqua;
                            }
                        }

                        if (dgvScan.Rows[x+1].Cells[y + i].Value == null) // проверка свободных клеток вниз вправо
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x+1].Cells[y + j].Style.BackColor = System.Drawing.Color.Aquamarine;
                            }
                        }

                        if (dgvScan.Rows[x + i].Cells[y + 1].Value == null) // проверка свободных клеток вправо вниз
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x + i].Cells[y + 1].Style.BackColor = System.Drawing.Color.Blue;
                            }
                        }

                        if (dgvScan.Rows[x + i + 1].Cells[y].Value == null) // проверка свободных клеток вниз
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.BlueViolet;
                            }
                        }
                    }
                }
            }
        }

        /*private void toolStripButton11_Click(object sender, EventArgs e)
        {
            AddWord df = new AddWord();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }*/

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            DownloadGal df = new DownloadGal();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            SaveGal df = new SaveGal();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            AddPicture df = new AddPicture();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (clickGridCell)
            {
                AddTask df = new AddTask();
                //this.Hide();
                df.ShowDialog();
                this.Show();
                clickGridCell = false;
            } else
            {
                MessageBox.Show("Нажмите на ячейку, в которую хотели бы поместить задание");
            }
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            DownloadCat df = new DownloadCat();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            SaveCat df = new SaveCat();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            AddAudio df = new AddAudio();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Syst df = new Syst();
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e) // кнопка создания нового словаря
        {
            dgv.Visible = true;
        }

        private void MakeScan_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {
        
          }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri("C:\\Users\\shark\\Desktop\\Scan\\Music\\song 1.mp3", UriKind.Absolute));
            player.Play();
        }

        private void label3_Click(object sender, EventArgs e) // направление вправо
        {
            if (y + lengthAnswer <= dgvScan.ColumnCount - 1)
            {
                if (x == 0)
                {
                    if (y == 0)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x].Cells[y + i].Value == null) // направление вправо
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x].Cells[y + j].Style.BackColor = System.Drawing.Color.Aqua;
                                }
                            }
                        }
                    } else if (y != 0 && y != dgvScan.ColumnCount)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x].Cells[y + i].Value == null) // направление вправо
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x].Cells[y + j].Style.BackColor = System.Drawing.Color.Aqua;
                                }
                            }
                        }
                    }
                } else
                {
                    if (y == 0)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x].Cells[y + i].Value == null) // направление вправо
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x].Cells[y + j].Style.BackColor = System.Drawing.Color.Aqua;
                                }
                            }
                        }
                    }
                    else if (y != 0 && y != dgvScan.ColumnCount)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x].Cells[y + i].Value == null) // направление вправо
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x].Cells[y + j].Style.BackColor = System.Drawing.Color.Aqua;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
                
        }

        private void label4_Click(object sender, EventArgs e) // направление вниз вправо
        {
            if (x == 0)
            {
                if (y == 0)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {

                        if (dgvScan.Rows[x + 1].Cells[y + i].Value == null) // проверка свободных клеток вниз вправо
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x + 1].Cells[y + j].Style.BackColor = System.Drawing.Color.Aquamarine;
                            }
                        }
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e) // направление вправо вниз
        {
            if (x == 0)
            {
                if (y == 0)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x + i].Cells[y + 1].Value == null) // проверка свободных клеток вправо вниз
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x + i].Cells[y + 1].Style.BackColor = System.Drawing.Color.Blue;
                            }
                        }
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e) // направление вниз
        {
            if (x + lengthAnswer <= dgvScan.RowCount - 1)
            {
                ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(6);
                if (x == 0)
                {
                    if (y == 0)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x + i + 1].Cells[y].Value == null) // проверка свободных клеток вниз
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.BlueViolet;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x + i + 1].Cells[y].Value == null) // проверка свободных клеток вниз
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.BlueViolet;
                                }
                            }
                        }
                    }
                }
                else if (x != 0 && x != dgvScan.RowCount)
                {
                    if (y == 0)
                    {

                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x + i + 1].Cells[y].Value == null) // проверка свободных клеток вниз
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.BlueViolet;
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            if (dgvScan.Rows[x + i + 1].Cells[y].Value == null) // проверка свободных клеток вниз
                            {
                                for (int j = 0; j < lengthAnswer + 1; j++)
                                {
                                    dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.BlueViolet;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }            
        }

        private void label7_Click(object sender, EventArgs e) // направление вверх вправо
        {
            if (x != 0)
            {
                if (y == 0)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x - 1].Cells[y + i].Value == null) // проверка свободных вверх вправо
                        {
                            for (int j = 0; j < lengthAnswer + 1; j++)
                            {
                                dgvScan.Rows[x - 1].Cells[y + i].Style.BackColor = System.Drawing.Color.BlueViolet;
                            }
                        }
                    }
                }
            } 
        }

        private void label8_Click(object sender, EventArgs e) // направление влево вниз
        {

        }

        private void Ок_Click(object sender, EventArgs e) 
        {
            Task addedTask = ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value));
            if (addedTask.getDirection() == 6) // если напрваление вниз 
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x + i + 1].Cells[y].Value = addedTask.getAnswer()[i];
                }
            }
        }
    }
}

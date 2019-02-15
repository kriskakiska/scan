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
        public static int freeCellsCount = 0;

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

        private void clearColorGrid() // функция очищения подсветки таблицы
        {
            for (int i = 0; i < dgvScan.RowCount; i++)
            {
                for (int j = 0; j < dgvScan.ColumnCount; j++)
                {
                    dgvScan.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void hideDirectionTools() // функция скрытия инструментов добавления направления
        {

        }

        private void label3_Click(object sender, EventArgs e) // направление вправо
        {
            clearColorGrid();
            ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(3);

            if (y != dgvScan.ColumnCount - 1)
            {
                if (y + lengthAnswer + 1 <= dgvScan.ColumnCount)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x].Cells[y + i + 1].Value == null) // проверка нужного количества свободных клеток вниз вправо
                        {
                            freeCellsCount++;
                        }
                    }
                    if (freeCellsCount == lengthAnswer)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x].Cells[y + i + 1].Style.BackColor = System.Drawing.Color.BlueViolet;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
            freeCellsCount = 0;
        }

        private void label4_Click(object sender, EventArgs e) // направление вниз вправо
        {
            clearColorGrid();
            ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(4);

            if (x != dgvScan.RowCount - 1)
            {
                if (y + lengthAnswer <= dgvScan.ColumnCount)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x + 1].Cells[y + i].Value == null) // проверка нужного количества свободных клеток вниз вправо
                        {
                            freeCellsCount++;
                        }
                    }
                    if (freeCellsCount == lengthAnswer)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + 1].Cells[y + i].Style.BackColor = System.Drawing.Color.BlueViolet;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
            freeCellsCount = 0;
        }

        private void label5_Click(object sender, EventArgs e) // направление вправо вниз
        {
            clearColorGrid();
            ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(5);

            if (y != dgvScan.ColumnCount - 1)
            {
                if (x + lengthAnswer <= dgvScan.RowCount)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x + i].Cells[y + 1].Value == null) // проверка нужного количетсва свободных клеток вправо вниз
                        {
                            freeCellsCount++;
                        }
                    }
                    if (freeCellsCount == lengthAnswer)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + i].Cells[y + 1].Style.BackColor = System.Drawing.Color.Blue;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
            freeCellsCount = 0;
        }

        private void label6_Click(object sender, EventArgs e) // направление вниз
        {
            clearColorGrid();
            ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(6);

            if (x != dgvScan.RowCount - 1)
            {
                if (x + lengthAnswer <= dgvScan.RowCount)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x + i + 1].Cells[y].Value == null) // проверка нужного количества свободных клеток вниз
                        {
                            freeCellsCount++;
                        }
                    }

                    if (freeCellsCount == lengthAnswer)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.BlueViolet;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
            freeCellsCount = 0;
        }

        private void label7_Click(object sender, EventArgs e) // направление вверх вправо
        {
            clearColorGrid();
            ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(7);

            if (x != 0)
            {
                if (y + lengthAnswer <= dgvScan.ColumnCount)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x - 1].Cells[y + i].Value == null) // проверка нужного количества свободных клеток вверх вправо
                        {
                            freeCellsCount++;
                        }
                    }
                    if (freeCellsCount == lengthAnswer)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x - 1].Cells[y + i].Style.BackColor = System.Drawing.Color.BlueViolet;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
            freeCellsCount = 0;
        }

        private void label8_Click(object sender, EventArgs e) // направление влево вниз
        {
            clearColorGrid();
            ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value)).setDirection(8);

            if (y != 0)
            {
                if (x + lengthAnswer <= dgvScan.RowCount)
                {
                    for (int i = 0; i < lengthAnswer; i++)
                    {
                        if (dgvScan.Rows[x + i].Cells[y - 1].Value == null) // проверка нужного количетсва свободных клеток влево вниз
                        {
                            freeCellsCount++;
                        }
                    }
                    if (freeCellsCount == lengthAnswer)
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + i].Cells[y - 1].Style.BackColor = System.Drawing.Color.Blue;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
            }
            freeCellsCount = 0;
        }

        private void Ок_Click(object sender, EventArgs e) 
        {
            clearColorGrid();
            Task addedTask = ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value));

            if (addedTask.getDirection() == 3) // если напрваление вправо
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x].Cells[y + i + 1].Value = addedTask.getAnswer()[i];
                }
            }

            if (addedTask.getDirection() == 4) // если напрваление вниз вправо
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x + 1].Cells[y + i].Value = addedTask.getAnswer()[i];
                }
            }

            if (addedTask.getDirection() == 5) // если напрваление вправо вниз
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x + i].Cells[y + 1].Value = addedTask.getAnswer()[i];
                }
            }

            if (addedTask.getDirection() == 6) // если напрваление вниз 
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x + i + 1].Cells[y].Value = addedTask.getAnswer()[i];
                }
            }

            if (addedTask.getDirection() == 7) // если напрваление вверх право
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x - 1].Cells[y + i].Value = addedTask.getAnswer()[i];
                }
            }

            if (addedTask.getDirection() == 8) // если напрваление влево вниз 
            {
                for (int i = 0; i < lengthAnswer; i++)
                {
                    dgvScan.Rows[x + i].Cells[y - 1].Value = addedTask.getAnswer()[i];
                }
            }
        }
    }
}

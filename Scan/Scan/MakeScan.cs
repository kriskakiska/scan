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
        public static Dictionary<string, string> dictGal = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> listGal = new List<KeyValuePair<string, string>>();
        public static DataGridViewTextBoxCell selectedCell;
        public static DataGridView dgvScan = new DataGridView();
        public static bool clickGridCell = false;
        public static bool selectCell = false;
        public static int x;
        public static int y;
        public static int lengthAnswer;
        public static int freeCellsCount = 0;
<<<<<<< HEAD
        public static bool selectedTaskToDelete = false;

=======
>>>>>>> 17773ac769963ecb650768ba1533009eccdfa254
        public MakeScan()
        {
            InitializeComponent();
            //MakeScan msk = new MakeScan();
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
            df.ShowDialog();
            this.Close();
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
            bool result = true;
            for (int i = 0; i < dgvScan.RowCount; i++)
            {
                for (int j = 0; j < dgvScan.ColumnCount; j++)
                {
                    if (dgvScan.Rows[i].Cells[j].Value == null)
                    {
                        result = false;
                    }
                }
            }
            if (result == false)
            {
                MessageBox.Show("Поле сканворда не должно содержать пустых ячеек. Пожалуйста, внесите исправления.");
            }
            else
            {
                SaveScan df = new SaveScan();
                this.Hide();
                df.ShowDialog();
                this.Show();
            }
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
            if (selectedTaskToDelete == true)
            {
                selectedCell = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (selectedCell.Value != null && selectedCell.Value.GetType() == typeof(int))
                {
                    Task deletedTask = ParamScan.NewScan.getTask(Convert.ToInt32(selectedCell.Value));

                    if (deletedTask.getDirection() == 0)
                    {
                        selectedCell.Value = null;
                    }

                    if (deletedTask.getDirection() == 3) // если напрваление вправо
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x].Cells[y + i + 1].Value = null;
                        }
                    }

                    if (deletedTask.getDirection() == 4) // если напрваление вниз вправо
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + 1].Cells[y + i].Value = null;
                        }
                    }

                    if (deletedTask.getDirection() == 5) // если напрваление вправо вниз
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + i].Cells[y + 1].Value = null;
                        }
                    }

                    if (deletedTask.getDirection() == 6) // если напрваление вниз 
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + i + 1].Cells[y].Value = null;
                        }
                    }

                    if (deletedTask.getDirection() == 7) // если напрваление вверх право
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x - 1].Cells[y + i].Value = null;
                        }
                    }

                    if (deletedTask.getDirection() == 8) // если напрваление влево вниз 
                    {
                        for (int i = 0; i < lengthAnswer; i++)
                        {
                            dgvScan.Rows[x + i].Cells[y - 1].Value = null;
                        }
                    }
                    clearColorGrid();
                    ParamScan.NewScan.deleteTask(Convert.ToInt32(selectedCell.Value));                    
                    selectedCell.Value = null;
                    selectedTaskToDelete = false;
                }
                else
                {
                    MessageBox.Show("Нажмите на ячейку c номером задания, которое вы хотели бы удалить.");
                }

            }
            else
            {
                clickGridCell = true;
                selectedCell = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                for (int i = 0; i < dgvScan.RowCount; i++)
                {
                    for (int j = 0; j < dgvScan.ColumnCount; j++)
                    {
                        if (ParamScan.NewScan.getTasks().Count != 0 && dgvScan.Rows[i].Cells[j].Value != null && dgvScan.Rows[i].Cells[j].Value.GetType() == typeof(int) && ParamScan.NewScan.getTasks().Count < Convert.ToInt32(dgvScan.Rows[i].Cells[j].Value))
                        {
                            dgvScan.Rows[i].Cells[j].Value = null;
                        }
                    }
                }

                if (selectedCell.Value == null)
                {
                    selectedCell.Value = ParamScan.NewScan.getTasks().Count + 1;
                    selectedCell.Style.ForeColor = System.Drawing.Color.Black;
                    x = selectedCell.RowIndex;
                    y = selectedCell.ColumnIndex;
                }
                else
                {
                    MessageBox.Show("Выбранная ячейка занята");
                }
            }
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

        private void toolStripButton17_Click(object sender, EventArgs e)//сохранения галереи
        {
            saveFileDialog2.DefaultExt = ".pic";
            saveFileDialog2.InitialDirectory = @"..\..\Gallery\";
            saveFileDialog2.AddExtension = true;
            saveFileDialog2.FileName = "Gallery";
            saveFileDialog2.Filter = "Файл галереи (*.pic)|*.pic";
            try
            {
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    string s = "";
                    foreach (var item in AddPicture.listGal2)
                    {
                        string def = item.Value;
                        s += item.Key.ToUpper() + " " + def + "\n";
                    }

                    using (StreamWriter sw = new StreamWriter(saveFileDialog2.FileName, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(s);
                    }
                    MessageBox.Show("Галерея успешно создана");
                }
            }
            catch (Exception) { MessageBox.Show("Выйдите из режима редактирования"); }
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

        private void toolStripButton5_Click(object sender, EventArgs e) // значок добавления задания на поле
        {
            showDirectionTools();
            if (clickGridCell)
            {
                AddTask df = new AddTask();
                df.ShowDialog();
                this.Show();
                clickGridCell = false;
            } else
            {
                MessageBox.Show("Нажмите на ячейку, в которую хотели бы поместить задание.");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e) // значок удаления задание с поля
        {
            int result = 0;
            for (int i = 0; i < dgvScan.RowCount; i++)
            {
                for (int j = 0; j < dgvScan.ColumnCount; j++)
                {
                    if (dgvScan.Rows[i].Cells[j].Value == null)
                    {
                        result++;
                    }
                }
            }
            if (result == dgvScan.RowCount * dgvScan.ColumnCount)
            {
                MessageBox.Show("Поле не содержит ячеек доступных для удаления.");
            }
            else
            {
                selectedTaskToDelete = true;
                MessageBox.Show("Нажмите на ячейку с номером задания, которое вы хотели бы удалить.");
            }

            //if (clickGridCell)
            //{
            //    ParamScan.NewScan.deleteTask(Convert.ToInt32(selectedCell.Value));
            //    selectedCell.Value = null;
            //}
            //else
            //{
            //    MessageBox.Show("Нажмите на ячейку с номером задания, которое вы хотели бы удалить");
            //}
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
                    if (dgvScan.Rows[i].Cells[j].Style.BackColor != System.Drawing.Color.White && dgvScan.Rows[i].Cells[j].Value == null)
                    {
                        dgvScan.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }

        private void showDirectionTools() // функция показа инструментов добавления направления
        {
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            Ок.Visible = true;
            button1.Visible = true;
        }

        private void hideDirectionTools() // функция скрытия инструментов добавления направления
        {
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            Ок.Visible = false;
            button1.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e) // направление вправо
        {
            Ок.Enabled = true;
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
                            dgvScan.Rows[x].Cells[y + i + 1].Style.BackColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                        Ок.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                    Ок.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
                Ок.Enabled = false;
            }
            freeCellsCount = 0;
        }

        private void label4_Click(object sender, EventArgs e) // направление вниз вправо
        {
            Ок.Enabled = true;
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
                            dgvScan.Rows[x + 1].Cells[y + i].Style.BackColor = System.Drawing.Color.Gold;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                        Ок.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                    Ок.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
                Ок.Enabled = false;
            }
            freeCellsCount = 0;
        }

        private void label5_Click(object sender, EventArgs e) // направление вправо вниз
        {
            Ок.Enabled = true;
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
                            dgvScan.Rows[x + i].Cells[y + 1].Style.BackColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                        Ок.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                    Ок.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
                Ок.Enabled = false;
            }
            freeCellsCount = 0;
        }

        private void label6_Click(object sender, EventArgs e) // направление вниз
        {
            Ок.Enabled = true;
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
                            dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.Coral;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данное направление недоступно для заданного слова");
                        Ок.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                    Ок.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
                Ок.Enabled = false;
            }
            freeCellsCount = 0;
        }

        private void label7_Click(object sender, EventArgs e) // направление вверх вправо
        {
            Ок.Enabled = true;
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
                        Ок.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                    Ок.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
                Ок.Enabled = false;
            }
            freeCellsCount = 0;
        }

        private void label8_Click(object sender, EventArgs e) // направление влево вниз
        {
            Ок.Enabled = true;
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
                        Ок.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Данное направление недоступно для заданного слова");
                    Ок.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Данное направление недоступно для заданного слова");
                Ок.Enabled = false;
            }
            freeCellsCount = 0;
        }

        private void Ок_Click(object sender, EventArgs e) 
        {
            selectCell = true;
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
            hideDirectionTools();
            Ок.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) // кнопка отметы добавления задания на поле
        {
            selectCell = true;
            clearColorGrid();
            hideDirectionTools();
            ParamScan.NewScan.deleteTask(Convert.ToInt32(selectedCell.Value));
            selectedCell.Value = null;
            Ок.Enabled = false;
        }
<<<<<<< HEAD

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            listView1.Visible = true;
        }
=======
>>>>>>> 6b8cb6deb0c6f2049c3437ff118b50e95bba5405
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.IO;

namespace arduino
{
    public partial class Form1 : Form
    {
        DataFromArduino dataFromArduino = new DataFromArduino();
        string[,] pole;
      
        public Form1()
        {
            InitializeComponent();
            dataFromArduino.FindPort();
            f1 = this;
        }
 
        public static Form1 f1
        {
            get;
            set;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataFromArduino.FindPort();
        }

        private double f(double x)
        {
            if (x == 0)
            {
                return 1;
            }

            return Math.Sin(x) / x;
        }

        
        private void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();

            double xmin = -50;
            double xmax = 50;

            // Заполняем список точек
            for (double x = xmin; x <= xmax; x += 0.01)
            {
                // добавим в список точку
                list.Add(x, f(x));
            }

            // Создадим кривую с названием "Sinc",
            // которая будет рисоваться голубым цветом (Color.Blue),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem myCurve = pane.AddCurve("Sinc", list, Color.Blue, SymbolType.None);

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nastr n = new Nastr();
            n.Show();
        }
        string[,] mas = new string[181, 2];
        private void hui()
        {
            try
            {           
                StreamReader sr1 = new StreamReader("D://arduino.txt");

                string c = null;              

                while (c != "180")
                {
                    string[] s = sr1.ReadLine().Split(';');
                    c = s[0];
                    mas[int.Parse(c), 0] = s[0];
                    mas[int.Parse(c), 1] = s[1];                            
                }
                sr1.Close();
            }
            catch (Exception)
            {
                hui();
            }
        }
        int k = 0;
        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hui();

            DrawGraph();
            DataGridViewTextBoxColumn kolonka;
            kolonka = new DataGridViewTextBoxColumn();

            kolonka.Name = "dgvAge";
            kolonka.HeaderText = "Высота";
            kolonka.Width = 100;

            dataGridView1.Columns.Add(kolonka);

     
            for (int i = 0; i < 181; i++)
            {
                dataGridView1.Rows.Add(mas[i,0]);
                dataGridView1.Rows[i].HeaderCell.Value = mas[i, 0];
                dataGridView1.Rows[i].Cells[k].Value= mas[i, 1];
            }
            k++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataFromArduino.Debag1();
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataFromArduino.Debag2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = dataFromArduino.Done();
        }



        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    inputFromKeybord.GettingFromKeybord(e.KeyData.ToString(),this);
        //    MessageBox.Show("хуй");
        //}



    }
}
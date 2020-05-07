using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace arduino
{
    public partial class Nastr : Form
    {
        public Nastr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nazv = textBox1.Text;

            Nastr n = this;           
            Form1.f1.ll(nazv);

            StreamReader sr1 = new StreamReader("D://arduino.txt");

            string c = null;
            string[,] mas = new string[181, 2];

            while (c != "180")
            {
                string[] s = sr1.ReadLine().Split(';');
                c = s[0];
                mas[int.Parse(c), 0] = s[0];
                mas[int.Parse(c), 1] = s[1];

                string str = mas[int.Parse(c), 0] + mas[int.Parse(c), 1];
                listBox1.Items.Add(str);

                label3.Text = ((double)int.Parse(c) / 180 * 100).ToString();
            }
            sr1.Close();

            //string str1 = " ";
            //while (!sr1.EndOfStream) str1 += sr1.ReadLine();
            //sr1.Close();

            //string[] s = str1.Split(';');
            //label3.Text = s[1];



            // n.Close();
        }
    }
}

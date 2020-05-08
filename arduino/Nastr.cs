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

        private void hui()
        {
            try
            {
                string nazv = textBox1.Text;

                Nastr n = this;
              //  Form1.f1.ll(nazv);

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
                  
                }
                sr1.Close();

                


                // n.Close();
            }
            catch (Exception)
            {
                hui();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hui();        
        }
    }
}

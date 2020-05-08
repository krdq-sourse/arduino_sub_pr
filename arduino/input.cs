using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arduino
{
    class InputFromKeybord
    {
        private  string[,] arr = new string[361, 2];
        private string temp = "";
        private int last = 0;
        private int counter = 0;
        public void GettingFromKeybord(string e, Form1 f)
        {
            last++;
            temp += Convert(e);
            if (last == 1444)
            {
                f.InputToThePole();
            }
        }
        private string Convert(string e)
        {
            switch (e) {
                case "D1": return "1"; 
                case "D2": return "2";
                case "D3": return "3";
                case "D4": return "4";
                case "D5": return "5";
                case "D6": return "6";
                case "D7": return "7";
                case "D8": return "8";
                case "D9": return "9";
                case "D0": return "0";
                case "P": return ";";
                case "H": return ":";
                default: return "hui";
            }
        }
        public string[,] ReturnArray()
        {
            string[] str = temp.Split(':');
            foreach (string s in str) {
                string[] tmp = s.Split(';');
                arr[counter,0] = tmp[0];
                arr[counter++,1] = tmp[1];
            }
            return arr;
        }
    }
}

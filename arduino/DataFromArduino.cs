using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows;

namespace arduino
{
    class DataFromArduino
    {
        // Dispatcher dispatcherUI = Dispatcher.CurrentDispatcher;
        private string val = ""; 
        System.Timers.Timer aTimer;
        SerialPort currentPort;
        private delegate void updateDelegate(string txt);
        public void FindPort()
        {
            bool ArduinoPortFound = false;

            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    currentPort = new SerialPort(port, 9600);
                    if (ArduinoDetected())
                    {
                        ArduinoPortFound = true;
                        break;
                    }
                    else
                    {
                        ArduinoPortFound = false;
                    }
                }
            }
            catch { }

            if (ArduinoPortFound == false) return;
            System.Threading.Thread.Sleep(500); // немного подождем

            currentPort.BaudRate = 9600;
            currentPort.DtrEnable = true;
            currentPort.ReadTimeout = 1000;
            try
            {
                currentPort.Open();
            }
            catch { }

            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private bool ArduinoDetected()
        {
            try
            {
                currentPort.Open();
                System.Threading.Thread.Sleep(1000);
                // небольшая пауза, ведь SerialPort не терпит суеты

                string returnMessage = currentPort.ReadLine();
                currentPort.Close();

                // необходимо чтобы void loop() в скетче содержал код Serial.println("Info from Arduino");
                if (returnMessage.Contains("Info from Arduino"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (!currentPort.IsOpen) return;
            try // так как после закрытия окна таймер еще может выполнится или предел ожидания может быть превышен
            {
                // удалим накопившееся в буфере
                currentPort.DiscardInBuffer();
                // считаем последнее значение 
                string strFromPort = currentPort.ReadLine();
                // lblPortData.Dispatcher.BeginInvoke(new updateDelegate(updateTextBox), strFromPort);
                updateTextBox(strFromPort);
            }
            catch { }
        }

        private void updateTextBox(string txt)
        {
            val += txt;
        }
        private void Window_Closing(object sender, EventArgs e)
        {
            aTimer.Enabled = false;
            currentPort.Close();
        }
        public string Done()
        {
            return val;
        }
        public void Debag1() {
            if (!currentPort.IsOpen) return;
            currentPort.Write("1");
        }
        public void Debag2()
        {
            if (!currentPort.IsOpen) return;
            currentPort.Write("1");
        }
    }
}

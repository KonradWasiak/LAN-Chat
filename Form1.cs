using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;


namespace KLIENT2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string port = "1410";
            backgroundWorker1.RunWorkerAsync(port);
        }

   

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            string str = (string)e.Argument;
            int port = 1410;
            port = int.Parse(str);
            TcpListener Sluchacz = new TcpListener(port);
            Sluchacz.Start();
            backgroundWorker1.ReportProgress(1, "Listener start: " + Environment.NewLine);
            backgroundWorker1.ReportProgress(2, "START");

            while (true)
            {
                Socket client = Sluchacz.AcceptSocket();
                byte[] buf = new byte[100];
                int size = client.Receive(buf);
                string strMsg = "";
                for (int i = 0; i < size; i++)
                    strMsg = strMsg + Convert.ToChar(buf[i]);

                backgroundWorker1.ReportProgress(3, "ON: " + strMsg + Environment.NewLine);

                client.Close();
            }
        }
       

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string str = (string)e.UserState;
            textBox2.Text += str;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string adres = textBox3.Text;
            int port = 1410;
            string wiadomosc = textBox1.Text;
            if (textBox1.Text == "")
            {
                MessageBox.Show("WIADOMOŚĆ NIE MOŻE BYĆ PUSTA!");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("PODAJ ADRES IP!");
            }
            else
            {
                try
                {
                    TcpClient wysylka = new TcpClient(adres, port);
                    Byte[] dane = System.Text.Encoding.ASCII.GetBytes(wiadomosc);
                    NetworkStream strumien = wysylka.GetStream();
                    strumien.Write(dane, 0, dane.Length);
                    backgroundWorker1.ReportProgress(1, "Ty: " + wiadomosc + Environment.NewLine);
                }
                catch
                {
                    MessageBox.Show("Podany adres IP nie odpowiada!");
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

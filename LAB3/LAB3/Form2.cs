using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LAB3
{
    public partial class Form2 : Form
    {
        private delegate void InfoMessageDel(string info);
        public Form2()
        {
            InitializeComponent();
        }

        private void listen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = true;
            Thread serverThread = new Thread(new ThreadStart(StartThread));
            serverThread.Start();
        }
        public void InfoMessage(string info)
        {
            if (listView.InvokeRequired)
            {
                InfoMessageDel method = new InfoMessageDel(InfoMessage);
                listView.Invoke(method, new object[] { info });
                return;
            }
            listView.Items.Add(info);
        }

        private void StartThread()
        {
            InfoMessage("Waiting for connetion");
            int bytesRecv = 0;
            byte[] recv = new byte[1024];
            Socket clientSocket;

            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepSV = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            listenerSocket.Bind(ipepSV);
            listenerSocket.Listen(-1);
            clientSocket = listenerSocket.Accept();

            InfoMessage("New client conneted");

            while (clientSocket.Connected)
            {
                string text = "";
                do
                {
                    bytesRecv = clientSocket.Receive(recv);
                    text += Encoding.UTF8.GetString(recv);
                }
                while (text[text.Length - 1] != '\n');
                InfoMessage("You said: " + text);
            }
            listenerSocket.Close();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

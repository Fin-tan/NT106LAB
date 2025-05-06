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

namespace LAB3
{
    public partial class bai3_TCP_server : Form
    {
        private delegate void InfoMessageDel(string info);
        private Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public bai3_TCP_server()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            listen = new Button();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // listen
            // 
            listen.Location = new Point(331, 39);
            listen.Name = "listen";
            listen.Size = new Size(75, 23);
            listen.TabIndex = 0;
            listen.Text = "listen";
            listen.UseVisualStyleBackColor = true;
            listen.Click += listen_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(29, 105);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(377, 199);
            listBox1.TabIndex = 1;
            // 
            // bai3_TCP_server
            // 
            ClientSize = new Size(492, 335);
            Controls.Add(listBox1);
            Controls.Add(listen);
            Name = "bai3_TCP_server";
            Load += bai3_TCP_server_Load;
            ResumeLayout(false);

        }
        public void InfoMessage(string info)
        {
            if (listBox1.InvokeRequired)
            {
                InfoMessageDel method = new InfoMessageDel(InfoMessage);
                listBox1.Invoke(method, new object[] { info });
                return;
            }
            listBox1.Items.Add(info);
        }

        private void listen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = true;
            listen.Enabled = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.Start();
        }
        private void StartUnsafeThread()
        {
            try
            {
                InfoMessage("Server running on 127.0.0.1:8080");
                int bytesReceived = 0;
                byte[] recv = new byte[1];
                Socket clientSocket;
                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
                //Gan socket lang nghe toi dia chi IP cua may va port 8080
                listenerSocket.Bind(ipepServer);
                //bat dau lang nghe.Socket.Listening(int backlog) voi backlog la do dai toi da cua hang doi cac ket noi dang cho xu ly
                listenerSocket.Listen(-1);
                //Dong y ket noi
                clientSocket = listenerSocket.Accept();
                //Nhan du lieu
                InfoMessage("New client connected!");
                while (clientSocket.Connected)
                {
                    string text = "";
                    do
                    {
                        bytesReceived = clientSocket.Receive(recv);
                        text += Encoding.ASCII.GetString(recv);
                    }
                    while (text[text.Length - 1] != '\n');
                    InfoMessage(text);
                }
                listenerSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bai3_TCP_server_Load(object sender, EventArgs e)
        {

        }
        private void bai3_TCP_server_Formclosing(object sender, EventArgs e)
        {
            listenerSocket.Close();
        }
    }
}

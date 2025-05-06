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
    public partial class bai4_chat_server : Form
    {
        private delegate void InfoMessageDel(string info);
        private byte[] recv;
        private int bytesReceived = 0;
        private List<Socket> ListClient;
        private Socket listenerSocket;
        private IPEndPoint ipepServer;

        public bai4_chat_server()
        {
            InitializeComponent();
        }
        public void InfoMessage(string info)
        {
            if (list.InvokeRequired)
            {
                InfoMessageDel method = new InfoMessageDel(InfoMessage);
                list.Invoke(method, new object[] { info });
                return;
            }
            list.Items.Add(info);
        }
        private void listen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = true;
            Thread StartListenThread = new Thread(ListenThread);
            if (!StartListenThread.IsAlive)
            {
                StartListenThread.Start();
            }

            listen.Text = "Listenning...";
            if (list.Items.Count != 0)
            {
                list.Items.Clear();
                list.Clear();
            }
        }
        private void ListenThread()
        {
            bytesReceived = 0;
            recv = new byte[1024];
            listenerSocket = new Socket(
                 AddressFamily.InterNetwork,
                 SocketType.Stream,
                 ProtocolType.Tcp
                 );
            ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            listenerSocket.Bind(ipepServer);
            InfoMessage("Listenning on: " + ipepServer.ToString());
            listenerSocket.Listen(-1);
            AcceptClient();
        }

        private void AcceptClient()
        {
            try
            {
                ListClient = new List<Socket>();
                while (true)
                {
                    Socket clientSocket = listenerSocket.Accept();
                    ListClient.Add(clientSocket);
                    SendData("Hi, Welcome to My Room Chat!", clientSocket);
                    InfoMessage("New Client Connected: " + clientSocket.RemoteEndPoint.ToString());
                    Thread receiver = new Thread(() => ReceiveDataThread(clientSocket));
                    receiver.Start();
                }
            }
            catch (Exception)
            {
                CloseMe();
            }
        }
        private void ReceiveDataThread(Socket clientSocket)
        {
            try
            {
                while (true && clientSocket.Connected && listenerSocket.LocalEndPoint != null)
                {
                    string msg = "";
                    bytesReceived = clientSocket.Receive(recv);
                    msg = Encoding.UTF8.GetString(recv, 0, bytesReceived);
                    string listViewString = clientSocket.RemoteEndPoint.ToString() + ": " + msg;

                    InfoMessage(listViewString);
                    broadcast(msg);
                    if (msg.Contains("quit"))
                    {
                        CloseClientConnection(clientSocket);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đóng kết nối!");
                this.Close();
            }
        }

        private void CloseClientConnection(Socket clientSocket)
        {
            clientSocket.Close();
            foreach (var item in ListClient.ToArray())
            {
                if (item == clientSocket)
                {
                    ListClient.RemoveAt(ListClient.IndexOf(item));
                }
            }
        }

        private void SendData(string msg, Socket client)
        {
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
            client.Send(data);
        }

        private void broadcast(string msg)
        {
            foreach (var item in ListClient)
            {
                SendData(msg, item);
            }
        }

        private void CloseMe()
        {
            StopListening();

            foreach (var item in ListClient.ToArray())
            {
                CloseClientConnection(item);
            }

            ListClient.Clear();

            recv = null;
            bytesReceived = 0;
            ipepServer = null;
        }

        private void StopListening()
        {
            if (listenerSocket != null)
            {
                broadcast("server quit");

                if (list.SelectedItems.Count == 0 && list.Items.Count != 0)
                {
                    list.Items.Clear();
                }

                listenerSocket.Close();
            }
        }

        private void bai4_chat_server_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseMe();
        }

        private void bai4_chat_server_Load(object sender, EventArgs e)
        {
            Thread ControlListenBtn = new Thread(ControlListenButton);
            ControlListenBtn.Start();
        }

        private void ControlListenButton()
        {
            while (true)
            {
                if (listen.InvokeRequired)
                {
                    listen.Invoke(new Action(() =>
                    {
                        if (listen.Text == "Listenning...")
                        {
                            listen.Enabled = false;
                            stop.Visible = true;
                        }
                        else stop.Visible = false;
                    }));
                }
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            StopListening();
            listen.Text = "Listen";
            listen.Enabled = true;
        }
    }
}

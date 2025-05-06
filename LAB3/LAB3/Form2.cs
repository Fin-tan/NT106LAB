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
using System.Security.Cryptography;

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
            listenerSocket.Listen(10);
           

            InfoMessage("New client conneted");
            while (true)
            {
                clientSocket = listenerSocket.Accept();
                while (clientSocket.Connected)
                {
                    try
                    {
                        string text = "";
                        recv = new byte[1024];
                        bytesRecv = clientSocket.Receive(recv);
                        if (bytesRecv <= 0) break;
                        text = Encoding.UTF8.GetString(recv);
                        InfoMessage("You said: " + text);
                    }
                    catch (Exception)
                    {

                    }
                        
                    
                }
               clientSocket.Close();
            }
           
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void Form2_FormClosing(object sender, EventArgs e)
        {
            
        }
    }
}

/*
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
            InfoMessage("Server khởi động, đang lắng nghe...");

            // Tạo socket listener
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            listenerSocket.Bind(localEP);
            listenerSocket.Listen(10); // backlog 10

            while (true)
            {
                InfoMessage("Waiting for connection");
                Socket clientSocket = null;
                try
                {
                    clientSocket = listenerSocket.Accept();
                    InfoMessage("New client connected: " + clientSocket.RemoteEndPoint);

                    // Đọc dữ liệu từ client
                    byte[] buffer = new byte[1024];
                    int bytesRecv;
                    StringBuilder sb = new StringBuilder();

                    while ((bytesRecv = clientSocket.Receive(buffer)) > 0)
                    {
                        string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRecv);
                        sb.Append(chunk);

                        // Nếu đã đọc tới dấu newline thì in ra và xóa buffer
                        if (chunk.EndsWith("\n"))
                        {
                            string message = sb.ToString().TrimEnd('\r', '\n');
                            InfoMessage("You said: " + message);
                            sb.Clear();
                        }
                    }
                    // Nếu Receive trả về 0 thì client đã đóng kết nối
                }
                catch (SocketException se)
                {
                    InfoMessage("Socket exception: " + se.Message);
                }
                catch (Exception ex)
                {
                    InfoMessage("Error: " + ex.Message);
                }
                finally
                {
                    if (clientSocket != null)
                    {
                        clientSocket.Close();
                        InfoMessage("Client disconnected");
                    }
                }
                // Vòng while tiếp theo sẽ quay lại Accept() để chờ client mới
            }

            // Chú ý: nếu bạn cần dừng server, bạn có thể break ra và gọi listenerSocket.Close()
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

*/
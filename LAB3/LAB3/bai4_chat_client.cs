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
    public partial class bai4_chat_client : Form
    {
        private delegate void InfoMessageDel(string info);
        private TcpClient tcpClient;
        private NetworkStream ns;
        private IPEndPoint ipEndPoint;
        private Thread UpdateUI;
        public bai4_chat_client()
        {
            InitializeComponent();
        }

        private void send_Click(object sender, EventArgs e)
        {
            SendData(tbmessage.Text);
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
        private bool NewClient()
        {
            try
            {
                //1 Tạo đối tượng TcpClient
                tcpClient = new TcpClient();
                //2 Kết nối đến Server với 1 địa chỉ Ip và Port xác định
                ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
                tcpClient.Connect(ipEndPoint);
                ns = tcpClient.GetStream();
                Thread Receiver = new Thread(ReceiveFromSever);
                Receiver.Start();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tcpClient = null;
                ipEndPoint = null;
                this.Close();
                return false;
            }
        }
        private void ReceiveFromSever()
        {
            //thiet lap ket noi de nhan du lieu tu server
            try
            {
                byte[] recv = new byte[1024];
                while (true)
                {
                    string text = "";
                    int bytesReceived = tcpClient.Client.Receive(recv);
                    text = Encoding.UTF8.GetString(recv, 0, bytesReceived);

                    if (text == "server quit")
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(name.Text + ": quit");
                        SendData("quit because server stop listenning");
                        tcpClient.Close();
                        this.Dispose();
                        this.Close();
                        break;
                    }
                    UpdateUI = new Thread(() => UpdateUIThread(text));
                    UpdateUI.Start();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đóng kết nối!");
                this.Close();
            }
        }

        private void UpdateUIThread(string text)
        {
           InfoMessage(text);
        }

        private void SendData(string msg)
        {
            //3 Tạo luồng để đọc và ghi dữ liệu dựa trên NetworkStream
            ns = tcpClient.GetStream();
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(name.Text + ": " + msg);
            ns.Write(data, 0, data.Length);
        }

        private bool CloseClient()
        {
            try
            {
                if (tcpClient != null)
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(name.Text + ": quit");
                    ns = tcpClient.GetStream();
                    ns.Write(data, 0, data.Length);
                    ns.Close();
                    tcpClient.Close();
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                this.Close();
                return true;
            }
            //5 Dùng phương thức Write để gửi dữ liệu mang dấu hiệu kết thúc cho Server
            // biết và đóng kết nối
        }

        private void bai4_chat_client_Load(object sender, EventArgs e)
        {
            if (!NewClient()) this.Close();
        }
        private void bai4_chat_client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseClient())
            {
                e.Cancel = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

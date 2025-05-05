using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class bai1_UDP_server : Form
    {
        private delegate void InfoMessageDel(string info);

        public bai1_UDP_server()
        {
            InitializeComponent();
        }

        public void serverThread()
        {
            try
            {
                UdpClient udpClient = new UdpClient(int.Parse(ServerPortB1.Text));
                while (true)
                {
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    string mess = RemoteIpEndPoint.Address.ToString() + ": " +
                        returnData.ToString();
                    InfoMessage(mess);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InfoMessage(string info)
        {
            if (messagelist.InvokeRequired)
            {
                InfoMessageDel method = new InfoMessageDel(InfoMessage);
                messagelist.Invoke(method, new object[] { info });
                return;
            }
            messagelist.Items.Add(info);
        }

        private void Listen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = true;
            Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
            thdUDPServer.Start();
        }

        private void messagelist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}












































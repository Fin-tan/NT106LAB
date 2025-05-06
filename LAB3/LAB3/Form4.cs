using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void server_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread ServerThrd = new Thread(ServerThread);
            ServerThrd.Start();
            Thread isServerAliv = new Thread(() => isServerAlive(ServerThrd));
            isServerAliv.Start();
        }
        private void isServerAlive(Thread ServerThrd)
        {
            while (true)
            {
                if (ServerThrd.IsAlive)
                {
                    Invoke(new Action(() => server.Enabled = false));
                }
                else
                {
                    Invoke(new Action(() => server.Enabled = true));
                    break;
                }
            }
        }
        private void ServerThread()
        {
            bai4_chat_server ChatServer = new bai4_chat_server();
            ChatServer.Show();
        }


        private void ClientThread()
        {
            bai4_chat_client ChatClient = new bai4_chat_client();
            ChatClient.Show();
        }

        private void client_Click(object sender, EventArgs e)
        {
            Thread ClientThrd = new Thread(ClientThread);
            ClientThrd.Start();
        }
    }
}

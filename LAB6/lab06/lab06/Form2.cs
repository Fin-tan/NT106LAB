using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace lab06
{
    public partial class Form2 : Form
    {
        private TcpListener server;
        private bool isRunning = false;
        private readonly int MaxClient = 5;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object clientListLock = new object();

        private Bitmap whiteboard;
        private Graphics whiteboardGraphics;
        private readonly object whiteboardLock = new object();

        public Form2()
        {
            InitializeComponent();

            // Gán sự kiện cho panel1 Paint
            panel1.Paint += Panel1_Paint;

            // Gán sự kiện cho nút
            btnstartserver.Click += Btnstartserver_Click;
            btnclose.Click += Btnclose_Click;

            // Khởi label ban đầu
            countclient.Text = "Clients: 0";
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            lock (whiteboardLock)
            {
                if (whiteboard != null)
                {
                    e.Graphics.DrawImage(whiteboard, Point.Empty);
                }
            }
        }

        private void Btnstartserver_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            // Khởi bitmap dựa trên kích thước panel1
            whiteboard = new Bitmap(panel1.Width, panel1.Height);
            using (var g = Graphics.FromImage(whiteboard))
                g.Clear(Color.White);
            whiteboardGraphics = Graphics.FromImage(whiteboard);

            // Start server ở port 4871 (hoặc port bạn chọn)
            server = new TcpListener(IPAddress.Any, 4871);
            server.Start();
            isRunning = true;

            // Bật thread accept client
            new Thread(AcceptClients) { IsBackground = true }.Start();

            btnstartserver.Enabled = false;
            Log("Server started");
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            if (!isRunning) return;
            StopServer();
            btnstartserver.Enabled = true;
            Log("Server stopped");
        }

        private void AcceptClients()
        {
            bool emailSent = false;
            while (isRunning)
            {
                lock (clientListLock)
                {
                    if (clients.Count >= MaxClient)
                    {
                        if (!emailSent)
                        {
                            SendWarningEmail();
                            emailSent = true;
                        }
                        Thread.Sleep(500);
                        continue;
                    }
                    emailSent = false;
                }
                try
                {
                    var client = server.AcceptTcpClient();
                    lock (clientListLock)
                    {
                        clients.Add(client);
                    }
                    // Gửi ảnh hiện tại cho client mới
                    var data = SerializeWhiteboard();
                    SendImageDataToClient(client, data);

                    UpdateClientCount();
                    // Start xử lý client
                    new Thread(() => HandleClient(client)) { IsBackground = true }.Start();

                    Log($"Client connected: {client.Client.RemoteEndPoint}");
                }
                catch { }
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                using var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                while (isRunning && client.Connected)
                {
                    string cmd;
                    try { cmd = reader.ReadString(); }
                    catch { break; }
                    switch (cmd)
                    {
                        case "DRAW":
                            ProcessDraw(reader, client);
                            break;
                        case "IMAGE":
                            ProcessImage(reader, client);
                            break;
                        case "END":
                            RemoveClient(client);
                            return;
                    }
                }
            }
            catch { }
            finally
            {
                RemoveClient(client);
            }
        }

        private void ProcessDraw(BinaryReader reader, TcpClient sender)
        {
            try
            {
                int x1 = reader.ReadInt32();
                int y1 = reader.ReadInt32();
                int x2 = reader.ReadInt32();
                int y2 = reader.ReadInt32();
                Color color = Color.FromArgb(reader.ReadInt32());
                int width = reader.ReadInt32();

                lock (whiteboardLock)
                {
                    using var pen = new Pen(color, width);
                    whiteboardGraphics.DrawLine(pen, x1, y1, x2, y2);
                }
                // Yêu cầu vẽ lại panel trên UI thread
                panel1.Invoke((MethodInvoker)(() => panel1.Invalidate()));

                // Broadcast tới client khác
                BroadcastDrawing(x1, y1, x2, y2, color.ToArgb(), width, sender);
            }
            catch { }
        }

        private void ProcessImage(BinaryReader reader, TcpClient sender)
        {
            try
            {
                int length = reader.ReadInt32();
                byte[] data = reader.ReadBytes(length);
                Bitmap newBmp;
                using var ms = new MemoryStream(data);
                newBmp = new Bitmap(ms);

                lock (whiteboardLock)
                {
                    whiteboard.Dispose();
                    whiteboard = newBmp;
                    whiteboardGraphics?.Dispose();
                    whiteboardGraphics = Graphics.FromImage(whiteboard);
                }
                panel1.Invoke((MethodInvoker)(() => panel1.Invalidate()));

                BroadcastImage(sender, data);
            }
            catch { }
        }

        private void BroadcastDrawing(int x1, int y1, int x2, int y2, int argb, int width, TcpClient sender)
        {
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (!c.Connected || c == sender) continue;
                    try
                    {
                        var stream = c.GetStream();
                        using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                        writer.Write("DRAW");
                        writer.Write(x1);
                        writer.Write(y1);
                        writer.Write(x2);
                        writer.Write(y2);
                        writer.Write(argb);
                        writer.Write(width);
                        writer.Flush();
                    }
                    catch
                    {
                        RemoveClient(c);
                    }
                }
            }
        }

        private void BroadcastImage(TcpClient sender, byte[] imageData)
        {
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (!c.Connected || c == sender) continue;
                    SendImageDataToClient(c, imageData);
                }
            }
        }

        private void SendImageDataToClient(TcpClient client, byte[] imageData)
        {
            try
            {
                var stream = client.GetStream();
                using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                writer.Write("IMAGE");
                writer.Write(imageData.Length);
                writer.Write(imageData);
                writer.Flush();
            }
            catch
            {
                RemoveClient(client);
            }
        }

        private byte[] SerializeWhiteboard()
        {
            lock (whiteboardLock)
            {
                using var clone = (Bitmap)whiteboard.Clone();
                using var ms = new MemoryStream();
                clone.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void UpdateClientCount()
        {
            int cnt;
            lock (clientListLock)
            {
                clients = clients.Where(c => c.Connected).ToList();
                cnt = clients.Count;
            }
            countclient.Invoke((MethodInvoker)(() => countclient.Text = $"Clients: {cnt}"));
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (!c.Connected) continue;
                    try
                    {
                        var stream = c.GetStream();
                        using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                        writer.Write("COUNT");
                        writer.Write(cnt);
                        writer.Flush();
                    }
                    catch
                    {
                        RemoveClient(c);
                    }
                }
            }
        }

        private void RemoveClient(TcpClient client)
        {
            lock (clientListLock)
            {
                if (clients.Remove(client))
                {
                    UpdateClientCount();
                }
            }
            try { client.Close(); } catch { }
            Log("Client disconnected");
        }

        private void StopServer()
        {
            isRunning = false;
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    try
                    {
                        var stream = c.GetStream();
                        using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                        writer.Write("SERVEREND");
                        writer.Flush();
                        c.Close();
                    }
                    catch { }
                }
                clients.Clear();
            }
            try { server.Stop(); } catch { }
            countclient.Invoke((MethodInvoker)(() => countclient.Text = "Clients: 0"));
            lock (whiteboardLock)
            {
                whiteboardGraphics?.Dispose();
                whiteboard = null;
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private void Log(string text)
        {
            // Nếu bạn có TextBox log, append ở đây; nếu không, bạn có thể ghi ra Debug hoặc bỏ qua
            // Ví dụ:
            // Console.WriteLine(text);
        }

        public void SendWarningEmail()
        {
            // Implement gửi email như bạn đã có
        }
    }
}

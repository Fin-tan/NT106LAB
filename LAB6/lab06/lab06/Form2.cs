using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging; // Added for ImageFormat
using System.Net.Mail; // Added for Email
using System.Net; // Added for NetworkCredential (if not already there)

namespace lab06
{
    public partial class Form2 : Form
    {
        private TcpListener server;
        private bool isRunning = false;
        private readonly int MaxClient = 5; // Giới hạn client cho yêu cầu email
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object clientListLock = new object();

        private Bitmap whiteboard;
        private Graphics whiteboardGraphics;
        private readonly object whiteboardLock = new object();

        public Form2()
        {
            InitializeComponent();

            // Bật double buffering cho panel1 và Form để chống nhấp nháy
            this.DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panel1, new object[] { true });

            panel1.Paint += Panel1_Paint;
            btnstartserver.Click += Btnstartserver_Click;
            btnclose.Click += Btnclose_Click;

            countclient.Text = "Clients: 0";

            this.FormClosing += Server_FormClosing; // Đảm bảo sự kiện này được gán
            panel1.Resize += Panel1_Resize; // Mới: Xử lý sự kiện Resize cho Panel
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeWhiteboard();
        }

        private void InitializeWhiteboard()
        {
            lock (whiteboardLock)
            {
                if (whiteboardGraphics != null) whiteboardGraphics.Dispose();
                if (whiteboard != null) whiteboard.Dispose();

                if (panel1.Width > 0 && panel1.Height > 0)
                {
                    whiteboard = new Bitmap(panel1.Width, panel1.Height);
                    whiteboardGraphics = Graphics.FromImage(whiteboard);
                    whiteboardGraphics.Clear(Color.White); // Khởi tạo whiteboard với màu trắng
                }
            }
            panel1.Invalidate();
        }

        // ***** Mới: Xử lý sự kiện Panel1_Resize *****
        private void Panel1_Resize(object sender, EventArgs e)
        {
            InitializeWhiteboard(); // Tái tạo whiteboard khi panel thay đổi kích thước
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

            InitializeWhiteboard(); // Đảm bảo whiteboard được khởi tạo khi server start

            try
            {
                server = new TcpListener(IPAddress.Any, 4871);
                server.Start();
                isRunning = true;
                Log("Server started. Waiting for connections...");
                new Thread(AcceptClients).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể khởi động server: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Lỗi khởi động server: {ex.Message}");
            }
        }

        private void AcceptClients()
        {
            try
            {
                while (isRunning)
                {
                    TcpClient client = server.AcceptTcpClient();
                    lock (clientListLock)
                    {
                        clients.Add(client);
                        Log($"Client {client.Client.RemoteEndPoint} connected. Total clients: {clients.Count}");
                        UpdateClientCountLabel();
                        CheckClientLimitAndSendEmail(); // ***** Mới: Kiểm tra và gửi email *****
                    }
                    new Thread(() => ListenToClient(client)).Start();
                    var data = SerializeWhiteboard(); // Lấy dữ liệu hình ảnh của whiteboard hiện tại
                    SendImageDataToClient(client, data);
                }
            }
            catch (SocketException sex)
            {
                if (isRunning)
                {
                    Log($"Lỗi Socket khi chấp nhận client: {sex.Message}");
                }
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi chấp nhận client: {ex.Message}");
            }
        }
        // Form2.cs
        private void SendImageDataToClient(TcpClient client, byte[] imageData)
        {
            try
            {
                var stream = client.GetStream();
                using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                writer.Write("IMAGE"); // Gửi lệnh "IMAGE"
                writer.Write(imageData.Length); // Gửi độ dài dữ liệu
                writer.Write(imageData); // Gửi dữ liệu hình ảnh
                writer.Flush();
            }
            catch { RemoveClient(client); } // Xóa client nếu gửi thất bại
        }
        private byte[] SerializeWhiteboard()
        {
            lock (whiteboardLock)
            {
                if (whiteboard == null)
                {
                    // Trả về mảng byte rỗng hoặc dữ liệu cho một whiteboard trống
                    using (var ms = new MemoryStream())
                    {
                        // Tạo một bitmap trắng nếu chưa có
                        Bitmap blankBitmap = new Bitmap(panel1.Width, panel1.Height);
                        using (Graphics g = Graphics.FromImage(blankBitmap))
                        {
                            g.FillRectangle(Brushes.White, 0, 0, blankBitmap.Width, blankBitmap.Height);
                        }
                        blankBitmap.Save(ms, ImageFormat.Png); // Luôn gửi định dạng PNG
                        return ms.ToArray();
                    }
                }
                using (var ms = new MemoryStream())
                {
                    // Sao chép bitmap để tránh lỗi GDI+ khi đang được vẽ
                    using (var clone = new Bitmap(whiteboard))
                    {
                        clone.Save(ms, ImageFormat.Png); // Luôn gửi định dạng PNG
                    }
                    return ms.ToArray();
                }
            }
        }
        private void ListenToClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);

                // ***** Mới: Gửi trạng thái whiteboard hiện tại khi client kết nối *****
                SendCurrentWhiteboardToClient(client);
                // *******************************************************************

                while (client.Connected)
                {
                    string command = reader.ReadString();
                    if (command == "DRAW")
                    {
                        int x1 = reader.ReadInt32();
                        int y1 = reader.ReadInt32();
                        int x2 = reader.ReadInt32();
                        int y2 = reader.ReadInt32();
                        int argb = reader.ReadInt32();
                        int thickness = reader.ReadInt32();

                        lock (whiteboardLock)
                        {
                            if (whiteboardGraphics == null || whiteboard == null) // Ensure initialized
                            {
                                InitializeWhiteboard();
                            }
                            using (Pen pen = new Pen(Color.FromArgb(argb), thickness))
                            {
                                whiteboardGraphics.DrawLine(pen, x1, y1, x2, y2);
                            }
                        }
                        panel1.Invoke((MethodInvoker)(() => panel1.Invalidate())); // Cập nhật UI server

                        // Broadcast DRAW command cho các client khác
                        BroadcastDrawCommand(client, x1, y1, x2, y2, argb, thickness);
                    }
                    else if (command == "INSERT_IMAGE") // ***** Mới: Lệnh nhận và chèn ảnh từ client *****
                    {
                        ReceiveAndInsertImageFromClient(reader, client);
                    }
                    else if (command == "END")
                    {
                        Log($"Client {client.Client.RemoteEndPoint} sent END command. Disconnecting...");
                        break;
                    }
                }
            }
            catch (IOException ioex)
            {
                Log($"Client {client.Client.RemoteEndPoint} disconnected unexpectedly: {ioex.Message}");
            }
            catch (ObjectDisposedException odex)
            {
                Log($"Client {client.Client.RemoteEndPoint} stream was disposed: {odex.Message}");
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi lắng nghe client {client.Client.RemoteEndPoint}: {ex.Message}");
            }
            finally
            {
                RemoveClient(client);
            }
        }

        // ***** Mới: Gửi toàn bộ whiteboard hiện tại cho client mới kết nối *****
        private void SendCurrentWhiteboardToClient(TcpClient client)
        {
            lock (whiteboardLock)
            {
                if (whiteboard != null)
                {
                    try
                    {
                        using (var ms = new MemoryStream())
                        {
                            whiteboard.Save(ms, ImageFormat.Png); // Lưu whiteboard vào stream
                            byte[] imageData = ms.ToArray();

                            var stream = client.GetStream();
                            using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);

                            writer.Write("UPDATE_WHITEBOARD"); // Gửi lệnh cập nhật toàn bộ whiteboard
                            writer.Write(imageData.Length);
                            writer.Write(imageData);
                            writer.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Lỗi gửi whiteboard hiện tại cho client {client.Client.RemoteEndPoint}: {ex.Message}");
                    }
                }
            }
        }

        // ***** Mới: Nhận và chèn ảnh từ client, sau đó broadcast *****
        private void ReceiveAndInsertImageFromClient(BinaryReader reader, TcpClient sender)
        {
            try
            {
                int length = reader.ReadInt32();
                byte[] imageData = reader.ReadBytes(length);
                int x = reader.ReadInt32(); // Đọc tọa độ X từ client
                int y = reader.ReadInt32(); // Đọc tọa độ Y từ client

                using (var ms = new MemoryStream(imageData))
                {
                    Bitmap receivedImage = new Bitmap(ms);

                    lock (whiteboardLock)
                    {
                        if (whiteboard == null || whiteboardGraphics == null)
                        {
                            InitializeWhiteboard(); // Đảm bảo whiteboard được khởi tạo
                        }
                        // Vẽ ảnh đã nhận vào whiteboard của server
                        // Đảm bảo ảnh không vượt quá bounds của whiteboard (có thể điều chỉnh logic này)
                        int drawX = Math.Max(0, Math.Min(x, whiteboard.Width - receivedImage.Width));
                        int drawY = Math.Max(0, Math.Min(y, whiteboard.Height - receivedImage.Height));

                        whiteboardGraphics.DrawImage(receivedImage, drawX, drawY);
                    }
                    panel1.Invoke((MethodInvoker)(() => panel1.Invalidate())); // Cập nhật UI server

                    // Broadcast lệnh "INSERT_IMAGE" cùng dữ liệu ảnh và vị trí cho các client khác
                    BroadcastInsertImage(sender, imageData, x, y);
                }
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi nhận hoặc chèn ảnh từ client: {ex.Message}");
            }
        }

        // ***** Mới: Broadcast lệnh chèn ảnh cho các client khác *****
        private void BroadcastInsertImage(TcpClient sender, byte[] imageData, int x, int y)
        {
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (!c.Connected || c == sender) continue; // Không gửi lại cho người gửi
                    try
                    {
                        var stream = c.GetStream();
                        using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                        writer.Write("INSERT_IMAGE");
                        writer.Write(imageData.Length);
                        writer.Write(imageData);
                        writer.Write(x); // Gửi lại tọa độ
                        writer.Write(y);
                        writer.Flush();
                    }
                    catch
                    {
                        RemoveClient(c); // Xóa client nếu có lỗi gửi
                    }
                }
            }
        }

        private void BroadcastDrawCommand(TcpClient sender, int x1, int y1, int x2, int y2, int argb, int thickness)
        {
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (!c.Connected || c == sender) continue; // Không gửi lại cho người gửi
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
                        writer.Write(thickness);
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
                    client.Close();
                    if (client.Client != null)
                    {
                        Log($"Client {client.Client.RemoteEndPoint} removed. Total clients: {clients.Count}");
                    }
                    else
                    {
                        Log($"Client removed. Total clients: {clients.Count}");
                    }
                    Log($"Client {client.Client.RemoteEndPoint} removed. Total clients: {clients.Count}");
                    UpdateClientCountLabel();
                }
            }
        }

        private void UpdateClientCountLabel()
        {
            countclient.Invoke((MethodInvoker)(() => countclient.Text = $"Clients: {clients.Count}"));
            BroadcastClientCount(); // Gửi số lượng client đã cập nhật cho tất cả client
        }

        private void BroadcastClientCount()
        {
            lock (clientListLock)
            {
                foreach (var c in clients.ToList())
                {
                    try
                    {
                        var stream = c.GetStream();
                        using var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                        writer.Write("UPDATE_CLIENT_COUNT");
                        writer.Write(clients.Count);
                        writer.Flush();
                    }
                    catch
                    {
                        RemoveClient(c);
                    }
                }
            }
        }


        private void Btnclose_Click(object sender, EventArgs e)
        {
            StopServer();
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
            try { server?.Stop(); } catch { } // Use null-conditional operator
            countclient.Invoke((MethodInvoker)(() => countclient.Text = "Clients: 0"));
            lock (whiteboardLock)
            {
                whiteboardGraphics?.Dispose();
                whiteboard = null;
            }
            Log("Server stopped.");
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }

        private void Log(string text)
        {
            Console.WriteLine(text); // Hoặc bạn có thể ghi vào một TextBox log trên Form
        }

        // ***** Mới: Kiểm tra và gửi email cảnh báo *****
        private void CheckClientLimitAndSendEmail()
        {
            lock (clientListLock)
            {
                if (clients.Count >= MaxClient)
                {
                    SendWarningEmail();
                }
            }
        }

        public void SendWarningEmail()
        {
            // Cần cấu hình thông tin SMTP của bạn
            string fromMail = "your_email@example.com"; // Thay bằng email của bạn
            string fromPassword = "your_app_password"; // Thay bằng mật khẩu ứng dụng (App Password) của bạn

            // Cấu hình email quản trị viên
            string toMail = "admin_email@example.com"; // Thay bằng email quản trị viên

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "CẢNH BÁO: Số lượng Client đạt giới hạn!";
                message.To.Add(new MailAddress(toMail));
                message.Body = $"Whiteboard Server đã đạt đến giới hạn số lượng client cho phép ({MaxClient}).\n" +
                               $"Tổng số client hiện tại: {clients.Count}";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; // Hoặc SMTP server của nhà cung cấp dịch vụ email của bạn
                smtp.Port = 587; // Cổng SMTP, thường là 587 cho TLS
                smtp.Credentials = new NetworkCredential(fromMail, fromPassword);
                smtp.EnableSsl = true;
                smtp.Send(message);

                Log("Email cảnh báo đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi gửi email cảnh báo: {ex.Message}");
                // Ghi log lỗi chi tiết hơn nếu có thể
            }
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void BeNothing()
        {
            tbDiemToan.Text = "";
            tbDiemVan.Text = "";
            tbDienThoai.Text = "";
            tbHoTen.Text = "";
            tbMSSV.Text = "";
        }

        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(tbMSSV.Text) || tbMSSV.Text.Length < 8 || !long.TryParse(tbMSSV.Text, out _))
                return false;

            if (string.IsNullOrWhiteSpace(tbHoTen.Text) || !tbHoTen.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                return false;

            if (string.IsNullOrWhiteSpace(tbDienThoai.Text) || tbDienThoai.Text.Length < 9 || !tbDienThoai.Text.All(char.IsDigit))
                return false;

            string toanInput = tbDiemToan.Text.Replace(',', '.');
            string vanInput = tbDiemVan.Text.Replace(',', '.');

            if (!float.TryParse(toanInput, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float diemToan) || diemToan < 0 || diemToan > 10)
                return false;

            if (!float.TryParse(vanInput, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float diemVan) || diemVan < 0 || diemVan > 10)
                return false;

            return true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbNumOfSV.Text, out _))
            {
                bntBatDauNhap.Enabled = false;
                tbNumOfSV.Enabled = false;
                tbMSSV.Enabled = true;
                tbHoTen.Enabled = true;
                tbDienThoai.Enabled = true;
                tbDiemVan.Enabled = true;
                tbDiemToan.Enabled = true;
                tbInput.Enabled = true;
                tbOutput.Enabled = true;
                btnWriteInput.Enabled = true;
                btnRead.Enabled = false;
            }
            else
            {
                MessageBox.Show("Xin Nhập số lượng sinh viên");
            }

            if (tbNumOfSV.Text == "0")
            {
                bntBatDauNhap.Enabled = true;
                tbNumOfSV.Enabled = true;
                tbMSSV.Enabled = false;
                tbHoTen.Enabled = false;
                tbDienThoai.Enabled = false;
                tbDiemVan.Enabled = false;
                tbDiemToan.Enabled = false;
                btnWriteInput.Enabled = false;
                btnRead.Enabled = true;
                BeNothing();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            tbMSSV.Enabled = false;
            tbHoTen.Enabled = false;
            tbDienThoai.Enabled = false;
            tbDiemVan.Enabled = false;
            tbDiemToan.Enabled = false;
            tbInput.Enabled = false;
            tbOutput.Enabled = false;
            btnWriteInput.Enabled = false;
            btnRead.Enabled = false;
        }

        private void btnWriteInput_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                SinhVien sv = new SinhVien
                {
                    MSSV = tbMSSV.Text.Trim(),
                    HoTen = tbHoTen.Text.Trim(),
                    DienThoai = tbDienThoai.Text.Trim(),
                    DiemToan = float.Parse(tbDiemToan.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                    DiemVan = float.Parse(tbDiemVan.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture)
                };

                List<SinhVien> danhSach;

                string path = "..\\input.txt";

                // Đọc danh sách hiện có nếu file tồn tại
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
#pragma warning disable SYSLIB0011
                        try
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            danhSach = (List<SinhVien>)bf.Deserialize(fs);
                        }
                        catch
                        {
                            danhSach = new List<SinhVien>();
                        }
#pragma warning restore SYSLIB0011
                    }
                }
                else
                {
                    danhSach = new List<SinhVien>();
                }

                // Thêm sinh viên mới
                danhSach.Add(sv);

                // Ghi lại toàn bộ danh sách
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
#pragma warning disable SYSLIB0011
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, danhSach);
#pragma warning restore SYSLIB0011
                }
                // Ghi dữ liệu vào tbInput (không có DiemTB)
                tbInput.Text += $"MSSV: {sv.MSSV}\n";
                tbInput.Text += $"Họ Tên: {sv.HoTen}\n";
                tbInput.Text += $"SĐT: {sv.DienThoai}\n";
                tbInput.Text += $"Toán: {sv.DiemToan}\n";
                tbInput.Text += $"Văn: {sv.DiemVan}\r\n";
                tbInput.Text += "\n";

                // Trừ số lượng cần nhập
                tbNumOfSV.Text = (int.Parse(tbNumOfSV.Text) - 1).ToString();
                BeNothing();

                // Cập nhật đường dẫn hiển thị
                tbPathInput.Text = Path.GetFullPath(path);
            }
            else
            {
                MessageBox.Show("Xin nhập đủ và đúng định dạng thông tin");
                return;
            }

            // Khi nhập xong
            if (tbNumOfSV.Text == "0")
            {
                bntBatDauNhap.Enabled = true;
                tbNumOfSV.Enabled = true;

                tbMSSV.Enabled = false;
                tbHoTen.Enabled = false;
                tbDienThoai.Enabled = false;
                tbDiemVan.Enabled = false;
                tbDiemToan.Enabled = false;

                btnWriteInput.Enabled = false;
                btnRead.Enabled = true;
                BeNothing();
            }
        
        }



        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText("..\\output.txt", string.Empty);
            File.WriteAllText("..\\input.txt", string.Empty, Encoding.UTF8);
            if (File.Exists("..\\sv_binary.dat"))
                File.Delete("..\\sv_binary.dat");

            tbInput.Text = "";
            tbOutput.Text = "";
            BeNothing();
            tbMSSV.Enabled = false;
            tbHoTen.Enabled = false;
            tbDienThoai.Enabled = false;
            tbDiemVan.Enabled = false;
            tbDiemToan.Enabled = false;
            btnWriteInput.Enabled = false;
            btnRead.Enabled = false;
            tbNumOfSV.Enabled = true;
            bntBatDauNhap.Enabled = true;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string path = "..\\input.txt";
            string outputPath = "..\\output.txt";

            tbPathOutput.Text = Path.GetFullPath(outputPath);
            List<string> outputLines = new List<string>();

            if (!File.Exists(path))
            {
                MessageBox.Show("Không tìm thấy file input.");
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {


                BinaryFormatter bf = new BinaryFormatter();

                try
                {
                    while (fs.Position < fs.Length)
                    {
#pragma warning disable SYSLIB0011
                        List<SinhVien> ds = (List<SinhVien>)bf.Deserialize(fs);
                        foreach (var sv in ds)
                        {
                            outputLines.Add($"MSSV: {sv.MSSV}");
                            outputLines.Add($"Họ Tên: {sv.HoTen}");
                            outputLines.Add($"SĐT: {sv.DienThoai}");
                            outputLines.Add($"Toán: {sv.DiemToan}");
                            outputLines.Add($"Văn: {sv.DiemVan}");
                            outputLines.Add($"Điểm TB: {sv.DiemTB:F2}");
                            outputLines.Add("");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đọc file: " + ex.Message);
                }
                finally
                {
                    fs.Close();
                }

                File.WriteAllLines(outputPath, outputLines, Encoding.UTF8);
                tbOutput.Text = File.ReadAllText(outputPath, Encoding.UTF8);
            }
        }



        private bool MustBeNum(char num)
        {
            return char.IsDigit(num) || num == '.';
        }

        private bool MustBeLetter(char letter)
        {
            return char.IsLetter(letter) || char.IsWhiteSpace(letter);
        }

        private void tbMSSV_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbMSSV.Text)
            {
                if (!char.IsDigit(item))
                {
                    MessageBox.Show("MSSV chỉ được chứa số");
                    tbMSSV.Text = "";
                    return;
                }
            }
        }


        private void tbHoTen_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbHoTen.Text)
            {
                if (!char.IsLetter(item) && !char.IsWhiteSpace(item))
                {
                    MessageBox.Show("Họ tên chỉ được chứa chữ cái");
                    tbHoTen.Text = "";
                    return;
                }
            }
        }

        private void tbDienThoai_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbDienThoai.Text)
            {
                if (!char.IsDigit(item))
                {
                    MessageBox.Show("SĐT chỉ được chứa số");
                    tbDienThoai.Text = "";
                    return;
                }
            }
        }


        private void tbDiemToan_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(tbDiemToan.Text, out float value) || value < 0 || value > 10)
            {
                if (!string.IsNullOrEmpty(tbDiemToan.Text))
                    MessageBox.Show("Điểm Toán phải là số thực từ 0 đến 10");
                tbDiemToan.Text = "";
            }
        }


        private void tbDiemVan_TextChanged(object sender, EventArgs e)
        {
            foreach (char item in tbDiemVan.Text)
            {
                if (!MustBeNum(item)) { MessageBox.Show("Phải là số"); tbDiemVan.Text = ""; return; }
            }
        }

        private void tbOutput_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }

    [Serializable]
    public class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public float DiemToan { get; set; }
        public float DiemVan { get; set; }

        public float DiemTB => (DiemToan + DiemVan) / 2;

        public override string ToString()
        {
            return $"MSSV: {MSSV}, Họ tên: {HoTen}, SĐT: {DienThoai}, Toán: {DiemToan}, Văn: {DiemVan}, TB: {DiemTB}";
        }
    }
}

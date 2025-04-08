namespace lab2
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bntBatDauNhap = new Button();
            button2 = new Button();
            btnWriteInput = new Button();
            btnRead = new Button();
            tbMSSV = new TextBox();
            tbHoTen = new TextBox();
            tbDienThoai = new TextBox();
            tbDiemToan = new TextBox();
            tbDiemVan = new TextBox();
            tbInput = new TextBox();
            tbOutput = new TextBox();
            sosinhvien = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            tbNumOfSV = new TextBox();
            tbPathInput = new TextBox();
            tbPathOutput = new TextBox();
            SuspendLayout();
            // 
            // bntBatDauNhap
            // 
            bntBatDauNhap.Location = new Point(379, 12);
            bntBatDauNhap.Name = "bntBatDauNhap";
            bntBatDauNhap.Size = new Size(75, 23);
            bntBatDauNhap.TabIndex = 0;
            bntBatDauNhap.Text = "Nhap";
            bntBatDauNhap.UseVisualStyleBackColor = true;
            bntBatDauNhap.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(489, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "reset";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btnWriteInput
            // 
            btnWriteInput.Location = new Point(106, 167);
            btnWriteInput.Name = "btnWriteInput";
            btnWriteInput.Size = new Size(75, 23);
            btnWriteInput.TabIndex = 2;
            btnWriteInput.Text = "Ghi file";
            btnWriteInput.UseVisualStyleBackColor = true;
            btnWriteInput.Click += btnWriteInput_Click;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(637, 180);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(75, 23);
            btnRead.TabIndex = 3;
            btnRead.Text = "Đọc file";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // tbMSSV
            // 
            tbMSSV.Location = new Point(106, 58);
            tbMSSV.Name = "tbMSSV";
            tbMSSV.Size = new Size(100, 23);
            tbMSSV.TabIndex = 4;
            // 
            // tbHoTen
            // 
            tbHoTen.Location = new Point(106, 87);
            tbHoTen.Name = "tbHoTen";
            tbHoTen.Size = new Size(100, 23);
            tbHoTen.TabIndex = 5;
            // 
            // tbDienThoai
            // 
            tbDienThoai.Location = new Point(106, 116);
            tbDienThoai.Name = "tbDienThoai";
            tbDienThoai.Size = new Size(100, 23);
            tbDienThoai.TabIndex = 6;
            // 
            // tbDiemToan
            // 
            tbDiemToan.Location = new Point(464, 58);
            tbDiemToan.Name = "tbDiemToan";
            tbDiemToan.Size = new Size(100, 23);
            tbDiemToan.TabIndex = 7;
            // 
            // tbDiemVan
            // 
            tbDiemVan.Location = new Point(464, 87);
            tbDiemVan.Name = "tbDiemVan";
            tbDiemVan.Size = new Size(100, 23);
            tbDiemVan.TabIndex = 8;
            // 
            // tbInput
            // 
            tbInput.Location = new Point(106, 209);
            tbInput.Multiline = true;
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(268, 151);
            tbInput.TabIndex = 9;
            tbInput.TextChanged += tbInput_TextChanged;
            // 
            // tbOutput
            // 
            tbOutput.Location = new Point(446, 209);
            tbOutput.Multiline = true;
            tbOutput.Name = "tbOutput";
            tbOutput.ScrollBars = ScrollBars.Both;
            tbOutput.Size = new Size(266, 151);
            tbOutput.TabIndex = 10;
            tbOutput.TextChanged += tbOutput_TextChanged;
            // 
            // sosinhvien
            // 
            sosinhvien.AutoSize = true;
            sosinhvien.Location = new Point(185, 20);
            sosinhvien.Name = "sosinhvien";
            sosinhvien.Size = new Size(69, 15);
            sosinhvien.TabIndex = 11;
            sosinhvien.Text = "so sinh vien";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 61);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 12;
            label2.Text = "MSSV";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 90);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 13;
            label3.Text = "Ho Ten";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 124);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 14;
            label4.Text = "Dien thoai";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(379, 61);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 15;
            label5.Text = "Diem toan";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(379, 90);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 16;
            label6.Text = "Diem Van";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(106, 374);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 17;
            label7.Text = "đường dẫn ghi ";
            label7.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(446, 374);
            label8.Name = "label8";
            label8.Size = new Size(88, 15);
            label8.TabIndex = 18;
            label8.Text = "đường dãn lưu ";
            label8.Click += label8_Click;
            // 
            // tbNumOfSV
            // 
            tbNumOfSV.Location = new Point(271, 17);
            tbNumOfSV.Name = "tbNumOfSV";
            tbNumOfSV.Size = new Size(51, 23);
            tbNumOfSV.TabIndex = 19;
            // 
            // tbPathInput
            // 
            tbPathInput.Location = new Point(106, 403);
            tbPathInput.Name = "tbPathInput";
            tbPathInput.Size = new Size(268, 23);
            tbPathInput.TabIndex = 20;
            // 
            // tbPathOutput
            // 
            tbPathOutput.Location = new Point(446, 403);
            tbPathOutput.Name = "tbPathOutput";
            tbPathOutput.Size = new Size(268, 23);
            tbPathOutput.TabIndex = 21;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(827, 513);
            Controls.Add(tbPathOutput);
            Controls.Add(tbPathInput);
            Controls.Add(tbNumOfSV);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(sosinhvien);
            Controls.Add(tbOutput);
            Controls.Add(tbInput);
            Controls.Add(tbDiemVan);
            Controls.Add(tbDiemToan);
            Controls.Add(tbDienThoai);
            Controls.Add(tbHoTen);
            Controls.Add(tbMSSV);
            Controls.Add(btnRead);
            Controls.Add(btnWriteInput);
            Controls.Add(button2);
            Controls.Add(bntBatDauNhap);
            Name = "Form4";
            Text = "Form4";
            Load += Form4_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        // Add the missing event handler method for label2_Click

        #endregion

        private Button bntBatDauNhap;
        private Button button2;
        private Button btnWriteInput;
        private Button btnRead;
        private TextBox tbMSSV;
        private TextBox tbHoTen;
        private TextBox tbDienThoai;
        private TextBox tbDiemToan;
        private TextBox tbDiemVan;
        private TextBox tbInput;
        private TextBox tbOutput;
        private Label sosinhvien;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox tbNumOfSV;
        private TextBox tbPathInput;
        private TextBox tbPathOutput;
    }
}
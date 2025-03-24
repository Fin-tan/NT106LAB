namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox4 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.Location = new Point(124, 191);
            button1.Name = "button1";
            button1.Size = new Size(100, 47);
            button1.TabIndex = 0;
            button1.Text = "Tính tổng";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Cursor = Cursors.Hand;
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.Location = new Point(124, 112);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 29);
            textBox1.TabIndex = 1;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Cursor = Cursors.Hand;
            textBox2.Font = new Font("Segoe UI", 12F);
            textBox2.Location = new Point(293, 112);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 29);
            textBox2.TabIndex = 2;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Cursor = Cursors.Hand;
            textBox3.Font = new Font("Segoe UI", 12F);
            textBox3.Location = new Point(465, 112);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(100, 29);
            textBox3.TabIndex = 3;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F);
            label1.Location = new Point(249, 115);
            label1.Name = "label1";
            label1.Size = new Size(19, 20);
            label1.TabIndex = 4;
            label1.Text = "+";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F);
            label2.Location = new Point(423, 115);
            label2.Name = "label2";
            label2.Size = new Size(19, 20);
            label2.TabIndex = 5;
            label2.Text = "=";
            label2.Click += label2_Click;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textBox4.Location = new Point(224, 34);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(245, 23);
            textBox4.TabIndex = 6;
            textBox4.Text = "TÍNH TỔNG 2 SỐ NGUYÊN";
            textBox4.TextAlign = HorizontalAlignment.Center;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button2.Location = new Point(293, 191);
            button2.Name = "button2";
            button2.Size = new Size(100, 47);
            button2.TabIndex = 7;
            button2.Text = "Xóa";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button3.Location = new Point(465, 191);
            button3.Name = "button3";
            button3.Size = new Size(100, 47);
            button3.TabIndex = 8;
            button3.Text = "Thoát";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private TextBox textBox4;
        private Button button2;
        private Button button3;
    }
}

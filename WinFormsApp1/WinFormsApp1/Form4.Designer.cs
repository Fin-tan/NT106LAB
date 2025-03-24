namespace WinFormsApp1
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            btndoc = new Button();
            button2 = new Button();
            button3 = new Button();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Cyan;
            label1.Location = new Point(221, 229);
            label1.Name = "label1";
            label1.Size = new Size(188, 32);
            label1.TabIndex = 0;
            label1.Text = "\r\n";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 131);
            label2.Name = "label2";
            label2.Size = new Size(174, 15);
            label2.TabIndex = 1;
            label2.Text = "Nhấp số nguyên dương từ 0->9\r\n";
            label2.Click += label2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(221, 128);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(112, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // btndoc
            // 
            btndoc.Location = new Point(436, 123);
            btndoc.Name = "btndoc";
            btndoc.Size = new Size(75, 23);
            btndoc.TabIndex = 4;
            btndoc.Text = "ĐỌC";
            btndoc.UseVisualStyleBackColor = true;
            btndoc.Click += btndoc_Click;
            // 
            // button2
            // 
            button2.Location = new Point(436, 180);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "XÓA";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(436, 238);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 6;
            button3.Text = "THOÁT";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(155, 238);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 7;
            label3.Text = "kết quả\r\n";
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(btndoc);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "Form4";
            Text = "2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button btndoc;
        private Button button2;
        private Button button3;
        private Label label3;
    }
}
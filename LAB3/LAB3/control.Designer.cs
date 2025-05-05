namespace LAB3
{
    partial class control
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
            bai1 = new Button();
            bai2 = new Button();
            bai3 = new Button();
            bai4 = new Button();
            SuspendLayout();
            // 
            // bai1
            // 
            bai1.Location = new Point(138, 75);
            bai1.Name = "bai1";
            bai1.Size = new Size(116, 61);
            bai1.TabIndex = 0;
            bai1.Text = "bai 1";
            bai1.UseVisualStyleBackColor = true;
            bai1.Click += bai1_Click;
            // 
            // bai2
            // 
            bai2.Location = new Point(138, 142);
            bai2.Name = "bai2";
            bai2.Size = new Size(116, 47);
            bai2.TabIndex = 1;
            bai2.Text = "bai2";
            bai2.UseVisualStyleBackColor = true;
            bai2.Click += bai2_Click;
            // 
            // bai3
            // 
            bai3.Location = new Point(138, 195);
            bai3.Name = "bai3";
            bai3.Size = new Size(116, 41);
            bai3.TabIndex = 2;
            bai3.Text = "bai3";
            bai3.UseVisualStyleBackColor = true;
            bai3.Click += bai3_Click;
            // 
            // bai4
            // 
            bai4.Location = new Point(138, 242);
            bai4.Name = "bai4";
            bai4.Size = new Size(116, 50);
            bai4.TabIndex = 3;
            bai4.Text = "bai4";
            bai4.UseVisualStyleBackColor = true;
            bai4.Click += bai4_Click;
            // 
            // control
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 378);
            Controls.Add(bai4);
            Controls.Add(bai3);
            Controls.Add(bai2);
            Controls.Add(bai1);
            Name = "control";
            Text = "control";
            ResumeLayout(false);
        }

        #endregion

        private Button bai1;
        private Button bai2;
        private Button bai3;
        private Button bai4;
    }
}
namespace lab06
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
            pictureBox1 = new PictureBox();
            btnConnect = new Button();
            btnColor = new Button();
            nudThickness = new NumericUpDown();
            btnEnd = new Button();
            lblClientCount = new Label();
            ((System.ComponentModel.ISupportInitialize)nudThickness).BeginInit();
            SuspendLayout();
            // 
            // panelCanvas
            // 
            pictureBox1.Location = new Point(38, 43);
            pictureBox1.Name = "panelCanvas";
            pictureBox1.Size = new Size(549, 332);
            pictureBox1.TabIndex = 0;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(644, 43);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 57);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "connect";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // btnColor
            // 
            btnColor.Location = new Point(644, 154);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(75, 57);
            btnColor.TabIndex = 3;
            btnColor.Text = "color";
            btnColor.UseVisualStyleBackColor = true;
            // 
            // nudThickness
            // 
            nudThickness.Location = new Point(644, 268);
            nudThickness.Name = "nudThickness";
            nudThickness.Size = new Size(120, 23);
            nudThickness.TabIndex = 4;
            // 
            // btnEnd
            // 
            btnEnd.Location = new Point(644, 349);
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(75, 57);
            btnEnd.TabIndex = 5;
            btnEnd.Text = "end";
            btnEnd.UseVisualStyleBackColor = true;
            // 
            // lblClientCount
            // 
            lblClientCount.AutoSize = true;
            lblClientCount.Location = new Point(660, 125);
            lblClientCount.Name = "lblClientCount";
            lblClientCount.Size = new Size(38, 15);
            lblClientCount.TabIndex = 6;
            lblClientCount.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblClientCount);
            Controls.Add(btnEnd);
            Controls.Add(nudThickness);
            Controls.Add(btnColor);
            Controls.Add(btnConnect);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)nudThickness).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnConnect;
        private Button btnColor;
        private NumericUpDown nudThickness;
        private Button btnEnd;
        private Label lblClientCount;
    }
}

namespace lab06
{
    partial class Form2
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
            panel1 = new Panel();
            countclient = new Label();
            btnstartserver = new Button();
            btnclose = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 52);
            panel1.Name = "panel1";
            panel1.Size = new Size(625, 342);
            panel1.TabIndex = 0;
            // 
            // countclient
            // 
            countclient.AutoSize = true;
            countclient.Location = new Point(664, 87);
            countclient.Name = "countclient";
            countclient.Size = new Size(67, 15);
            countclient.TabIndex = 1;
            countclient.Text = "countclient";
            // 
            // btnstartserver
            // 
            btnstartserver.Location = new Point(664, 162);
            btnstartserver.Name = "btnstartserver";
            btnstartserver.Size = new Size(99, 87);
            btnstartserver.TabIndex = 2;
            btnstartserver.Text = "startserver";
            btnstartserver.UseVisualStyleBackColor = true;
            // 
            // btnclose
            // 
            btnclose.Location = new Point(664, 285);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(99, 67);
            btnclose.TabIndex = 3;
            btnclose.Text = "closeserver";
            btnclose.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnclose);
            Controls.Add(btnstartserver);
            Controls.Add(countclient);
            Controls.Add(panel1);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label countclient;
        private Button btnstartserver;
        private Button btnclose;
    }
}
namespace lab4
{
    partial class bai2
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
            url = new TextBox();
            content = new TextBox();
            button1 = new Button();
            result = new RichTextBox();
            SuspendLayout();
            // 
            // url
            // 
            url.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            url.Location = new Point(21, 21);
            url.Multiline = true;
            url.Name = "url";
            url.ReadOnly = true;
            url.Size = new Size(620, 44);
            url.TabIndex = 0;
            url.Text = "https://httpbun.org/post";
            url.TextChanged += url_TextChanged;
            // 
            // content
            // 
            content.Location = new Point(21, 71);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(620, 45);
            content.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(675, 21);
            button1.Name = "button1";
            button1.Size = new Size(97, 44);
            button1.TabIndex = 2;
            button1.Text = "POST";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // result
            // 
            result.Location = new Point(21, 136);
            result.Name = "result";
            result.ReadOnly = true;
            result.Size = new Size(751, 302);
            result.TabIndex = 3;
            result.Text = "";
            // 
            // bai2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(result);
            Controls.Add(button1);
            Controls.Add(content);
            Controls.Add(url);
            Name = "bai2";
            Text = "bai2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox url;
        private TextBox content;
        private Button button1;
        private RichTextBox result;
    }
}
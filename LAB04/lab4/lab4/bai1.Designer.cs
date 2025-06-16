namespace lab4
{
    partial class bai1
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
            urltext = new TextBox();
            Get = new Button();
            html = new RichTextBox();
            SuspendLayout();
            // 
            // urltext
            // 
            urltext.Location = new Point(22, 30);
            urltext.Multiline = true;
            urltext.Name = "urltext";
            urltext.Size = new Size(651, 33);
            urltext.TabIndex = 0;
            // 
            // Get
            // 
            Get.Location = new Point(691, 30);
            Get.Name = "Get";
            Get.Size = new Size(97, 33);
            Get.TabIndex = 1;
            Get.Text = "Get";
            Get.UseVisualStyleBackColor = true;
            Get.Click += Get_Click;
            // 
            // html
            // 
            html.Location = new Point(22, 87);
            html.Name = "html";
            html.ReadOnly = true;
            html.Size = new Size(766, 336);
            html.TabIndex = 2;
            html.Text = "";
            html.TextChanged += richTextBox1_TextChanged;
            // 
            // bai1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(html);
            Controls.Add(Get);
            Controls.Add(urltext);
            Name = "bai1";
            Text = "bai1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox urltext;
        private Button Get;
        private RichTextBox html;
    }
}
namespace DnDM
{
    partial class Note
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
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(192, 192, 255);
            comboBox1.ForeColor = SystemColors.WindowText;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Plot", "Character", "Location", "PastOneShots", "PastCampaign" });
            comboBox1.Location = new Point(25, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(143, 25);
            comboBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Navy;
            textBox1.ForeColor = SystemColors.Info;
            textBox1.Location = new Point(25, 57);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(198, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "Название";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Navy;
            textBox2.ForeColor = SystemColors.Info;
            textBox2.Location = new Point(239, 57);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(211, 23);
            textBox2.TabIndex = 2;
            textBox2.Text = "Описание";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(192, 192, 255);
            textBox3.Location = new Point(26, 100);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(424, 191);
            textBox3.TabIndex = 3;
            textBox3.Text = "Заметка";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.Location = new Point(138, 297);
            button1.Name = "button1";
            button1.Size = new Size(312, 26);
            button1.TabIndex = 4;
            button1.Text = "Сохранить заметку";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 0, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(25, 297);
            button2.Name = "button2";
            button2.Size = new Size(96, 26);
            button2.TabIndex = 5;
            button2.Text = "УДАЛИТЬ";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Note
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            ClientSize = new Size(479, 333);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Name = "Note";
            Text = "Note";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
    }
}
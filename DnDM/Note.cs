using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DnDM
{
    public partial class Note : Form
    {
        public string path = @"C:\LOTT\SinGit\DnDMaster\DnDM\notes.txt";
        public Note(string name)
        {
            InitializeComponent();

            if (name == "")
                comboBox1.SelectedIndex = 0;
            else
            {
                var text = File.ReadAllLines(path);
                var index = -1;
                for (var i = 0; i < text.Length; i++)
                {
                    if (text[i].Split('_')[1] == name)
                    { index = i; break; }
                }
                comboBox1.SelectedItem = text[index].Split('_')[0];
                textBox1.Text = text[index].Split('_')[1];
                textBox2.Text = text[index].Split('_')[2];
                var dop = "";
                for (var i = 3; i < text[index].Split('_').Length; i++)
                    dop += text[index].Split("_")[i] + Environment.NewLine;
                textBox3.Text = dop;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = File.ReadAllLines(path);
            var index = -1;
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i].Split('_')[1] == textBox1.Text)
                { index = i; break; }
            }
            var text3 = textBox3.Text.Replace(Environment.NewLine, "_");
            var newText = comboBox1.Text + "_" + textBox1.Text
                + "_" + textBox2.Text + "_" + text3;
            var answer = MessageBox.Show("Сохраняем заметку " + textBox1.Text + "??", "", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                if (index != -1)
                {
                    text[index] = newText;
                    File.WriteAllLines(path, text);
                }
                else
                {
                    var textWith = new string[text.Length + 1];
                    textWith[0] = newText;
                    for (var i = 1; i < textWith.Length; i++)
                        textWith[i] = text[i - 1];
                    File.WriteAllLines(path, textWith);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var text = File.ReadAllLines(path);
            var name = textBox1.Text;
            var index = -1;
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i].Split('_')[1] == textBox1.Text)
                { index = i; break; }
            }
            var answer = MessageBox.Show("Точно удалить " + textBox1.Text + "??", "", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                if (index != -1)
                {
                    var newText = new string[text.Length - 1];
                    for (int i = 0; i < index; i++)
                        newText[i] = text[i];
                    for (int i = index + 1; i <= newText.Length; i++)
                        newText[i - 1] = text[i];
                    File.WriteAllLines(path, newText);
                }
            }
        }
    }
}

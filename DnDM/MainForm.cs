using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DnDM
{
    public partial class MainForm : Form
    {
        public string path = @"C:\LOTT\SinGit\DnDMaster\DnDM\notes.txt";
        public MainForm()
        {
            InitializeComponent();
            Names();
            comboBox1.SelectedIndex = 0;
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            Note();
        }

        public void Names()
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var name = line.Split("_")[0] + ": " + line.Split("_")[1];
                listBox1.Items.Add(name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Note();
        }

        public void Note()
        {
            if (listBox1.Text == null || listBox1.Text == "")
                return;
            var name = listBox1.Text.Split(": ")[1];
            var text = File.ReadAllLines(path);
            var index = -1;
            for (var i = 0; i < text.Length; i++)
            {
                if (text[i].Split('_')[1] == name)
                { index = i; break; }
            }

            if (index != -1)
            {
                var note = "";
                foreach (var line in text[index].Split('_'))
                {
                    if (line == text[index].Split('_')[1])
                        note += line + Environment.NewLine + Environment.NewLine;
                    else
                        note += line + Environment.NewLine;
                }
                textBox1.Text = note;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sort = comboBox1.SelectedItem.ToString();

            listBox1.Items.Clear();

            Names();
            if (sort != "All")
            {
                var allItems = listBox1.Items.Cast<string>().ToList();

                var filteredItems = allItems.Where(item => item.StartsWith(sort)).ToList();

                listBox1.Items.Clear();
                foreach (var item in filteredItems)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        public void OpenNote(string name)
        {
            var note = new Note(name);
            note.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenNote("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Text != null && listBox1.Text != "")
                OpenNote(listBox1.Text.Split(": ")[1]);
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                string itemText = listBox1.Items[e.Index].ToString();
                Color backgroundColor;

                // ������������� ���� ���� � ����������� �� ������� �����
                switch (itemText.Split(": ")[0])
                {
                    case "Plot":
                        backgroundColor = Color.FromArgb(230, 155, 0);  // ���������
                        break;
                    case "Character":
                        backgroundColor = Color.FromArgb(89, 209, 77);  // ������
                        break;
                    case "Location":
                        backgroundColor = Color.FromArgb(141, 90, 153); // ����������
                        break;
                    default:
                        backgroundColor = Color.White;  // ����� ��� �� ���������
                        break;
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    backgroundColor = Color.Blue;


                // ������������ ���
                e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.Bounds);

                // ������������ �����
                e.Graphics.DrawString(itemText, e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
            }
        }
    }
}

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
            if (!Path.Exists(path))
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\notes.txt";
                if (!Path.Exists(path))
                    File.WriteAllText(path, "");
            }
            Names();
            comboBox1.SelectedIndex = 4;
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
            if (sort != "All" && sort != "Active" && sort != "Past")
            {
                var allItems = listBox1.Items.Cast<string>().ToList();

                var filteredItems = allItems.Where(item => item.StartsWith(sort)).ToList();

                listBox1.Items.Clear();
                foreach (var item in filteredItems)
                {
                    listBox1.Items.Add(item);
                }
            }
            if (sort == "Active")
            {
                var allItems = listBox1.Items.Cast<string>().ToList();

                var filteredItems = allItems.Where(item => item.StartsWith("Plot") || item.StartsWith("Character") || item.StartsWith("Location")).ToList();

                listBox1.Items.Clear();
                foreach (var item in filteredItems)
                {
                    listBox1.Items.Add(item);
                }
            }
            if (sort == "Past")
            {
                var allItems = listBox1.Items.Cast<string>().ToList();

                var filteredItems = allItems.Where(item => item.StartsWith("PastOneShots") || item.StartsWith("PastCampeing")).ToList();

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

                // Устанавливаем цвет фона в зависимости от первого слова
                switch (itemText.Split(": ")[0])
                {
                    case "Plot":
                        backgroundColor = Color.FromArgb(176, 255, 157);  // Зелёный
                        break;
                    case "Character":
                        backgroundColor = Color.FromArgb(255, 150, 79);  // Оранжевый
                        break;
                    case "Location":
                        backgroundColor = Color.FromArgb(179, 158, 181); // Фиолетовый
                        break;
                    case "PastOneShots":
                        backgroundColor = Color.White; // Белый
                        break;
                    case "PastCampaign":
                        backgroundColor = Color.Silver; // Сероватый
                        break;
                    default:
                        backgroundColor = Color.White;  // Белый фон по умолчанию
                        break;
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    backgroundColor = Color.Blue;


                // Отрисовываем фон
                e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.Bounds);
                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                    // Отрисовываем текст
                    e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                else
                    e.Graphics.DrawString(itemText, e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}

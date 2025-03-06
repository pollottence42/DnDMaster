using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace DnDnotes
{
    public partial class Form1 : Form
    {
        public static string Path = @"C:\Users\Murron\Downloads\notes.txt";
        public Form1()
        {
            InitializeComponent();
            var ThisNotes = new Notes(Path);
            ListboxUpdate();
            Notes.Types.ToList().ForEach(type => comboBox1.Items.Add(type));
            comboBox1.SelectedIndex = 1;
        }

        public void ListboxUpdate()
        {
            var ThisNotes = new Notes(Path);
            listBox1.Items.Clear();
            ThisNotes.Titles.ForEach(note => listBox1.Items.Insert(0, note));
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
        }

        public void ListboxUpdate(string type)
        {
            var ThisNotes = new Notes(Path, type);
            listBox1.Items.Clear();
            ThisNotes.Titles.ForEach(note => listBox1.Items.Insert(0, note));
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnFormClosing(FormClosingEventArgs eventArgs)
        {
            var result = MessageBox.Show("Действительно закрыть??", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                eventArgs.Cancel = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = comboBox1.SelectedItem.ToString().Trim();
            var ThisNotes = new Notes(Path, item);
            ListboxUpdate(item);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var thisNotes = new Notes(Path);
            if (listBox1.SelectedItem != null)
            {
                var item = listBox1.SelectedItem.ToString().Trim();
                var note = thisNotes.Text[item];
                textBox1.Text = note.Title;
                textBox2.Text = note.Description;
                textBox3.Clear();
                foreach (var line in note.Text)
                    textBox3.Text += line + Environment.NewLine;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new EditForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            var thisNote = new Notes(Path);
            var item = listBox1.SelectedItem.ToString().Trim();
            var note = thisNote.Text[item];
            var form = new EditForm(note);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;
            var thisNote = new Notes(Path);
            var result = MessageBox.Show("Действительно удалить??", "!!Удаление!!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                thisNote.RemoveNote(textBox1.Text.Trim());
                ListboxUpdate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListboxUpdate(comboBox1.Text.Trim());
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var thisNote = new Notes(Path);
            var item = listBox1.Items[e.Index].ToString().Trim();
            var type = thisNote.Text[item].Type;
            var color = Notes.GetColor(type);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                color = Color.Blue;

            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);
            e.Graphics.DrawString(item, e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
        }
    }
}

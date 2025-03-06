using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnDnotes
{
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
            Notes.TypesForNote.ToList().ForEach(type => comboBox1.Items.Add(type));
            comboBox1.SelectedIndex = 0;
            var thisNote = new Notes(Form1.Path);
            if (thisNote.Text.ContainsKey("Название"))
            {
                for (var i = 1; i < int.MaxValue; i++)
                {
                    if (!thisNote.Text.ContainsKey("Название " + i))
                    { textBox1.Text = "Название " + i; break; }
                }
            }
            thisNote.AddNote(comboBox1.Text.ToString().Trim(), textBox1.Text, textBox2.Text, textBox3.Text.Split('\n').ToList());
        }

        public EditForm(Note note)
        {
            InitializeComponent();
            Notes.TypesForNote.ToList().ForEach(type => comboBox1.Items.Add(type));
            comboBox1.SelectedItem = note.Type;
            textBox1.Text = note.Title;
            textBox2.Text = note.Description;
            textBox3.Text = "";
            for (var i = 0; i < note.Text.Count; i++)
            {
                if (i < note.Text.Count - 1)
                    textBox3.Text += note.Text[i] + Environment.NewLine;
                else
                    textBox3.Text += note.Text[i];
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs eventArgs)
        {
            var result = MessageBox.Show("Действительно закрыть??", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                eventArgs.Cancel = true;
            var thisNote = new Notes(Form1.Path);
            thisNote.AddNote(comboBox1.Text.ToString().Trim(), textBox1.Text, textBox2.Text, textBox3.Text.Split('\n').ToList());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = comboBox1.SelectedItem.ToString().Trim();
            this.BackColor = Notes.GetColor(item);
        }
    }
}

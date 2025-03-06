using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace DnDnotes
{
    public class Notes
    {
        public string ThisPath;
        public string Name;
        public List<string> Titles;
        public Dictionary<string, Note> Text;
        public static string[] Types =
            { "All", "Active", "Plot", "Character", "Location",
              "Past", "PastOneShot", "PastCampaign" };
        public static string[] TypesForNote =
            { "Plot", "Character", "Location",
              "PastOneShot", "PastCampaign" };

        public Notes(string path)
        {
            ThisPath = path;
            Name = Path.GetFileName(ThisPath);
            var t = File.ReadAllLines(path);
            Text = new Dictionary<string, Note>();
            foreach (var line in t)
            {
                Text.Add(line.Split('_')[1].Trim(), new Note(line));
            }
            Titles = Text.Values.Select(value => value.Title).ToList();
        }

        public Notes(string path, string type)
        {
            ThisPath = path;
            Name = Path.GetFileName(ThisPath);
            var t = File.ReadAllLines(path);
            Text = new Dictionary<string, Note>();
            var temp = new Dictionary<string, Note>();

            foreach (var line in t)
            {
                temp.Add(line.Split('_')[1].Trim(), new Note(line));
            }

            switch (type)
            {
                case "All":
                    Text = temp;
                    break;
                case "Active":
                    Text = temp.Where(x => x.Value.Type != "PastCampaign" && x.Value.Type != "PastOneShot").ToDictionary(x => x.Key, x => x.Value);
                    break;
                case "Past":
                    Text = temp.Where(x => x.Value.Type == "PastCampaign" || x.Value.Type == "PastOneShot").ToDictionary(x => x.Key, x => x.Value);
                    break;
                default:
                    Text = temp.Where(x => x.Value.Type == type).ToDictionary(x => x.Key, x => x.Value);
                    break;
            }
            Titles = Text.Values.Select(value => value.Title).ToList();
        }

        public static Color GetColor(string type)
        {
            switch (type)
            {
                case "PastOneShot":
                case "PastCampaign":
                    return Color.Black;
                case "Plot":
                    return Color.Coral;
                case "Character":
                    return Color.Teal;
                case "Location":
                    return Color.MediumPurple;
                default:
                    return Color.White;
            }
        }

        public void AddNote(string type, string name, string description, List<string> text)
        {
            var note = new Note(type, name, description, text);
            if (Text.ContainsKey(name))
                Text.Remove(name);
            Text[name] = note;
            var file = File.ReadAllLines(Form1.Path).ToList();
            for (var i = 0; i < file.Count(); i++)
            {
                if (file[i].Split('_')[1] == name)
                {
                    file.Remove(file[i]);
                }
            }
            var line = type + "_" + name + "_" + description;
            foreach (var t in text)
                line += "_" + t.Trim();
            file.Add(line);
            File.WriteAllLines(Form1.Path, file);
        }

        public void RemoveNote(string name)
        {
            var note = Text[name];
            Text.Remove(name);
            var file = File.ReadAllLines(Form1.Path).ToList();
            for (var i = 0; i < file.Count(); i++)
            {
                if (file[i].Split('_')[1] == name)
                {
                    file.Remove(file[i]);
                }
            }
            File.WriteAllLines(Form1.Path, file);
        }
    }

    public class Note
    {
        public string Type;
        public string Title;
        public string Description;
        public List<string> Text;

        public Note(string text)
        {
            var full = text.Split('_');
            Type = full[0];
            Title = full[1];
            Description = full[2];
            Text = full.Skip(3).ToList();
        }

        public Note(string type, string name, string description, List<string> text)
        {
            Type = type;
            Title = name;
            Description = description;
            Text = text;
        }


    }
}

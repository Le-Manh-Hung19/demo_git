using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotePad
{
    public partial class note : Form
    {
        public note()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"D:\";
            openFile.Filter = "Text File (.txt)|*.txt|C# Code file|*.cs";
            openFile.Multiselect = false;
            openFile.ShowDialog();
            StreamReader reader = new StreamReader(openFile.FileName);
            string line;
            this.input.Text = "";
            while ((line = reader.ReadLine()) != null)
            {
                this.input.Text += line;
            }
            reader.Close();
            this.link.Text = openFile.FileName;
            string[] s = openFile.FileName.Split(@"\");
            this.Text = s[s.Length-1] + " - Notepad" ;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text.Contains("Untitled - Notepad"))
            {
                saveAsToolStripMenuItem_Click(sender,e);
            }
            else
            {
                string s = this.Text;
                StreamWriter writer = new StreamWriter(this.link.Text);
                writer.WriteLine(this.input.Text);
                writer.Close();
                string[] ss = s.Split("*");
                this.Text = ss[ss.Length - 1];
            }
        }

        private void input_TextChanged(object sender, EventArgs e)
        {
            if(!this.Text.Contains("*"))
                this.Text = "*" + this.Text;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = @"D:\";
            saveFile.Filter = "Text File (.txt)|*.txt|C# Code file|*.cs";
            saveFile.Title = "Save a text file";
            saveFile.ShowDialog();
            string s = this.Text;
            if (saveFile.FileName != "")
            {
                StreamWriter writer = new StreamWriter(saveFile.FileName);
                writer.WriteLine(this.input.Text);
                writer.Close();
                string[] ss = saveFile.FileName.Split(@"\");
                this.Text = ss[ss.Length - 1] + " - Notepad";
                this.link.Text = saveFile.FileName;
            }
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text.Contains("*"))
            {
                string s = "Do you want to save changes to ";
                if (this.link.Text.Equals(""))
                {
                    s += "Untitled";
                }
                else
                {
                    s += this.link.Text;
                }
                s += " ?";
                DialogResult r = MessageBox.Show(s, "Notepad", MessageBoxButtons.YesNoCancel);
                if(r == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    this.input.Text = "";
                    this.Text = "Untitled - Notepad";
                    this.link.Text = "";
                }
                else if (r == DialogResult.No)
                {
                    this.input.Text = "";
                    this.Text = "Untitled - Notepad";
                    this.link.Text = "";
                }
            }
            else
            {
                this.input.Text = "";
                this.Text = "Untitled - Notepad";
                this.link.Text = "";
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}

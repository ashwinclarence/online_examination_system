using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChatBot
{
    public partial class aimlwriter : Form
    {
        public aimlwriter()
        {
            InitializeComponent();
            aimlreader();
        }

        public void aimlreader()
        {
            string text;
            var fileStream = new FileStream(Application.StartupPath+"\\aiml\\test.aiml", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            richTextBox1.Text = text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string tag = "<category><pattern>" + textBox2.Text + "</pattern><template>" + textBox1.Text + "</template></category>";
            richTextBox2.Text = tag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] contents=richTextBox1.Text.Split(new string[] { "<aiml>" }, StringSplitOptions.None);
            richTextBox3.Text = contents[0].ToString() + "<aiml>" + richTextBox2.Text + contents[1].ToString();

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Application.StartupPath + "\\aiml\\test.aiml",false))
            {
                file.WriteLine(richTextBox3.Text);
            }
            aimlreader();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }
    }
}

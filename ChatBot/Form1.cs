using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using System.IO;

namespace ChatBot
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            
           
        }
        private int ComputeDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] distance = new int[n + 1, m + 1]; // matrix
            int cost = 0;
            if (n == 0) return m;
            if (m == 0) return n;
            //init1
            for (int i = 0; i <= n; distance[i, 0] = i++) ;
            for (int j = 0; j <= m; distance[0, j] = j++) ;
            //find min distance
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    cost = (t.Substring(j - 1, 1) ==
                        s.Substring(i - 1, 1) ? 0 : 1);
                    distance[i, j] = Min3(distance[i - 1, j] + 1,
                    distance[i, j - 1] + 1,
                    distance[i - 1, j - 1] + cost);
                }
            }
            return distance[n, m];
        }
        public float GetSimilarity(string string1, string string2)
        {
            float dis = ComputeDistance(string1, string2);
            float maxLen = string1.Length;
            if (maxLen < string2.Length)
                maxLen = string2.Length;
            if (maxLen == 0.0F)
                return 1.0F;
            else
                return 1.0F - dis / maxLen;
        }
        private int Min3(int a, int b, int c)
        {
            return System.Math.Min(System.Math.Min(a, b), c);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AVIWriter videoWriter = new AVIWriter();
            videoWriter.Open("C:\\cap\\test.avi", 640, 480);
          //  videoWriter.FrameRate = 1;
           // videoWriter.Open("C:\\cap\\test.wmv3", 320, 240);
            string[] files = Directory.GetFiles(Application.StartupPath + "\\anu123");
            int index = 0;
            int failed = 0;
            Bitmap image = new Bitmap(640, 480);
            foreach (var item in files)
            {
                index++;
                try
                {
                    image = Image.FromFile(item) as Bitmap;
                    //image = cubit.Apply(image);

                    for (int i = 0; i < 10; i++)
                    {
                        videoWriter.AddFrame(image);
                    }
                   
                }
                catch
                {
                    failed++;
                }
                this.Text = index + " of " + files.Length + ". Failed: " + failed;
            }
            videoWriter.Close();
            //videoWriter.AddFrame(bitmap1);
            //videoWriter.AddFrame(bitmap2);
            //videoWriter.AddFrame(bitmap3);
            //videoWriter.Close();
        }
    }
}

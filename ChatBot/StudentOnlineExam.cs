using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AForge.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;

using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace ChatBot
{
    public partial class StudentOnlineExam : Form
    {
        private Stopwatch stopWatch = null;
        public static int squest = 0;
        public static string suname, ecode, scode, tuname, tquest, etime = "";
        ArrayList scodelist = new ArrayList();
        ArrayList srlist = new ArrayList(); 
        BaseConnection con = new BaseConnection();
        BaseConnection con1 = new BaseConnection();
        string ss;
        List<string> list = new List<string>();
        string answer = "";

 
        Random rnd=new Random();
        AVIWriter videoWriter = new AVIWriter();
            
        public StudentOnlineExam()
        {
            InitializeComponent();
        }
        public StudentOnlineExam(string s1,string s2,string s3,string s4,string s5,string s6)
        {
            InitializeComponent();
             suname=s1;
            ecode= s2;
            scode=s3;
            tuname=s4;
            tquest=s5;
            etime = s6;


        }
        private void button3_Click(object sender, EventArgs e)
        {
            videoWriter.Open("C:\\cap\\test.avi", 640, 480);
            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // create video source
                VideoCaptureDevice videoSource = form.VideoDevice;

                // open it
                OpenVideoSource(videoSource);
            }
            

             string query = "select qcode from Questions where subcode='"+deuserid.Text+"'";
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {
                scodelist.Add(dr[0].ToString());
                list.Add(dr[0].ToString());
            }

            list = list.GetRandomItems(Convert.ToInt32(textBox5.Text));
            
            
           

        }
        private void OpenVideoSource(IVideoSource source)
        {
            // set busy cursor
            this.Cursor = Cursors.WaitCursor;

            // stop current video source
            CloseCurrentVideoSource();

            // start new video source
            videoSourcePlayer.VideoSource = source;
            videoSourcePlayer.Start();

            // reset stop watch
            stopWatch = null;

            // start timer
            timer.Start();

            this.Cursor = Cursors.Default;
        }
        private void CloseCurrentVideoSource()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                videoSourcePlayer.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoSourcePlayer.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (videoSourcePlayer.IsRunning)
                {
                    videoSourcePlayer.Stop();
                }

                videoSourcePlayer.VideoSource = null;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void timer_Tick_1(object sender, EventArgs e)
        {
            
            IVideoSource videoSource = videoSourcePlayer.VideoSource;

            if (videoSource != null)
            {
                // get number of frames since the last timer tick
                int framesReceived = videoSource.FramesReceived;

                if (stopWatch == null)
                {
                    stopWatch = new Stopwatch();
                    stopWatch.Start();
                }
                else
                {
                    stopWatch.Stop();

                    float fps = 1000.0f * framesReceived / stopWatch.ElapsedMilliseconds;
                    fpsLabel.Text = fps.ToString("F2") + " fps";
                   
                    string path = Application.StartupPath+"\\"+textBox3.Text+"\\";
                    if (Directory.Exists(path))
                    {

                    }
                    else
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }
                 //  videoSourcePlayer.GetCurrentVideoFrame().Save("C:\\cap\\" + fpsLabel.Text + ".jpg");
                   videoSourcePlayer.GetCurrentVideoFrame().Save(path + fpsLabel.Text + ".jpg");
                   //videoWriter.AddFrame(videoSourcePlayer.GetCurrentVideoFrame());
                   

                    stopWatch.Reset();
                    stopWatch.Start();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = "Time: " + DateTime.Now.ToShortTimeString();
        }

        private void StudentOnlineExam_Load(object sender, EventArgs e)
        {
            deuserid.Text = scode;
            textBox2.Text = ecode;
            textBox3.Text = suname;
            textBox5.Text = tquest;
            textBox6.Text = etime;
            textBox7.Text = DateTime.Now.ToShortDateString();
            label9.Text = squest.ToString();

          
           

        }
        public string Similaritycheck(string first, string second)
        {
            int count = 0;
          string[] str1 = first.Split(' ');
          string[] str2 = second.Split(' ');
          for (int i = 0; i < str1.Length; i++)
          {
              for (int j = 0; j <str2.Length;j++)            
              {
                  if (str1[i] == str2[j])
                  { 
                      count++;
                  }
              }
          }
           double per=0.0;
           if (count <= str1.Length)
           {
               per = (count * 100) / str1.Length;
           }
           else
           {
               per = 100;
           }

           return per.ToString();
       
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

        private void button4_Click(object sender, EventArgs e)
        {
           
                squest++;
                label9.Text = squest.ToString();
                string query = "select qcode,question,answer from Questions where qcode='" + list[squest-1] + "'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    textBox4.Text = dr[1].ToString();
                    answer = dr[2].ToString();
                    textBox8.Text = dr[0].ToString();
                }
                button4.Enabled = false;
                button1.Enabled = true;

            
            
        }
        public string getid()
        {
            
            
                string query = "select isnull(max(eid),100)+1 from oexam";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                   ss = dr[0].ToString();
                }
                return ss;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
           // string res = Similaritycheck(answer, textBox1.Text);
            double res = GetSimilarity(answer, textBox1.Text);
            double mark = res * 100;
            string id = getid();
            string query1 = "insert into oexam values(" + id + ",'" + textBox2.Text + "','" + deuserid.Text + "','" + textBox3.Text + "','" + tuname + "'," + label9.Text + ",'" + textBox7.Text + "','" + textBox4.Text + "','" + textBox1.Text + "','"+res+"',"+mark+",0,'" + textBox8.Text+ "')";
            con1.exec1(query1);
            squest++;
            if (squest > Convert.ToInt32(textBox5.Text))
            {
                MessageBox.Show("Exam is over Thank you");
                this.Close();
            }
            else
            {
                textBox1.Text = "";
                double etime = Convert.ToDouble(Convert.ToDateTime(textBox6.Text).TimeOfDay.TotalHours);
                double ntime = Convert.ToDouble(Convert.ToDateTime(DateTime.Now.ToShortTimeString()).TimeOfDay.TotalHours);
                if (ntime < etime)
                {
                    label9.Text = squest.ToString();
                    string query = "select qcode,question,answer from Questions where qcode='" + list[squest - 1] + "'";
                    SqlDataReader dr = con.ret_dr(query);
                    if (dr.Read())
                    {
                        textBox4.Text = dr[1].ToString();
                        answer = dr[2].ToString();
                        textBox8.Text = dr[0].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Time is over. thank you");
                    this.Close();
                }
            }
            button1.Enabled = true;
            
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
    public static class ListExtensions
    {
        private static Random rnd = new Random();

        public static List<T> GetRandomItems<T>(this IList<T> list, int maxCount)
        {
            List<T> resultList = new List<T>();
            list.Shuffle();

            for (int i = 0; i < maxCount && i < list.Count; i++)
                resultList.Add(list[i]);

            return resultList;
        }

        private static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

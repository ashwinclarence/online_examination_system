using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ChatBot
{
    public partial class TeacherUpdateResult : Form
    {
        BaseConnection con = new BaseConnection();
        public static string answer = "";
        public TeacherUpdateResult()
        {
            InitializeComponent();
        }
        public void fillgrid()
        {
            try
            {
               

                //  string query1 = "select * from Student_details where department=(select department from Teacher_details where userid='"+Program.userid+"')";
                string query1 = "select eid,ecode,scode,suname,qno,edate,question,answer,sim,mark,qcode from oexam where tuname='" + Program.username + "' and status=0";
                DataSet ds1 = con.ret_ds(query1);
                dataGridView2.DataSource = ds1.Tables[0].DefaultView;



            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillgrid(string q1)
        {
            try
            {


                //  string query1 = "select * from Student_details where department=(select department from Teacher_details where userid='"+Program.userid+"')";
                
                DataSet ds1 = con.ret_ds(q1);
                dataGridView2.DataSource = ds1.Tables[0].DefaultView;



            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TeacherUpdateResult_Load(object sender, EventArgs e)
        {
          //  Program.username = "bismi";
            fillgrid();
            string query = "select distinct edate from oexam where tuname='" + Program.username + "' and status=0";
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {
               comboBox1.Items.Add(dr[0].ToString());
            }
            string query1 = "select distinct scode from oexam where tuname='" + Program.username + "' and status=0";
            SqlDataReader dr1 = con.ret_dr(query1);
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1[0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query2 = "select eid,ecode,scode,suname,qno,edate,question,answer,sim,mark,qcode from oexam where edate='" + comboBox1.SelectedItem.ToString() + "' and status=0";
            fillgrid(query2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query3 = "select eid,ecode,scode,suname,qno,edate,question,answer,sim,mark,qcode from oexam where edate='" + comboBox1.SelectedItem.ToString() + "' and scode='"+comboBox2.SelectedItem.ToString()+"'and status=0";
            fillgrid(query3);
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

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox2.Text = row.Cells[0].Value.ToString();
                admno.Text = row.Cells[6].Value.ToString();
                textBox1.Text = row.Cells[7].Value.ToString();
                textBox6.Text = row.Cells[10].Value.ToString();
                textBox3.Text = row.Cells[9].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = 0;
            string query = "select answer from Questions where qcode='" + textBox6.Text + "'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
               
                answer = dr[0].ToString();
                
            }
            string[] str1 = textBox1.Text.Split(' ');
            for (int i = 0; i < str1.Length; i++)
            {
                count++;
            }

            textBox5.Text = count.ToString();
            double res = GetSimilarity(answer, textBox1.Text);
            textBox4.Text = res.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "update oexam set mark='" + textBox3.Text + "',status=1 where eid=" + textBox2.Text + "";
            if (con.exec1(query1) > 0)
            {
                MessageBox.Show("Student Mark details Updated Successfully...");
                this.Close();
            }
        }
    }
}

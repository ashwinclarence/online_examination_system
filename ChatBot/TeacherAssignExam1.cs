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
    public partial class TeacherAssignExam1 : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection con1 = new BaseConnection();
        string s;
        public TeacherAssignExam1()
        {
            InitializeComponent();
        }

        private void TeacherAssignExam1_Load(object sender, EventArgs e)
        {
           // Program.username = "bismi";
            mail.Text = Program.username;
            getexamcode();
            string query = "select suname from Student_details where tuname='" + Program.username + "'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    checkedListBox1.Items.Add(dr[0].ToString());
                }
                string query1 = "select distinct subcode from Questions where tuname='" + Program.username + "'";
                SqlDataReader dr1 = con1.ret_dr(query1);
                while (dr1.Read())
                {
                   comboBox1.Items.Add(dr1[0].ToString());
                }
                rad();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i;
            
           
            for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    s+= checkedListBox1.Items[i].ToString() + ":";
                }
            }
            textBox2.Text = s.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void getexamcode()
        {
            try
            {
                string query = "select isnull(max(examid),600)+1 from assignexam";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    mob.Text = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query1 = "select distinct dept from Questions where subcode='" + comboBox1.SelectedItem.ToString() + "'";
            SqlDataReader dr1 = con.ret_dr(query1);
            if (dr1.Read())
            {
                textBox1.Text = dr1[0].ToString();
               
            }
            string query2 = "select Count(*) from Questions where subcode='" + comboBox1.SelectedItem.ToString() + "'";
            SqlDataReader dr2 = con.ret_dr(query2);
            if (dr2.Read())
            {
                admno.Text = dr2[0].ToString();

            }
        }
        public void rad()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            textBox5.Text = finalString.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into assignexam values(" + mob.Text + ",'" + comboBox1.Text + "','"+mail.Text+"','" + textBox2.Text + "'," + admno.Text + ",'" + textBox3.Text + "','" + textBox4.Text + "','"+dateTimePicker1.Value.ToShortDateString()+"',0,'"+textBox5.Text+"')";
            if (con.exec1(query) > 0)
            {

                MessageBox.Show("Exam Assign details Registered Successfully...");
                this.Close();
                

            }
        }
    }
}

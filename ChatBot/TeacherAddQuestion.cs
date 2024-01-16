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
    public partial class TeacherAddQuestion : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection con1 = new BaseConnection();
        public TeacherAddQuestion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into Questions values(" + mob.Text + ",'" + comboBox2.Text + "','" + admno.Text + "','" + textBox2.Text + "','" + mail.Text + "','" + pwd.Text + "','"+textBox3.Text+"','"+textBox1.Text+"',0)";
            if (con.exec1(query) > 0)
            {
               
                    MessageBox.Show("Student details Registered Successfully...");
                    this.Close();
               

            }
        }
        public void getqcode()
        {
            try
            {
                string query = "select isnull(max(qcode),500)+1 from Questions where status=0";
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string text = System.IO.File.ReadAllText(ofd.FileName);
                    textBox4.Text = ofd.FileName;
                    textBox1.Text = text;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while choosing cover pic.......");
            }
        }
        public void addquestion()
        {
            try
            {
                string query = "select department,subcode from Teacher_Details where username='" + Program.username + "'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    comboBox2.Text = dr[1].ToString();
                    admno.Text = dr[0].ToString();

                }
                string query1 = "select semester from subject_details where subcode='" + comboBox2.Text + "'";
                SqlDataReader dr1 = con1.ret_dr(query1);
                if (dr1.Read())
                {
                    textBox2.Text = dr1[0].ToString();
                    
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating subject Id........");
            }
        }
        private void TeacherAddQuestion_Load(object sender, EventArgs e)
        {
            addquestion();
            getqcode();
            pwd.Text = DateTime.Now.ToShortDateString();
           // Program.username = "bismi";
            mail.Text = Program.username;

        }

        private void admno_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

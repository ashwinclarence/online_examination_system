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
    public partial class StudentViewMark : Form
    {
        BaseConnection con = new BaseConnection();
        public StudentViewMark()
        {
            InitializeComponent();
        }

        public void fillgrid()
        {
            try
            {


                //  string query1 = "select * from Student_details where department=(select department from Teacher_details where userid='"+Program.userid+"')";
                string query1 = "select eid,ecode,scode,qno,edate,question,answer,mark,qcode from oexam where suname='" + Program.username + "' and status=1";
                DataSet ds1 = con.ret_ds(query1);
                dataGridView2.DataSource = ds1.Tables[0].DefaultView;



            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
                    }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
                    }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StudentViewMark_Load(object sender, EventArgs e)
        {
          //  Program.username = "anu123";
            fillgrid();
            string query1 = "select distinct scode from oexam where suname='" + Program.username + "' and status=1";
            SqlDataReader dr1 = con.ret_dr(query1);
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1[0].ToString());
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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            double m = 0;
            int c = 0;
            double tm = 0;
            string query1 = "select eid,ecode,scode,qno,edate,question,answer,mark,qcode from oexam where suname='" + Program.username + "' and ecode='"+admno.Text+"' and scode='"+comboBox2.SelectedItem.ToString()+"' and status=1";
            fillgrid(query1);
            string query2 = "select count(*),sum(mark) from oexam where suname='" + Program.username + "' and ecode='" + admno.Text + "' and scode='" + comboBox2.SelectedItem.ToString() + "' and status=1 ";
            SqlDataReader dr1 = con.ret_dr(query2);
            while (dr1.Read())
            {
                c=Convert.ToInt32(dr1[0].ToString());
                m = Convert.ToDouble(dr1[1].ToString());

            }
            tm = (m / c) ;
            textBox2.Text = tm.ToString();
        }
    }
}

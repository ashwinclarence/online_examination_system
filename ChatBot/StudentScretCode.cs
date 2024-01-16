using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace ChatBot
{
    public partial class StudentScretCode : Form
    {
        BaseConnection con = new BaseConnection();
        ArrayList arslist = new ArrayList(); 
        public StudentScretCode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tdate = DateTime.Now.ToShortDateString();
            double ttime = Convert.ToDouble( DateTime.Now.TimeOfDay.TotalHours);
            string query = "select * from assignexam where scode='"+usert.Text+"'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                string ddate = dr[7].ToString();
                double dtime = Convert.ToDouble(Convert.ToDateTime(dr[5].ToString()).TimeOfDay.TotalHours);
                double etime = Convert.ToDouble(Convert.ToDateTime(dr[6].ToString()).TimeOfDay.TotalHours);
                if (ddate == tdate)
                {
                    if (ttime >= dtime && ttime < etime)
                    {

                        string str = dr[3].ToString();
                        string[] suname = str.Split(':');
                        
                        for (int i = 0; i < suname.Count() - 1; i++)
                        {
                            arslist.Add(suname[i].ToString());
                            
                        }

                        if (arslist.Contains(Program.username))
                        {
                            StudentOnlineExam obj = new StudentOnlineExam(Program.username,dr[0].ToString(),dr[1].ToString(),dr[2].ToString(),dr[4].ToString(),dr[6].ToString());
                            ActiveForm.Hide();
                            obj.Show();

                        }
                        else
                        {
                            MessageBox.Show("This Student is not  Allowed for Exam");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Exam on Allowed Time");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Exam on Allowed Date");
                }
            }
            else
            {

                MessageBox.Show("Invalid Exam Code");
            }

        }

        private void StudentScretCode_Load(object sender, EventArgs e)
        {
            //Program.username = "anu123";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

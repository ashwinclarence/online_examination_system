using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ChatBot
{
    public partial class Student_Registration : Form
    {
        BaseConnection con = new BaseConnection();

        public static string uid = "";
        public static string sid = "";
        public static string coverpath = "";
        public static string thumbpath = "";


        public Student_Registration()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = ofd.Filter = "Jpeg Images(*.jpg)|*.jpg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    profilepic.ImageLocation = ofd.FileName;
                    profilepic.BackgroundImageLayout = ImageLayout.Stretch;
                    pic.Text = ofd.FileName;
                    coverpath = ofd.FileName;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while choosing cover pic.......");
            }
        }

        public static Image RoundCorners(Image StartImage, int CornerRadius, Color BackgroundColor)
        {
           
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(BackgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(StartImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            g.FillPath(brush, gp);
            return RoundedImage;
            
        }

        public void user()
        {
            try
            {
                string query = "select isnull(max(Userid),100)+1 from login";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    uid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }

        public void studentid()
        {
            try
            {
                string query = "select isnull(max(studentid),100)+1 from Student_details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    sid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                studentid();
                user();

                coverpath = Application.StartupPath + "\\ProfilePictures\\" + uid + ".jpg";
                System.IO.File.Copy(profilepic.ImageLocation.ToString(), coverpath);
                coverpath = "\\ProfilePictures\\" + uid + ".jpg";

                Image image = Image.FromFile(profilepic.ImageLocation);
                Image thumbnail = image.GetThumbnailImage(60, 60, () => false, IntPtr.Zero);
                Image RoundedImage = RoundCorners(thumbnail, 20, Color.Transparent);

                thumbpath = Application.StartupPath + "\\ProfilePictures\\Thumbnails\\" + uid + ".jpg";
                RoundedImage.Save(thumbpath);
                thumbpath = "\\ProfilePictures\\Thumbnails\\" + uid + ".jpg";


                string utype = "0";
                string query = "insert into login values(" + uid + ",'" + usert.Text + "','" + pwd.Text + "','" + utype + "','" + coverpath + "','" + thumbpath + "')";
                if (con.exec1(query) > 0)
                {
                    string query1 = "insert into Student_details values(" + sid + "," + uid + ",'" + admno.Text + "','" + name.Text + "','" + dept.Text + "','" + mob.Text + "','" + mail.Text + "','"+comboBox1.SelectedItem.ToString()+"','"+textBox1.Text+"',0,'"+usert.Text+"')";
                    if (con.exec1(query1) > 0)
                    {
                        MessageBox.Show("Student details Registered Successfully...");
                        this.Close();
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured....");

            }
        }

        private void mob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                if (e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar == '.') || (e.KeyChar == '-') || (e.KeyChar == ' '))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public void addteacher()
        {
            try
            {
                string query = "select username from Teacher_Details where department='"+dept.SelectedItem.ToString()+"'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating subject Id........");
            }
        }
        public void adddepartment()
        {
            try
            {
                string query = "select distinct dept from Subject_Details";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    dept.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating subject Id........");
            }
        }
        private void Student_Registration_Load(object sender, EventArgs e)
        {

            adddepartment();
        }

        private void dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            addteacher();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

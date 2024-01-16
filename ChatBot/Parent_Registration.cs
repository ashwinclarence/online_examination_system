using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ChatBot
{
    public partial class Parent_Registration : Form
    {
        BaseConnection con = new BaseConnection();

        public static string uid = "";
        public static string tid = "";
         public static string sid = "";
         public static string suid = "";
        
        public static string coverpath = "";
        public static string thumbpath = "";


        public Parent_Registration()
        {
            InitializeComponent();
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

        public void parentid()
        {
            try
            {
                string query = "select isnull(max(Parentid),100)+1 from Parent_details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    tid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating Parent Id........");
            }
        }

        public void studentid(string admno)
        {
            try
            {
                string query = "select * from Student_details where admno='"+admno+"'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    sid = dr[0].ToString();
                    suid = dr[1].ToString();
                    sname.Text = dr[3].ToString();
                    sdept.Text = dr[4].ToString();
                    string q = "select Profilepic from login where userid='" + suid + "'";
                    SqlDataReader dr1 = con.ret_dr(q);
                    if (dr1.Read())
                    {
                        childpic.ImageLocation = Application.StartupPath + dr1[0].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Admno ..If your child has not registered into this system ,Please register your Child's details first...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating Student Id........");
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            studentid(admno.Text);
        }

        private void Parent_Registration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                parentid();
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


                string utype = "Parent";
                string query = "insert into login values(" + uid + ",'" + usert.Text + "','" + pwd.Text + "','" + utype + "','" + coverpath + "','" + thumbpath + "')";
                if (con.exec1(query) > 0)
                {
                    string query1 = "insert into Parent_details values("+tid+","+sid+","+uid+",'"+textBox1.Text+"','"+mob.Text+"','"+mail.Text+"')";

                    if (con.exec1(query1) > 0)
                    {
                        MessageBox.Show("Parent details Registered Successfully...");
                        this.Close();
                    }

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured....");

            }
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

        private void mob_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                if (e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }
    }
}

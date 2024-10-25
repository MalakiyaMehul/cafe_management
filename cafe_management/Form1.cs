using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data;
using System.Data.SqlClient;
//using System;
using System.IO;

namespace cafe_management
{
    public partial class Form1 : Form
    {
        SqlConnection Con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mehul\source\repos\cafe_management\cafe_management\cafedbmdf.mdf;Integrated Security=True";
        void connection()
        {
            Con = new SqlConnection(s);
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GuestOrder guest = new GuestOrder();
            guest.Show();
        }
        public static string user;
        private void Login_Click(object sender, EventArgs e)
        {
            connection();
            user = UnameTB.Text;
            if (UnameTB.Text==""||Password.Text=="")
            {
                MessageBox.Show("Enter a Username  Or  Password");
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from Login where  Uname='"+UnameTB.Text+"'and Upassword='"+Password.Text+"'",Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString()=="1")
                {

                    UserOrder uorder = new UserOrder();
                    uorder.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username and Password");
                }
            }
        }
    }
}

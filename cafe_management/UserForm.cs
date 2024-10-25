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
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;
namespace cafe_management
{
    public partial class UserForm : Form
    {
        SqlConnection Con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";

        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mehul\source\repos\cafe_management\cafe_management\cafedbmdf.mdf;Integrated Security=True";
        void populate()
        {
            connection();
            //Con.Open();
            String Query = "select * from User_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            //Con.Close();
        }
        void connection()
        {
            Con = new SqlConnection(s);
        }
        public UserForm()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection();
            Con.Open();
            cmd = new SqlCommand("insert into User_tbl (Uname,Uphone,Upassword)values('" + UnameTb.Text + "','" + UphoneTb.Text + "','" + UpassTb.Text + "')", Con);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("insert into Login (Uname,Upassword)values('" + UnameTb.Text + "','" + UpassTb.Text + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Successfully created");
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ItemForm item = new ItemForm();
            item.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserOrder Uorder = new UserOrder();
            Uorder.Show();
            this.Hide();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            populate();
        }
        int key = 0;

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            UnameTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            UphoneTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
            UpassTb.Text = UsersGV.SelectedRows[0].Cells[3].Value.ToString();

            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {   
                key = Convert.ToInt32(UsersGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (UphoneTb.Text == "")
            {
                MessageBox.Show("Selecet The User to be ");
                MessageBox.Show("Selecet The User to be Deleted");
            }
            else
            {
                connection();
                Con.Open();
                string Query = "delete from User_tbl where Uphone='" + UphoneTb.Text + "'";
                string Query2 = "delete from Login where Uphone='" + UphoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand(Query, Con);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted ");
                Con.Close();
                populate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (UphoneTb.Text == "" || UnameTb.Text == "" || UpassTb.Text == "")
            {
                MessageBox.Show("Fill all the Field");
            }
            else
            {
                connection();
                Con.Open();
                //string Query
                SqlCommand cmd = new SqlCommand("Update User_tbl set Uname = '" + UnameTb.Text + "', Uphone='" + UphoneTb.Text + "', Upassword='" + UpassTb.Text + "' where Uid = '"+key+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User successfully Update");
                Con.Close();
                populate();
            }
        }

        private void UsersGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            connection();
            da = new SqlDataAdapter("select * from User_tbl ",Con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"C:/Users/mehul/source/repos/cafe_management/cafe_management/User_Rep.xml";
            ds.WriteXmlSchema(xml);


            crystalReportViewer2.Visible = true;

            Crypath = @"C:/Users/mehul/source/repos/cafe_management/cafe_management/Order_Rep.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            crystalReportViewer2.ReportSource = cr;
        }
    }
}

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
    public partial class UserOrder : Form
    {
        SqlConnection Con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";

        string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mehul\source\repos\cafe_management\cafe_management\cafedbmdf.mdf;Integrated Security=True";
        void connection()
        {
            Con = new SqlConnection(s);
        }
        public UserOrder()
        {
            InitializeComponent();
        }
        int num = 0;
        int price, qty, total;
        string item,cat;
        void populate()
        {
            connection();
            Con.Open();
            String Query = "select * from Item_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        void filterbycat()
        {
            connection();
            Con.Open();
            String Query = "select * from Item_tbl where Itemcat='"+catedorycb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int flag = 0;
        int sum = 0;
        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Num",typeof(int));
            table.Columns.Add("Item",typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Unit Price", typeof(int));
            table.Columns.Add("Total", typeof(int));
            OrderGV.DataSource = table;
            flag = 1;
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            User_guest.Text = Form1.user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(OtyTB.Text == " ")
            {
                MessageBox.Show("what is the Quntity of item?");
            }
           
            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(OtyTB.Text);
                table.Rows.Add(num,item,cat,price,total);
                OrderGV.DataSource = table;
                flag = 0;
            }
            sum = sum + total;
            OrderAmt.Text = " " + sum;
        }
        DataTable table = new DataTable();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterbycat();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection();
            Con.Open();
            cmd = new SqlCommand("insert into Orders_tbl values('" + OrderNum.Text + "','" + Datelbl.Text + "','" + User_guest.Text + "','" + OrderAmt.Text + "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order successfully Created");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            connection();
            da = new SqlDataAdapter("select * from Orders_tbl", Con);
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"C:/Users/mehul/source/repos/cafe_management/cafe_management/Order.xml";
            ds.WriteXmlSchema(xml);

            crystalReportViewer1.Visible = true;

            Crypath = @"C:/Users/mehul/source/repos/cafe_management/cafe_management/Order_Rep.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            crystalReportViewer1.ReportSource = cr;


        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Name = (ItemsGV.Rows[e.RowIndex].Cells[1].Value).ToString();
            cat= (ItemsGV.Rows[e.RowIndex].Cells[2].Value).ToString();
            price = Convert.ToInt32(ItemsGV.Rows[e.RowIndex].Cells[3].Value);
            flag = 1;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ItemForm item = new ItemForm();
            item.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.Show();
            this.Hide();
        }
    }
}

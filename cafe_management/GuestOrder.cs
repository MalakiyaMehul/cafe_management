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
    public partial class GuestOrder : Form
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
        public GuestOrder()
        {
            InitializeComponent();
        }
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
            String Query = "select * from Item_tbl where Itemcat='" + categorycb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int flag = 0;
        int sum = 0;

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
        int num = 0;
        int price, qty, total;
        string item, cat;

        private void button2_Click(object sender, EventArgs e)
        {
            if (OtyTB.Text == " ")
            {
                MessageBox.Show("what is the Quntity of item?");
            }

            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(OtyTB.Text);
                table.Rows.Add(num, item, cat, price, total);
                OrderGV.DataSource = table;
                flag = 0;
            }
            sum = sum + total;
            OrderAmt.Text = " " + sum;
        }
        DataTable table = new DataTable();

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            Name = (ItemsGV.Rows[e.RowIndex].Cells[1].Value).ToString();
            cat = (ItemsGV.Rows[e.RowIndex].Cells[2].Value).ToString();
            price = Convert.ToInt32(ItemsGV.Rows[e.RowIndex].Cells[3].Value);
            flag = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection();
            Con.Open();
            cmd = new SqlCommand("insert into Orders_tbl values('" + OrderNum.Text + "','" + Datelbl.Text + "','" + User_gues.Text + "','"+ OrderAmt.Text+ "')", Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order successfully Created");
            
            //populate();
        }

        private void OrderGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Datelbl_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GuestOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Unit Price", typeof(int));
            table.Columns.Add("Total", typeof(int));
            OrderGV.DataSource = table;
            flag = 1;
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void categorycb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterbycat();
        }
    }
}

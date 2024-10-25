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
    public partial class ItemForm : Form
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
        public ItemForm()
        {
            InitializeComponent();
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

        private void button4_Click(object sender, EventArgs e)
        {
            UserOrder order = new UserOrder();
            order.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.Show();
            this.Hide();

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
        private void button9_Click(object sender, EventArgs e)
        {
            if (ItemNameTB.Text == "" || ItemNum.Text == "" || PriceCB.Text == "")
            {
                MessageBox.Show("Fill All the data");
            }
            else
            {
                connection();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into Item_tbl (ItemNum,ItemName,Itemcat,ItemPrice)values('" + ItemNum.Text + "','" + ItemNameTB.Text + "','" + CatCB.Text + "','" + PriceCB.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully created");
                populate();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ItemNum.Text == ""|| ItemNameTB.Text == ""|| PriceCB.Text == "")
            {
                MessageBox.Show("Selecet The User to be ");
                MessageBox.Show("Selecet The User to be Deleted");
            }
            else
            {
                connection();
                Con.Open();
                string Query = "delete from Item_tbl where ItemNum='" + ItemNum.Text + "'";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted ");
                Con.Close();
                populate();
            }
        }

        private void ItemNum_TextChanged(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ItemNum.Text = (ItemsGV.Rows[e.RowIndex].Cells["ItemNum"].Value).ToString();
            ItemNameTB.Text = (ItemsGV.Rows[e.RowIndex].Cells["ItemName"].Value).ToString();
            CatCB.Text = (ItemsGV.Rows[e.RowIndex].Cells["ItemCat"].Value).ToString();
            PriceCB.Text = (ItemsGV.Rows[e.RowIndex].Cells["ItemPrice"].Value).ToString();
        }
    }
}

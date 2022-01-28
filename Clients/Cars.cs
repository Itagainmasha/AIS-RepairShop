using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Уверены, что хотите закрыть список машин?", "Уточнение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Program.Context.MainForm.Hide();
                Program.Context.MainForm = new MainForm();
                Program.Context.MainForm.Show();
            }
        }
        private void Cars_Shown(object sender, EventArgs e)
        {
            try
            {
                label1.Text = Bank.ClientSurname + " " + Bank.ClientName + " " + Bank.ClientPatronymic;
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CarID, CarTitle as 'Марка', GosNumber as 'Гос. номер', VIN, BrandTitle as 'Бренд' FROM Cars, BrandCars WHERE Cars.BrandID=BrandCars.BrandID and ClientID = " + Bank.ClientID + "";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите удалить данную запись?", "Уточнение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                        Bank.con.Open();
                        SqlCommand cmd = Bank.con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM Cars WHERE CarID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT CarID, CarTitle as 'Марка', GosNumber as 'Гос. номер', VIN, BrandTitle as 'Бренд' FROM Cars, BrandCars WHERE Cars.BrandID=BrandCars.BrandID and ClientID = " + Bank.ClientID + "";
                        cmd.ExecuteNonQuery();
                        Bank.con.Close();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        MessageBox.Show("Данные удалены!", "Ура", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.Columns[0].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Выберите запись!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm.Hide();
            Program.Context.MainForm = new AddCars();
            Program.Context.MainForm.Show();
        }
    }
}
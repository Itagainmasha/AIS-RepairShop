using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class Clients : Form
    {
        public static Clients SelfRef { get; set; }
        public Clients()
        {
            SelfRef = this;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Context.MainForm.Hide();
                Program.Context.MainForm = new AddClients();
                Program.Context.MainForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Clients_Shown(object sender, EventArgs e)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ClientID, ClientSurname as 'Фамилия', ClientName as 'Имя', ClientPatronymic as 'Отчество', ClientPhone as 'Номер телефона', ClientEmail as 'Почта' FROM Clients";
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
        private void button2_Click(object sender, EventArgs e)
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
                        cmd.CommandText = "DELETE FROM Clients WHERE ClientID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT ClientID, ClientSurname as 'Фамилия', ClientName as 'Имя', ClientPatronymic as 'Отчество', ClientPhone as 'Номер телефона', ClientEmail as 'Почта' FROM Clients";
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
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    Bank.ClientID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    Bank.ClientSurname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                    Bank.ClientName = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
                    Bank.ClientPatronymic = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                    Bank.ClientPhone = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
                    Bank.ClientEmail = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
                    Program.Context.MainForm.Hide();
                    Program.Context.MainForm = new ChangeClients();
                    Program.Context.MainForm.Show();
                }
                else
                {
                    MessageBox.Show("Выберите запись!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    Bank.ClientID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    Bank.ClientSurname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                    Bank.ClientName = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
                    Bank.ClientPatronymic = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                    Program.Context.MainForm.Hide();
                    Program.Context.MainForm = new Cars();
                    Program.Context.MainForm.Show();
                }
                else
                {
                    MessageBox.Show("Выберите запись!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
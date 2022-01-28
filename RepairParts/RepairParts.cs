using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RepairShop
{
    public partial class RepairParts : Form
    {
        public RepairParts()
        {
            InitializeComponent();
        }
        private void RepairParts_Shown(object sender, EventArgs e)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RepairPartID, RepairPartTitle as 'Деталь', SerialNumber as 'Серийный номер', RepairPartPrice as 'Цена', RepairPartDescription as 'Описание', CountOnStore as 'Количество на складе' FROM RepairParts";
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (numericUpDown1.Value != 0)
                    {
                        int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                        Bank.con.Open();
                        SqlCommand cmd = Bank.con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE RepairParts SET CountOnStore = " + numericUpDown1.Value + "  WHERE RepairPartID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT RepairPartID, RepairPartTitle as 'Деталь', SerialNumber as 'Серийный номер', RepairPartPrice as 'Цена', RepairPartDescription as 'Описание', CountOnStore as 'Количество на складе' FROM RepairParts";
                        cmd.ExecuteNonQuery();
                        Bank.con.Close();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Visible = false;
                        numericUpDown1.Value = 0;
                    }
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (numericUpDown2.Value != 0)
                    {
                        int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                        Bank.con.Open();
                        SqlCommand cmd = Bank.con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT CountOnStore FROM RepairParts WHERE RepairPartID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        int price = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        if (price < numericUpDown2.Value)
                        {
                            MessageBox.Show($"На складе не хватает деталей!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE RepairParts SET CountOnStore = CountOnStore - " + numericUpDown2.Value + " WHERE RepairPartID = " + ID + "";
                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT RepairPartID, RepairPartTitle as 'Деталь', SerialNumber as 'Серийный номер', RepairPartPrice as 'Цена', RepairPartDescription as 'Описание', CountOnStore as 'Количество на складе' FROM RepairParts";
                        cmd.ExecuteNonQuery();
                        Bank.con.Close();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Visible = false;
                        numericUpDown2.Value = 0;
                    }
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
                        cmd.CommandText = "DELETE FROM RepairParts WHERE RepairPartID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT RepairPartID, RepairPartTitle as 'Деталь', SerialNumber as 'Серийный номер', RepairPartPrice as 'Цена', RepairPartDescription as 'Описание', CountOnStore as 'Количество на складе' FROM RepairParts";
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Context.MainForm.Hide();
                Program.Context.MainForm = new AddRepairPart();
                Program.Context.MainForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

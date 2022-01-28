using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepairShop
{
    public partial class AddOrder : Form
    {
        public AddOrder()
        {
            InitializeComponent();
        }
        public int chooseClient;
        public List<string> getCars()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Cars WHERE ClientID = " + chooseClient + "";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    date.Add(item[1].ToString());
                }
                return date;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public int getIdByCars(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CarID FROM Cars where CarTitle ='" + name + "'";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
        }
        public List<string> getClients()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Clients";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    date.Add(item[1].ToString() + " " + item[2].ToString() + " " + item[3].ToString());
                }
                return date;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public int getIdByClients(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ClientID FROM Clients where (ClientSurname ='" + name.Split(' ')[0] + "' and ClientName ='" + name.Split(' ')[1] + "' and ClientPatronymic ='" + name.Split(' ')[2] + "')";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
        }
        public List<string> getEmployee()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Employee";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    date.Add(item[1].ToString() + " " + item[2].ToString() + " " + item[3].ToString());
                }
                return date;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public int getIdByEmployee(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT EmployeeID FROM Employee where (EmployeeSurname ='" + name.Split(' ')[0] + "' and EmployeeName ='" + name.Split(' ')[1] + "' and EmployeePatronymic ='" + name.Split(' ')[2] + "')";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите закрыть окно регистрации новой заявки?", "Уточнение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Program.Context.MainForm.Hide();
                    Program.Context.MainForm = new MainForm();
                    Program.Context.MainForm.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void AddOrder_Shown(object sender, EventArgs e)
        {
            try
            {
                string[] array1 = getClients().Select(n => n.ToString()).ToArray();
                comboBox1.Items.AddRange(array1);
                string[] array3 = getEmployee().Select(n => n.ToString()).ToArray();
                comboBox3.Items.AddRange(array3);
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                chooseClient = getIdByClients(comboBox1.SelectedItem.ToString());
                string[] array2 = getCars().Select(n => n.ToString()).ToArray();
                comboBox2.Items.AddRange(array2);
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
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите зарегистировать новую заявку?", "Уточнение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1)
                    {
                        if (textBox1.Text.Length < 50)
                        {
                            int idClient = getIdByClients(comboBox1.SelectedItem.ToString());
                            int idCar = getIdByCars(comboBox2.SelectedItem.ToString());
                            int idEmployee = getIdByEmployee(comboBox3.SelectedItem.ToString());
                            DateTime today = DateTime.Now.Date;
                            Bank.con.Open();
                            SqlCommand cmd = Bank.con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO RegRepair (RegTitle, RegDate, CarID, ClientID, EmployeeID) VALUES ('" + textBox1.Text + "', '" + today + "', " + idCar + ", " + idClient + ", " + idEmployee + ")";
                            cmd.ExecuteNonQuery();
                            Bank.con.Close();
                            MessageBox.Show("Данные добавлены!", "Ура", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.Context.MainForm.Hide();
                            Program.Context.MainForm = new MainForm();
                            Program.Context.MainForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Длина полей не должна превышать 50 символов!", "Упс-с", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля!", "Упс-с", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class ChangeEmployee : Form
    {
        public ChangeEmployee()
        {
            InitializeComponent();
        }
        public List<string> getDepartment()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Departments";
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
        public int getIdByDepartment(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT DepartmentID FROM Departments where DepartmentTitle ='" + name + "'";
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
        public List<string> getPosition()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Position";
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
        public int getIdByPosition(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PositionID FROM Position where PositionTitle ='" + name + "'";
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
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите закрыть окно изменения сотрудника?", "Уточнение", MessageBoxButtons.YesNo);
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
        private void ChangeEmployee_Shown(object sender, EventArgs e)
        {
            try
            {
                string[] array1 = getDepartment().Select(n => n.ToString()).ToArray();
                comboBox1.Items.AddRange(array1);
                string[] array2 = getPosition().Select(n => n.ToString()).ToArray();
                comboBox2.Items.AddRange(array2);
                textBox1.Text = Bank.EmployeeSurname;
                textBox2.Text = Bank.EmployeeName;
                textBox3.Text = Bank.EmployeePatronymic;
                comboBox1.SelectedItem = Bank.Department;
                comboBox2.SelectedItem = Bank.Position;
                textBox4.Text = Bank.EmployeeDescription;
                textBox5.Text = Bank.EmployeeLogin;
                textBox6.Text = Bank.EmployeePassword;
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
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите изменить данные сотрудника?", "Уточнение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
                    {
                        if (textBox1.Text.Length < 50 && textBox2.Text.Length < 50 && textBox3.Text.Length < 50 && textBox4.Text.Length < 50 && textBox5.Text.Length < 50 && textBox6.Text.Length < 50)
                        {
                            int idDepartment = getIdByDepartment(comboBox1.SelectedItem.ToString());
                            int idPosition = getIdByPosition(comboBox2.SelectedItem.ToString());
                            Bank.con.Open();
                            SqlCommand cmd = Bank.con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE Employee SET EmployeeSurname = '" + textBox1.Text + "', EmployeeName = '" + textBox2.Text + "', EmployeePatronymic = '" + textBox3.Text + "', DepartmentID = " + idDepartment + ", PositionID = " + idPosition + ", EmployeeDescription = '" + textBox4.Text + "', EmployeeLogin = '" + textBox5.Text + "', EmployeePassword = '" + textBox6.Text + "' WHERE EmployeeID = " + Bank.EmployeeID + "";
                            cmd.ExecuteNonQuery();
                            Bank.con.Close();
                            MessageBox.Show("Данные изменены!", "Ура", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }
    }
}
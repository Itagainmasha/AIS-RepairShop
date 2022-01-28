using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class Employee : Form
    {
        public static Employee SelfRef { get; set; }
        public Employee()
        {
            SelfRef = this;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Context.MainForm.Hide();
                Program.Context.MainForm = new AddEmployee();
                Program.Context.MainForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Employee_Shown(object sender, EventArgs e)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT EmployeeID, EmployeeSurname as 'Фамилия', EmployeeName as 'Имя', EmployeePatronymic as 'Отчество', DepartmentTitle as 'Отдел', PositionTitle as 'Должность', EmployeeDescription as 'Описание', EmployeeLogin as 'Логин', EmployeePassword as 'Пароль' FROM Employee, Position, Departments WHERE Employee.DepartmentID=Departments.DepartmentID and Employee.PositionID=Position.PositionID";
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
                        cmd.CommandText = "DELETE FROM Employee WHERE EmployeeID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT EmployeeID, EmployeeSurname as 'Фамилия', EmployeeName as 'Имя', EmployeePatronymic as 'Отчество', DepartmentTitle as 'Отдел', PositionTitle as 'Должность', EmployeeDescription as 'Описание', EmployeeLogin as 'Логин', EmployeePassword as 'Пароль' FROM Employee, Position, Departments WHERE Employee.DepartmentID=Departments.DepartmentID and Employee.PositionID=Position.PositionID";
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
                    Bank.EmployeeID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    Bank.EmployeeSurname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                    Bank.EmployeeName = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
                    Bank.EmployeePatronymic = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                    Bank.Department = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
                    Bank.Position = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
                    Bank.EmployeeDescription = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString();
                    Bank.EmployeeLogin = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString();
                    Bank.EmployeePassword = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString();
                    Program.Context.MainForm.Hide();
                    Program.Context.MainForm = new ChangeEmployee();
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
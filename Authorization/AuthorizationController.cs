using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RepairShop
{
    public class AuthorizationController
    {
        IAuthorization authorizationView;
        public AuthorizationController(IAuthorization view)
        {
            authorizationView = view;
        }
        public void Authorization()
        {
            AuthorizationModel authorization = new AuthorizationModel();
            authorization.Login = authorizationView.LoginText;
            authorization.Password = authorizationView.PasswordText;
            if (authorization.Login != "" && authorization.Password != "")
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT count(*) FROM Employee Where EmployeeLogin = '" + authorization.Login + "' and EmployeePassword = '" + authorization.Password + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int a = Convert.ToInt32(reader[0]);
                Bank.con.Close();
                if (a == 1)
                {
                    Program.Context.MainForm.Hide();
                    Program.Context.MainForm = new MainForm();
                    Program.Context.MainForm.Show();
                }
                else
                {
                    MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
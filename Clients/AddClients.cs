using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class AddClients : Form
    {
        public AddClients()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите добавить нового клиента?", "Уточнение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                    {
                        if (textBox1.Text.Length < 50 && textBox2.Text.Length < 50 && textBox3.Text.Length < 50 && textBox4.Text.Length < 50 && textBox5.Text.Length < 50)
                        {
                            Bank.con.Open();
                            SqlCommand cmd = Bank.con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "INSERT INTO Clients (ClientSurname, ClientName, ClientPatronymic, ClientPhone, ClientEmail) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "')";
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
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Уверены, что хотите закрыть окно добавления нового клиента?", "Уточнение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Program.Context.MainForm.Hide();
                Program.Context.MainForm = new MainForm();
                Program.Context.MainForm.Show();
            }
        }
    }
}
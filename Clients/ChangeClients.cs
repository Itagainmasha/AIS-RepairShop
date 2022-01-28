using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RepairShop
{
    public partial class ChangeClients : Form
    {
        public ChangeClients()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите закрыть окно изменения клиента?", "Уточнение", MessageBoxButtons.YesNo);
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
        private void ChangeClients_Shown(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Bank.ClientSurname;
                textBox2.Text = Bank.ClientName;
                textBox3.Text = Bank.ClientPatronymic;
                textBox4.Text = Bank.ClientPhone;
                textBox5.Text = Bank.ClientEmail;
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
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите изменить данные клиента?", "Уточнение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                    {
                        if (textBox1.Text.Length < 50 && textBox2.Text.Length < 50 && textBox3.Text.Length < 50 && textBox4.Text.Length < 50 && textBox5.Text.Length < 50)
                        {
                            Bank.con.Open();
                            SqlCommand cmd = Bank.con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "UPDATE Clients SET ClientSurname = '" + textBox1.Text + "', ClientName = '" + textBox2.Text + "', ClientPatronymic = '" + textBox3.Text + "', ClientPhone = '" + textBox4.Text + "', ClientEmail = '" + textBox5.Text + "' WHERE ClientID = " + Bank.ClientID + "";
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsDigit(e.KeyChar)) return;
            e.Handled = true;
        }
    }
}
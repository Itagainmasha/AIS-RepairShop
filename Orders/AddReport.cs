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
    public partial class AddReport : Form
    {
        public AddReport()
        {
            InitializeComponent();
        }
        public List<string> getRepairParts()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM RepairParts WHERE CountOnStore > 0";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    date.Add(item[1].ToString() + ":" + item[3].ToString());
                }
                return date;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public int getIdByRepairParts(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RepairPartID FROM RepairParts where (RepairPartTitle ='" + name.Split(':')[0] + "' and RepairPartPrice = " + Convert.ToInt32(name.Split(':')[1]);
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
        public List<string> getCarService()
        {
            try
            {
                List<string> date = new List<string>();
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM CarService";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    date.Add(item[2].ToString() + ":" + item[3].ToString());
                }
                return date;
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public int getIdByCarService(string name)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ServiceID FROM CarService where (ServiceTitle ='" + name.Split(':')[0] + "' and ServicePrice = " + Convert.ToInt32(name.Split(':')[1]);
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
        private void button3_Click(object sender, EventArgs e)
        {
            double SumRepairParts = CalculatingRepairParts();
            double SumService = CalculatingService();
            double totalPrice = SumRepairParts + SumService;
            DateTime today = DateTime.Now.Date;
            string[] dateCh = dateTimePicker1.Value.ToShortDateString().Split('.');
            Bank.con.Open();
            SqlCommand cmd = Bank.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT CarID FROM RegRepair WHERE RegID = " + Bank.RegID + "";
            cmd.ExecuteNonQuery();
            int CarID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ClientID FROM RegRepair WHERE RegID = " + Bank.RegID + "";
            cmd.ExecuteNonQuery();
            int ClientID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT EmployeeID FROM RegRepair WHERE RegID = " + Bank.RegID + "";
            cmd.ExecuteNonQuery();
            int EmployeeID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO RepRepair (RepTitle, RepDate, RepDateEnd, RegID, CarID, ClientID, TotalPrice) VALUES ('" + richTextBox1.Text + "', '" + today.ToString().Split(' ')[0] + "', '" + dateTimePicker1.Value.ToString().Split(' ')[0] + "', " + Bank.RegID + ", " + CarID + ", " + ClientID + ", " + totalPrice + ")";
            cmd.ExecuteNonQuery();
            Bank.con.Close();
            //
            addRepairParts();
            addService();
            Program.Context.MainForm.Hide();
            Program.Context.MainForm = new MainForm();
            Program.Context.MainForm.Show();
        }
        private double CalculatingRepairParts()
        {
            double sum = 0;
            for (int i = 1; i <= listBox1.Items.Count - 1; i++)
            {
                sum = sum + (Convert.ToDouble(Convert.ToString(listBox1.Items[i]).Split(':')[1]) * Convert.ToDouble(Convert.ToString(listBox1.Items[i]).Split(':')[2]));
            }
            return sum;
        }
        private double CalculatingService()
        {
            double sum = 0;
            for (int i = 1; i <= listBox2.Items.Count - 1; i++)
            {
                sum = sum + Convert.ToDouble(Convert.ToString(listBox2.Items[i]).Split(':')[1]);
            }
            return sum;
        }
        private void addRepairParts()
        {
            Bank.con.Open();
            SqlCommand cmd = Bank.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 RepID FROM RepRepair ORDER BY RepID DESC";
            cmd.ExecuteNonQuery();
            int id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            for (int i = 1; i <= listBox1.Items.Count - 1; i++)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RepairPartID FROM RepairParts WHERE RepairPartTitle = '" + Convert.ToString(listBox1.Items[i]).Split(':')[0] + "'";
                cmd.ExecuteNonQuery();
                int RepairPartID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO PartsForRepair (RepairPartID, RepID, CountParts, PriceForCount) VALUES (" + RepairPartID + ", " + id + ", " + Convert.ToDouble(Convert.ToString(listBox1.Items[i]).Split(':')[2]) + ", " + Convert.ToDouble(Convert.ToString(listBox1.Items[i]).Split(':')[1]) * Convert.ToDouble(Convert.ToString(listBox1.Items[i]).Split(':')[2]) + ")";
                cmd.ExecuteNonQuery();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE RepairParts SET CountOnStore = CountOnStore - " + Convert.ToDouble(Convert.ToString(listBox1.Items[i]).Split(':')[2]) + " WHERE RepairPartID = " + RepairPartID + "";
                cmd.ExecuteNonQuery();
            }
            Bank.con.Close();
        }
        private void addService()
        {
            Bank.con.Open();
            SqlCommand cmd = Bank.con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP 1 RepID FROM RepRepair ORDER BY RepID DESC";
            cmd.ExecuteNonQuery();
            int id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            for (int i = 1; i <= listBox2.Items.Count - 1; i++)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ServiceID FROM CarService WHERE ServiceTitle = '" + Convert.ToString(listBox2.Items[i]).Split(':')[0] + "'";
                cmd.ExecuteNonQuery();
                int ServiceID = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO ServiceForRepair (ServiceID, RepID) VALUES (" + ServiceID + ", " + id + ")";
                cmd.ExecuteNonQuery();
            }
            Bank.con.Close();
        }
        private void AddReport_Shown(object sender, EventArgs e)
        {
            try
            {
                string[] array1 = getRepairParts().Select(n => n.ToString()).ToArray();
                comboBox1.Items.AddRange(array1);
                string[] array2 = getCarService().Select(n => n.ToString()).ToArray();
                comboBox2.Items.AddRange(array2);
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
                DialogResult dialogResult = MessageBox.Show("Уверены, что хотите закрыть данное окно?", "Уточнение", MessageBoxButtons.YesNo);
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    //
                    Bank.con.Open();
                    SqlCommand cmd = Bank.con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT CountOnStore FROM RepairParts WHERE RepairPartTitle = '" + Convert.ToString(comboBox1.SelectedItem.ToString().Split(':')[0]) + "'";
                    cmd.ExecuteNonQuery();
                    int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    Bank.con.Close();
                    if (count < numericUpDown1.Value)
                    {
                        MessageBox.Show($"На складе осталось {count} единиц данной детали", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        listBox1.Items.Add(comboBox1.SelectedItem + ":" + Convert.ToString(numericUpDown1.Value));
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
            try
            {
                if (comboBox2.SelectedIndex != -1)
                {
                    listBox2.Items.Add(comboBox2.SelectedItem);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

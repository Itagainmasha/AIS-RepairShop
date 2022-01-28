using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class Orders : Form
    {
        public static Orders SelfRef { get; set; }
        public Orders()
        {
            SelfRef = this;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Context.MainForm.Hide();
                Program.Context.MainForm = new AddOrder();
                Program.Context.MainForm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Orders_Shown(object sender, EventArgs e)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RegID, RegTitle as 'Описание', RegDate as 'Дата регистрации', CarTitle as 'Машина', GosNumber as 'Гос. номер', VIN, BrandTitle as 'Бренд', ClientSurname as 'Фамилия клиента', ClientName as 'Имя клиента', ClientPatronymic as 'Отчество клиента', EmployeeSurname as 'Фамилия сотрудника', EmployeeName as 'Имя сотрудника', EmployeePatronymic as 'Отчество сотрудника' FROM BrandCars, Employee, Clients, Cars, RegRepair WHERE RegRepair.CarID=Cars.CarID and Cars.BrandID=BrandCars.BrandID and RegRepair.ClientID=Clients.ClientID and RegRepair.EmployeeID=Employee.EmployeeID";
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
                        cmd.CommandText = "DELETE FROM RegRepair WHERE RegID = " + ID + "";
                        cmd.ExecuteNonQuery();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT RegID, RegTitle as 'Описание', RegDate as 'Дата регистрации', CarTitle as 'Машина', GosNumber as 'Гос. номер', VIN, BrandTitle as 'Бренд', ClientSurname as 'Фамилия клиента', ClientName as 'Имя клиента', ClientPatronymic as 'Отчество клиента', EmployeeSurname as 'Фамилия сотрудника', EmployeeName as 'Имя сотрудника', EmployeePatronymic as 'Отчество сотрудника' FROM BrandCars, Employee, Clients, Cars, RegRepair WHERE RegRepair.CarID=Cars.CarID and Cars.BrandID=BrandCars.BrandID and RegRepair.ClientID=Clients.ClientID and RegRepair.EmployeeID=Employee.EmployeeID";
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
        private readonly string TemplateFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RegRepair.docx");
        private void ReplateWordDocument(string stupToReplate, string text, Microsoft.Office.Interop.Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupToReplate, ReplaceWith: text);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Document (*.docx) | *.docx";
                try
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var newpathdoc = sfd.FileName;
                        var wordAPP = new Microsoft.Office.Interop.Word.Application();
                        wordAPP.Visible = false;
                        var worddocument = wordAPP.Documents.Open(TemplateFileName);
                        ReplateWordDocument("{date}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString().Split(' ')[0], worddocument);
                        ReplateWordDocument("{employee}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value.ToString(), worddocument);
                        ReplateWordDocument("{CarTitle}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString(), worddocument);
                        ReplateWordDocument("{GosNumber}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString(), worddocument);
                        ReplateWordDocument("{VIN}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString(), worddocument);
                        ReplateWordDocument("{Brand}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString(), worddocument);
                        ReplateWordDocument("{RegTitle}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString(), worddocument);
                        worddocument.SaveAs(newpathdoc);
                        wordAPP.Visible = true;
                        MessageBox.Show("Документ сохранен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Выберите запись!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    Bank.RegID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    Program.Context.MainForm.Hide();
                    Program.Context.MainForm = new AddReport();
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
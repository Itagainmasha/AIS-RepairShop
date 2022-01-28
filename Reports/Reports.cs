using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Shown(object sender, EventArgs e)
        {
            try
            {
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RepID, RepTitle as 'Title', RepDate as 'Дата отчёта', RepDateEnd as 'Дата конца работ', RegTitle as 'Описание', RegDate as 'Дата регистрации', CarTitle as 'Машина', GosNumber as 'Гос. номер', VIN, BrandTitle as 'Бренд', ClientSurname as 'Фамилия клиента', ClientName as 'Имя клиента', ClientPatronymic as 'Отчество клиента', EmployeeSurname as 'Фамилия сотрудника', EmployeeName as 'Имя сотрудника', EmployeePatronymic as 'Отчество сотрудника' FROM RepRepair, BrandCars, Employee, Clients, Cars, RegRepair WHERE RegRepair.CarID=Cars.CarID and Cars.BrandID=BrandCars.BrandID and RegRepair.ClientID=Clients.ClientID and RegRepair.EmployeeID=Employee.EmployeeID and RepRepair.RegID=RegRepair.RegID";
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
        private void ShowRepairPart()
        {
            if (dataGridView1.CurrentRow != null)
            {
                int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT RepairPartTitle as 'Деталь', SerialNumber as 'Серийный номер', CountParts as 'Количество' FROM PartsForRepair, RepairParts WHERE PartsForRepair.RepairPartID=RepairParts.RepairPartID and RepID = " + ID + "";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Выберите запись!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowService()
        {
            if (dataGridView1.CurrentRow != null)
            {
                int ID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                Bank.con.Open();
                SqlCommand cmd = Bank.con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ServiceTitle as 'Услуга' FROM ServiceForRepair, CarService WHERE ServiceForRepair.ServiceID=CarService.ServiceID and RepID = " + ID + "";
                cmd.ExecuteNonQuery();
                Bank.con.Close();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView3.DataSource = dt;

            }
            else
            {
                MessageBox.Show("Выберите запись!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ShowRepairPart();
                ShowService();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private readonly string TemplateFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RepRepair.docx");
        private void ReplateWordDocument(string stupToReplate, string text, Microsoft.Office.Interop.Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupToReplate, ReplaceWith: text);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
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
                            ReplateWordDocument("{employee}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value.ToString() + " " + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[15].Value.ToString(), worddocument);
                            ReplateWordDocument("{CarTitle}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString(), worddocument);
                            ReplateWordDocument("{GosNumber}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString(), worddocument);
                            ReplateWordDocument("{VIN}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString(), worddocument);
                            ReplateWordDocument("{Brand}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString(), worddocument);
                            ReplateWordDocument("{RepTitle}", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString(), worddocument);
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
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
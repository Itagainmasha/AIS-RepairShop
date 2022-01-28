using System;
using System.Windows.Forms;
namespace RepairShop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public static MainForm SelfRef { get; set; }
        public Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            try
            {
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                panel2.Controls.Add(childForm);
                panel2.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
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
                Bank.selectedTable = "Employee";
                openChildForm(new Employee());
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Bank.selectedTable = "Clients";
                openChildForm(new Clients());
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
                Bank.selectedTable = "Orders";
                openChildForm(new Orders());
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
                Bank.selectedTable = "Reports";
                openChildForm(new Reports());
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Bank.selectedTable == "Employee")
                {
                    openChildForm(new Employee());
                }
                if (Bank.selectedTable == "Clients")
                {
                    openChildForm(new Clients());
                }
                if (Bank.selectedTable == "Orders")
                {
                    openChildForm(new Orders());
                }
                if (Bank.selectedTable == "Reports")
                {
                    openChildForm(new Reports());
                }
                if (Bank.selectedTable == "RepairParts")
                {
                    openChildForm(new RepairParts());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.selectedTable = "RepairParts";
                openChildForm(new RepairParts());
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!", "Упс-с!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
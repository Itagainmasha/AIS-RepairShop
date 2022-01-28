using System;
using System.Windows.Forms;

namespace RepairShop
{
    public partial class Authorization : Form, IAuthorization
    {
        public Authorization()
        {
            InitializeComponent();
        }

        public string LoginText 
        {
            get
            {
                return textLogin.Text;
            }
            set
            {
                textLogin.Text = value;
            }
        }
        public string PasswordText
        {
            get
            {
                return textPassword.Text;
            }
            set
            {
                textPassword.Text = value;
            }
        }
        public Authorization form 
        {
            get;set;
        }
        private void Entrance_Click(object sender, EventArgs e)
        {
            AuthorizationController controller = new AuthorizationController(this);
            controller.Authorization();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
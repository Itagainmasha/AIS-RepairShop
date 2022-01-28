using System;
using System.Windows.Forms;
namespace RepairShop
{
    static class Program
    {
        public static ApplicationContext Context { get; set; }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Context = new ApplicationContext(new Authorization());
            Application.Run(Context);
        }
    }
}
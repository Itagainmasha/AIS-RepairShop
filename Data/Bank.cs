using System.Data.SqlClient;

namespace RepairShop
{
    class Bank
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-L6MF8M5;Initial Catalog=RepairShop;Integrated Security=True");
        public static string selectedTable;
        //change employee
        public static int EmployeeID = -1;
        public static string EmployeeSurname;
        public static string EmployeeName;
        public static string EmployeePatronymic;
        public static string Department;
        public static string Position;
        public static string EmployeeDescription;
        public static string EmployeeLogin;
        public static string EmployeePassword;
        //change clients
        public static int ClientID;
        public static string ClientSurname;
        public static string ClientName;
        public static string ClientPatronymic;
        public static string ClientPhone;
        public static string ClientEmail;
        //create report
        public static int RegID;
    }
}
// ============================================================
//  Program.cs  –  Program.cs
//  Application entry point.
// ============================================================
using System;
using System.Windows.Forms;
using ClinicManagementSystem.Forms;

namespace ClinicManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Verify database connectivity before opening the Login form
            if (!Data.DatabaseManager.Instance.TestConnection())
            {
                MessageBox.Show(
                    "Cannot connect to the SQL Server database.\n\n" +
                    "Please check:\n" +
                    "  1. SQL Server is running.\n" +
                    "  2. ClinicManagementDB exists (run ClinicDB_Setup.sql).\n" +
                    "  3. The connection string in DatabaseManager.cs matches your server.",
                    "Database Connection Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            while (true)
            {
                string loggedInUser = "";
                bool loggedIn = false;

                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        loggedIn = true;
                        loggedInUser = login.LoggedInUsername;
                    }
                    else
                    {
                        break; // exit loop if login was cancelled or closed
                    }
                }

                if (loggedIn)
                {
                    var dashboard = new DashboardForm(loggedInUser);
                    Application.Run(dashboard);

                    if (!dashboard.LogoutRequested)
                    {
                        break; // exit loop if closed dashboard directly
                    }
                }
            }
        }
    }
}

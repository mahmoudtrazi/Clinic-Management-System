// ============================================================
//  DashboardForm.cs  –  Forms/DashboardForm.cs
//  Main navigation hub after successful login.
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystem.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly string _username;

        public DashboardForm(string username)
        {
            _username = username;
            InitializeComponent();
            lblGreeting.Text = $"Welcome, {username}  👋";
            lblDateTime.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy  |  HH:mm");

            // Tick clock every second
            var timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += (s, e) => lblDateTime.Text =
                DateTime.Now.ToString("dddd, MMMM dd yyyy  |  HH:mm");
            timer.Start();
        }

        // ── Navigation ────────────────────────────────────────────────────────

        private void btnPatients_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PatientsForm());
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AppointmentsForm());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SearchFilterForm());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReportsForm());
        }

        public bool LogoutRequested { get; private set; } = false;

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Log out and return to the Login screen?", "Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LogoutRequested = true;
                this.Close();
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private void OpenChildForm(Form childForm)
        {
            childForm.StartPosition = FormStartPosition.CenterParent;
            childForm.ShowDialog(this);
        }
    }
}

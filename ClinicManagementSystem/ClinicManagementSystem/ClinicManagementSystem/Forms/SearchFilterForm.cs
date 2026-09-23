// ============================================================
//  SearchFilterForm.cs  –  Forms/SearchFilterForm.cs
//  Search Patients by Name/Phone; Filter Appointments by
//  Treatment Type or Date Range.
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Forms
{
    public partial class SearchFilterForm : Form
    {
        public SearchFilterForm()
        {
            InitializeComponent();
        }

        // ── Search Patients by Name ───────────────────────────────────────────

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            string name = txtSearchName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name to search.",
                    "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DatabaseManager.Instance.SearchPatientsByName(name);
            dgvResults.DataSource = dt;
            lblResultCount.Text   = $"{dt.Rows.Count} result(s) found.";
            StyleGrid(Color.FromArgb(21, 101, 192));
        }

        // ── Search Patients by Phone ──────────────────────────────────────────

        private void btnSearchByPhone_Click(object sender, EventArgs e)
        {
            string phone = txtSearchPhone.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter a phone number to search.",
                    "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DatabaseManager.Instance.SearchPatientsByPhone(phone);
            dgvResults.DataSource = dt;
            lblResultCount.Text   = $"{dt.Rows.Count} result(s) found.";
            StyleGrid(Color.FromArgb(21, 101, 192));
        }

        // ── Filter Appointments by Treatment ─────────────────────────────────

        private void btnFilterTreatment_Click(object sender, EventArgs e)
        {
            string treatment = txtFilterTreatment.Text.Trim();
            if (string.IsNullOrEmpty(treatment))
            {
                MessageBox.Show("Please enter a treatment type to filter.",
                    "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DatabaseManager.Instance.FilterAppointmentsByTreatment(treatment);
            dgvResults.DataSource = dt;
            lblResultCount.Text   = $"{dt.Rows.Count} appointment(s) found.";
            StyleGrid(Color.FromArgb(0, 131, 143));
        }

        // ── Filter Appointments by Date Range ─────────────────────────────────

        private void btnFilterDate_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpFrom.Value.Date;
            DateTime endDate   = dtpTo.Value.Date.AddDays(1).AddSeconds(-1);

            if (startDate > endDate)
            {
                MessageBox.Show("'From' date must be before 'To' date.",
                    "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DatabaseManager.Instance.FilterAppointmentsByDate(startDate, endDate);
            dgvResults.DataSource = dt;
            lblResultCount.Text   = $"{dt.Rows.Count} appointment(s) in range.";
            StyleGrid(Color.FromArgb(0, 131, 143));
        }

        // ── Clear Results ─────────────────────────────────────────────────────

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvResults.DataSource = null;
            lblResultCount.Text   = "";
            txtSearchName.Clear();
            txtSearchPhone.Clear();
            txtFilterTreatment.Clear();
        }

        // ── Grid Styling ──────────────────────────────────────────────────────

        private void StyleGrid(Color headerColor)
        {
            dgvResults.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(242, 248, 255);
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.ReadOnly = true;
        }
    }
}

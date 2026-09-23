// ============================================================
//  ReportsForm.cs  –  Forms/ReportsForm.cs
//  Statistics dashboard: summary cards + treatment breakdown.
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Forms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                // ── Summary Cards ────────────────────────────────────────────
                lblTotalPatients.Text     = DatabaseManager.Instance
                    .GetTotalPatients().ToString("N0");

                lblTotalAppointments.Text = DatabaseManager.Instance
                    .GetTotalAppointments().ToString("N0");

                lblTotalRevenue.Text      = DatabaseManager.Instance
                    .GetTotalRevenue().ToString("C2");

                lblMostCommon.Text        = DatabaseManager.Instance
                    .GetMostCommonTreatment();

                // ── Treatment Breakdown Grid ──────────────────────────────────
                DataTable dt = DatabaseManager.Instance.GetTreatmentSummary();
                dgvSummary.DataSource = dt;
                StyleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load statistics:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadStatistics();

        private void StyleGrid()
        {
            dgvSummary.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvSummary.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(121, 85, 72);
            dgvSummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSummary.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvSummary.EnableHeadersVisualStyles = false;
            dgvSummary.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(250, 245, 243);
            dgvSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSummary.ReadOnly = true;
        }
    }
}

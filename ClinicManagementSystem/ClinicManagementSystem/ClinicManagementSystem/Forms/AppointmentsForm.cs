// ============================================================
//  AppointmentsForm.cs  –  Forms/AppointmentsForm.cs
//  Full CRUD for Appointments + Patient ComboBox.
// ============================================================
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagementSystem.Data;
using ClinicManagementSystem.Helpers;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Forms
{
    public partial class AppointmentsForm : Form
    {
        private int _selectedAppointmentID = -1;

        public AppointmentsForm()
        {
            InitializeComponent();
            LoadPatientComboBox();
            LoadAppointments();
        }

        // ── Load Patient ComboBox ─────────────────────────────────────────────

        private void LoadPatientComboBox()
        {
            DataTable dt = DatabaseManager.Instance.GetPatientList();
            cmbPatient.DataSource    = dt;
            cmbPatient.DisplayMember = "FullName";
            cmbPatient.ValueMember   = "PatientID";
            cmbPatient.SelectedIndex = -1;
        }

        // ── Load Appointments ─────────────────────────────────────────────────

        private void LoadAppointments()
        {
            DataTable dt = DatabaseManager.Instance.GetAllAppointments();
            dgvAppointments.DataSource = dt;
            StyleGrid();
            lblCount.Text = $"Total appointments: {dt.Rows.Count}";
        }

        private void StyleGrid()
        {
            dgvAppointments.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointments.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(0, 131, 143);
            dgvAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAppointments.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvAppointments.EnableHeadersVisualStyles = false;
            dgvAppointments.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(240, 250, 251);
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.ReadOnly = true;
        }

        // ── Add ───────────────────────────────────────────────────────────────

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var appt)) return;

            if (DatabaseManager.Instance.AddAppointment(appt))
            {
                MessageBox.Show("Appointment added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadAppointments();
            }
        }

        // ── Update ────────────────────────────────────────────────────────────

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedAppointmentID < 0)
            {
                MessageBox.Show("Please select an appointment row first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(out var appt)) return;
            appt.AppointmentID = _selectedAppointmentID;

            if (DatabaseManager.Instance.UpdateAppointment(appt))
            {
                MessageBox.Show("Appointment updated.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadAppointments();
            }
        }

        // ── Delete ────────────────────────────────────────────────────────────

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedAppointmentID < 0)
            {
                MessageBox.Show("Please select an appointment row first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this appointment?",
                    "Confirm Delete", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (DatabaseManager.Instance.DeleteAppointment(_selectedAppointmentID))
                {
                    MessageBox.Show("Appointment deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadAppointments();
                }
            }
        }

        // ── Row Selection ─────────────────────────────────────────────────────

        private void dgvAppointments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0) return;
            var row = dgvAppointments.SelectedRows[0];

            _selectedAppointmentID = Convert.ToInt32(row.Cells["AppointmentID"].Value);
            txtTreatment.Text      = row.Cells["TreatmentType"].Value?.ToString() ?? string.Empty;
            txtCost.Text           = row.Cells["Cost"].Value?.ToString()           ?? string.Empty;
            dtpDate.Value          = Convert.ToDateTime(row.Cells["AppointmentDate"].Value);

            // Re-select patient in ComboBox
            int pid = Convert.ToInt32(row.Cells["PatientID"].Value);
            cmbPatient.SelectedValue = pid;
        }

        // ── Clear ─────────────────────────────────────────────────────────────

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            txtTreatment.Clear();
            txtCost.Clear();
            dtpDate.Value = DateTime.Now;
            cmbPatient.SelectedIndex = -1;
            _selectedAppointmentID = -1;
            dgvAppointments.ClearSelection();
        }

        // ── Validation ────────────────────────────────────────────────────────

        private bool ValidateInputs(out Appointment appt)
        {
            appt = new Appointment();

            if (cmbPatient.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a patient.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Validator.IsNotEmpty(txtTreatment.Text, "Treatment Type")) return false;
            if (!Validator.IsValidDecimal(txtCost.Text, "Cost", out decimal cost)) return false;

            appt.PatientID       = Convert.ToInt32(cmbPatient.SelectedValue);
            appt.AppointmentDate = dtpDate.Value;
            appt.TreatmentType   = txtTreatment.Text.Trim();
            appt.Cost            = cost;
            return true;
        }
    }
}

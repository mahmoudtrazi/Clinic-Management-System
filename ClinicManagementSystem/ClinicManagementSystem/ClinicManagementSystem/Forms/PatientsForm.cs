// ============================================================
//  PatientsForm.cs  –  Forms/PatientsForm.cs
//  Full CRUD for Patients with DataGridView & Validation.
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
    public partial class PatientsForm : Form
    {
        private int _selectedPatientID = -1;

        public PatientsForm()
        {
            InitializeComponent();
            LoadPatients();
        }

        // ── Load / Refresh ────────────────────────────────────────────────────

        private void LoadPatients()
        {
            DataTable dt = DatabaseManager.Instance.GetAllPatients();
            dgvPatients.DataSource = dt;
            StyleGrid();
            lblCount.Text = $"Total patients: {dt.Rows.Count}";
        }

        private void StyleGrid()
        {
            dgvPatients.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(21, 101, 192);
            dgvPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPatients.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9f, FontStyle.Bold);
            dgvPatients.EnableHeadersVisualStyles = false;
            dgvPatients.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 255);
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.ReadOnly = true;
        }

        // ── Add ───────────────────────────────────────────────────────────────

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out var patient)) return;

            if (DatabaseManager.Instance.AddPatient(patient))
            {
                MessageBox.Show("Patient added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadPatients();
            }
        }

        // ── Update ────────────────────────────────────────────────────────────

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedPatientID < 0)
            {
                MessageBox.Show("Please select a patient row first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs(out var patient)) return;
            patient.PatientID = _selectedPatientID;

            if (DatabaseManager.Instance.UpdatePatient(patient))
            {
                MessageBox.Show("Patient updated.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadPatients();
            }
        }

        // ── Delete ────────────────────────────────────────────────────────────

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPatientID < 0)
            {
                MessageBox.Show("Please select a patient row first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this patient and all their appointments?",
                    "Confirm Delete", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (DatabaseManager.Instance.DeletePatient(_selectedPatientID))
                {
                    MessageBox.Show("Patient deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadPatients();
                }
            }
        }

        // ── Row Selection ─────────────────────────────────────────────────────

        private void dgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0) return;
            var row = dgvPatients.SelectedRows[0];

            _selectedPatientID   = Convert.ToInt32(row.Cells["PatientID"].Value);
            txtName.Text         = row.Cells["FullName"].Value?.ToString()    ?? string.Empty;
            txtAge.Text          = row.Cells["Age"].Value?.ToString()         ?? string.Empty;
            txtPhone.Text        = row.Cells["PhoneNumber"].Value?.ToString()  ?? string.Empty;
            cmbGender.Text       = row.Cells["Gender"].Value?.ToString()      ?? string.Empty;
        }

        // ── Clear ─────────────────────────────────────────────────────────────

        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            txtName.Clear();
            txtAge.Clear();
            txtPhone.Clear();
            cmbGender.SelectedIndex = -1;
            _selectedPatientID = -1;
            dgvPatients.ClearSelection();
        }

        // ── Validation ────────────────────────────────────────────────────────

        private bool ValidateInputs(out Patient patient)
        {
            patient = new Patient();

            if (!Validator.IsNotEmpty(txtName.Text, "Full Name"))  return false;
            if (!Validator.IsValidAge(txtAge.Text, out int age))   return false;
            if (!Validator.IsValidPhone(txtPhone.Text))            return false;
            if (cmbGender.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a gender.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            patient.FullName    = txtName.Text.Trim();
            patient.Age         = age;
            patient.PhoneNumber = txtPhone.Text.Trim();
            patient.Gender      = cmbGender.SelectedItem!.ToString()!;
            return true;
        }
    }
}

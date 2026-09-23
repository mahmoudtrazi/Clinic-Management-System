// ============================================================
//  AppointmentsForm.Designer.cs  –  Forms/AppointmentsForm.Designer.cs
// ============================================================
namespace ClinicManagementSystem.Forms
{
    partial class AppointmentsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel          pnlHeader;
        private System.Windows.Forms.Label          lblTitle;
        private System.Windows.Forms.GroupBox       grpInput;
        private System.Windows.Forms.Label          lblPatient;
        private System.Windows.Forms.ComboBox       cmbPatient;
        private System.Windows.Forms.Label          lblTreatment;
        private System.Windows.Forms.TextBox        txtTreatment;
        private System.Windows.Forms.Label          lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label          lblCost;
        private System.Windows.Forms.TextBox        txtCost;
        private System.Windows.Forms.Button         btnAdd;
        private System.Windows.Forms.Button         btnUpdate;
        private System.Windows.Forms.Button         btnDelete;
        private System.Windows.Forms.Button         btnClear;
        private System.Windows.Forms.DataGridView   dgvAppointments;
        private System.Windows.Forms.Label          lblCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var clrTeal  = System.Drawing.Color.FromArgb(0, 131, 143);
            var clrDark  = System.Drawing.Color.FromArgb(0, 96, 106);
            var clrLight = System.Drawing.Color.FromArgb(245, 250, 251);
            var clrWhite = System.Drawing.Color.White;
            var clrText  = System.Drawing.Color.FromArgb(30, 58, 95);

            this.SuspendLayout();

            this.Text          = "Appointments Management";
            this.Size          = new System.Drawing.Size(980, 660);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor     = clrLight;
            this.Font          = new System.Drawing.Font("Segoe UI", 9f);

            // ── Header ────────────────────────────────────────────────────────
            pnlHeader = new System.Windows.Forms.Panel
            { Dock = System.Windows.Forms.DockStyle.Top, Height = 60, BackColor = clrDark };
            this.Controls.Add(pnlHeader);

            lblTitle = new System.Windows.Forms.Label
            {
                Text      = "📅  Appointments Management",
                ForeColor = clrWhite,
                Font      = new System.Drawing.Font("Segoe UI", 15f, System.Drawing.FontStyle.Bold),
                AutoSize  = false, Dock = System.Windows.Forms.DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding   = new System.Windows.Forms.Padding(20, 0, 0, 0)
            };
            pnlHeader.Controls.Add(lblTitle);

            // ── Input GroupBox ────────────────────────────────────────────────
            grpInput = new System.Windows.Forms.GroupBox
            {
                Text = "Appointment Details", Size = new System.Drawing.Size(940, 170),
                Location = new System.Drawing.Point(16, 76),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold),
                ForeColor = clrTeal, BackColor = clrWhite
            };
            this.Controls.Add(grpInput);

            int lbY = 26, tbY = 46, tbH = 28;

            lblPatient   = MakeLabel("Patient",        10,  lbY, clrText);
            cmbPatient   = new System.Windows.Forms.ComboBox
            {
                Name = "cmbPatient", Size = new System.Drawing.Size(220, tbH),
                Location = new System.Drawing.Point(10, tbY),
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                BackColor = clrLight, ForeColor = clrText,
                Font = new System.Drawing.Font("Segoe UI", 9.5f)
            };

            lblTreatment = MakeLabel("Treatment Type", 246, lbY, clrText);
            txtTreatment = MakeTextBox("txtTreatment", 246, tbY, 240, tbH, clrLight, clrText);

            lblDate      = MakeLabel("Date & Time",    502, lbY, clrText);
            dtpDate      = new System.Windows.Forms.DateTimePicker
            {
                Name = "dtpDate", Size = new System.Drawing.Size(200, tbH),
                Location = new System.Drawing.Point(502, tbY),
                Format = System.Windows.Forms.DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd  HH:mm",
                Font = new System.Drawing.Font("Segoe UI", 9.5f)
            };

            lblCost      = MakeLabel("Cost (SAR)",     718, lbY, clrText);
            txtCost      = MakeTextBox("txtCost",      718, tbY, 130, tbH, clrLight, clrText);

            grpInput.Controls.AddRange(new System.Windows.Forms.Control[]
            { lblPatient, cmbPatient, lblTreatment, txtTreatment,
              lblDate, dtpDate, lblCost, txtCost });

            btnAdd    = MakeButton("btnAdd",    "➕ Add",    10,  108, System.Drawing.Color.FromArgb(0,  131, 143), clrWhite);
            btnUpdate = MakeButton("btnUpdate", "✏ Update", 124, 108, System.Drawing.Color.FromArgb(21, 101, 192), clrWhite);
            btnDelete = MakeButton("btnDelete", "🗑 Delete",  238, 108, System.Drawing.Color.FromArgb(198, 40, 40),  clrWhite);
            btnClear  = MakeButton("btnClear",  "✖ Clear",  352, 108, System.Drawing.Color.FromArgb(90, 120, 155),  clrWhite);

            btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);
            btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            btnClear.Click  += new System.EventHandler(this.btnClear_Click);

            grpInput.Controls.AddRange(new System.Windows.Forms.Control[]
            { btnAdd, btnUpdate, btnDelete, btnClear });

            // ── DataGridView ──────────────────────────────────────────────────
            dgvAppointments = new System.Windows.Forms.DataGridView
            {
                Name = "dgvAppointments",
                Size = new System.Drawing.Size(940, 360),
                Location = new System.Drawing.Point(16, 258),
                BackgroundColor = clrWhite,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = System.Drawing.Color.FromArgb(200, 230, 230),
                RowHeadersVisible = false,
                Font = new System.Drawing.Font("Segoe UI", 9f),
                DefaultCellStyle = { SelectionBackColor = System.Drawing.Color.FromArgb(190, 230, 235),
                                     SelectionForeColor = clrText }
            };
            dgvAppointments.SelectionChanged +=
                new System.EventHandler(this.dgvAppointments_SelectionChanged);
            this.Controls.Add(dgvAppointments);

            lblCount = new System.Windows.Forms.Label
            {
                Text = "", ForeColor = System.Drawing.Color.FromArgb(90, 120, 155),
                Font = new System.Drawing.Font("Segoe UI", 8.5f),
                AutoSize = true, Location = new System.Drawing.Point(16, 625)
            };
            this.Controls.Add(lblCount);

            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Label MakeLabel(
            string text, int x, int y, System.Drawing.Color fore) =>
            new System.Windows.Forms.Label
            {
                Text = text, ForeColor = fore,
                Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold),
                AutoSize = false, Size = new System.Drawing.Size(220, 18),
                Location = new System.Drawing.Point(x, y)
            };

        private static System.Windows.Forms.TextBox MakeTextBox(
            string name, int x, int y, int w, int h,
            System.Drawing.Color back, System.Drawing.Color fore) =>
            new System.Windows.Forms.TextBox
            {
                Name = name, Size = new System.Drawing.Size(w, h),
                Location = new System.Drawing.Point(x, y),
                Font = new System.Drawing.Font("Segoe UI", 9.5f),
                BackColor = back, ForeColor = fore,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };

        private static System.Windows.Forms.Button MakeButton(
            string name, string text, int x, int y,
            System.Drawing.Color back, System.Drawing.Color fore)
        {
            var b = new System.Windows.Forms.Button
            {
                Name = name, Text = text,
                Size = new System.Drawing.Size(108, 32),
                Location = new System.Drawing.Point(x, y),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold),
                BackColor = back, ForeColor = fore,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }
    }
}

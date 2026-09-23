// ============================================================
//  SearchFilterForm.Designer.cs  –  Forms/SearchFilterForm.Designer.cs
// ============================================================
namespace ClinicManagementSystem.Forms
{
    partial class SearchFilterForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel        pnlHeader;
        private System.Windows.Forms.Label        lblTitle;
        private System.Windows.Forms.GroupBox     grpPatients;
        private System.Windows.Forms.Label        lblSearchName;
        private System.Windows.Forms.TextBox      txtSearchName;
        private System.Windows.Forms.Button       btnSearchByName;
        private System.Windows.Forms.Label        lblSearchPhone;
        private System.Windows.Forms.TextBox      txtSearchPhone;
        private System.Windows.Forms.Button       btnSearchByPhone;
        private System.Windows.Forms.GroupBox     grpAppointments;
        private System.Windows.Forms.Label        lblFilterTreatment;
        private System.Windows.Forms.TextBox      txtFilterTreatment;
        private System.Windows.Forms.Button       btnFilterTreatment;
        private System.Windows.Forms.Label        lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label        lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button       btnFilterDate;
        private System.Windows.Forms.Button       btnClear;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label        lblResultCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var clrGreen = System.Drawing.Color.FromArgb(56, 142, 60);
            var clrDark  = System.Drawing.Color.FromArgb(27, 94, 32);
            var clrLight = System.Drawing.Color.FromArgb(245, 250, 247);
            var clrWhite = System.Drawing.Color.White;
            var clrText  = System.Drawing.Color.FromArgb(30, 58, 95);
            var clrBlue  = System.Drawing.Color.FromArgb(21, 101, 192);
            var clrTeal  = System.Drawing.Color.FromArgb(0, 131, 143);

            this.SuspendLayout();

            this.Text          = "Search & Filter";
            this.Size          = new System.Drawing.Size(980, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor     = clrLight;
            this.Font          = new System.Drawing.Font("Segoe UI", 9f);

            // ── Header ────────────────────────────────────────────────────────
            pnlHeader = new System.Windows.Forms.Panel
            { Dock = System.Windows.Forms.DockStyle.Top, Height = 60, BackColor = clrDark };
            this.Controls.Add(pnlHeader);

            lblTitle = new System.Windows.Forms.Label
            {
                Text = "🔍  Search & Filter", ForeColor = clrWhite,
                Font = new System.Drawing.Font("Segoe UI", 15f, System.Drawing.FontStyle.Bold),
                AutoSize = false, Dock = System.Windows.Forms.DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new System.Windows.Forms.Padding(20, 0, 0, 0)
            };
            pnlHeader.Controls.Add(lblTitle);

            // ── Patient Search Group ───────────────────────────────────────────
            grpPatients = new System.Windows.Forms.GroupBox
            {
                Text = "Search Patients", Size = new System.Drawing.Size(460, 100),
                Location = new System.Drawing.Point(16, 76),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold),
                ForeColor = clrBlue, BackColor = clrWhite
            };
            this.Controls.Add(grpPatients);

            lblSearchName    = new System.Windows.Forms.Label
            { Text = "By Name:", Location = new System.Drawing.Point(10, 30),
              AutoSize = true, ForeColor = clrText, Font = new System.Drawing.Font("Segoe UI", 9f) };
            txtSearchName    = new System.Windows.Forms.TextBox
            { Name = "txtSearchName", Location = new System.Drawing.Point(80, 26),
              Size = new System.Drawing.Size(200, 26),
              Font = new System.Drawing.Font("Segoe UI", 9.5f),
              BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };
            btnSearchByName  = MakeBtn("btnSearchByName", "Search", 290, 25, clrBlue, clrWhite, 120);
            btnSearchByName.Click += new System.EventHandler(this.btnSearchByName_Click);

            lblSearchPhone   = new System.Windows.Forms.Label
            { Text = "By Phone:", Location = new System.Drawing.Point(10, 62),
              AutoSize = true, ForeColor = clrText, Font = new System.Drawing.Font("Segoe UI", 9f) };
            txtSearchPhone   = new System.Windows.Forms.TextBox
            { Name = "txtSearchPhone", Location = new System.Drawing.Point(80, 58),
              Size = new System.Drawing.Size(200, 26),
              Font = new System.Drawing.Font("Segoe UI", 9.5f),
              BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };
            btnSearchByPhone = MakeBtn("btnSearchByPhone", "Search", 290, 57, clrBlue, clrWhite, 120);
            btnSearchByPhone.Click += new System.EventHandler(this.btnSearchByPhone_Click);

            grpPatients.Controls.AddRange(new System.Windows.Forms.Control[]
            { lblSearchName, txtSearchName, btnSearchByName,
              lblSearchPhone, txtSearchPhone, btnSearchByPhone });

            // ── Appointments Filter Group ─────────────────────────────────────
            grpAppointments = new System.Windows.Forms.GroupBox
            {
                Text = "Filter Appointments", Size = new System.Drawing.Size(480, 100),
                Location = new System.Drawing.Point(490, 76),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold),
                ForeColor = clrTeal, BackColor = clrWhite
            };
            this.Controls.Add(grpAppointments);

            lblFilterTreatment = new System.Windows.Forms.Label
            { Text = "Treatment:", Location = new System.Drawing.Point(10, 30), AutoSize = true,
              ForeColor = clrText, Font = new System.Drawing.Font("Segoe UI", 9f) };
            txtFilterTreatment = new System.Windows.Forms.TextBox
            { Name = "txtFilterTreatment", Location = new System.Drawing.Point(90, 26),
              Size = new System.Drawing.Size(180, 26), Font = new System.Drawing.Font("Segoe UI", 9.5f),
              BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle };
            btnFilterTreatment = MakeBtn("btnFilterTreatment", "Filter", 280, 25, clrTeal, clrWhite, 120);
            btnFilterTreatment.Click += new System.EventHandler(this.btnFilterTreatment_Click);

            lblFrom = new System.Windows.Forms.Label
            { Text = "From:", Location = new System.Drawing.Point(10, 62), AutoSize = true,
              ForeColor = clrText, Font = new System.Drawing.Font("Segoe UI", 9f) };
            dtpFrom = new System.Windows.Forms.DateTimePicker
            { Location = new System.Drawing.Point(60, 58), Size = new System.Drawing.Size(140, 26),
              Format = System.Windows.Forms.DateTimePickerFormat.Short,
              Font = new System.Drawing.Font("Segoe UI", 9f) };
            lblTo = new System.Windows.Forms.Label
            { Text = "To:", Location = new System.Drawing.Point(210, 62), AutoSize = true,
              ForeColor = clrText, Font = new System.Drawing.Font("Segoe UI", 9f) };
            dtpTo = new System.Windows.Forms.DateTimePicker
            { Location = new System.Drawing.Point(235, 58), Size = new System.Drawing.Size(140, 26),
              Format = System.Windows.Forms.DateTimePickerFormat.Short,
              Font = new System.Drawing.Font("Segoe UI", 9f) };
            btnFilterDate = MakeBtn("btnFilterDate", "Filter", 385, 57, clrTeal, clrWhite, 80);
            btnFilterDate.Click += new System.EventHandler(this.btnFilterDate_Click);

            grpAppointments.Controls.AddRange(new System.Windows.Forms.Control[]
            { lblFilterTreatment, txtFilterTreatment, btnFilterTreatment,
              lblFrom, dtpFrom, lblTo, dtpTo, btnFilterDate });

            // ── Clear Button ──────────────────────────────────────────────────
            btnClear = MakeBtn("btnClear", "✖  Clear Results", 16, 186,
                System.Drawing.Color.FromArgb(90, 120, 155), clrWhite, 160);
            btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.Controls.Add(btnClear);

            // ── Results Grid ──────────────────────────────────────────────────
            dgvResults = new System.Windows.Forms.DataGridView
            {
                Name = "dgvResults",
                Size = new System.Drawing.Size(940, 420),
                Location = new System.Drawing.Point(16, 226),
                BackgroundColor = clrWhite,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = System.Drawing.Color.FromArgb(205, 225, 210),
                RowHeadersVisible = false,
                Font = new System.Drawing.Font("Segoe UI", 9f)
            };
            this.Controls.Add(dgvResults);

            lblResultCount = new System.Windows.Forms.Label
            {
                Text = "", ForeColor = System.Drawing.Color.FromArgb(90, 120, 155),
                Font = new System.Drawing.Font("Segoe UI", 8.5f),
                AutoSize = true, Location = new System.Drawing.Point(16, 652)
            };
            this.Controls.Add(lblResultCount);

            this.ResumeLayout(false);
        }

        private static System.Windows.Forms.Button MakeBtn(
            string name, string text, int x, int y,
            System.Drawing.Color back, System.Drawing.Color fore, int w = 108)
        {
            var b = new System.Windows.Forms.Button
            {
                Name = name, Text = text,
                Size = new System.Drawing.Size(w, 28),
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

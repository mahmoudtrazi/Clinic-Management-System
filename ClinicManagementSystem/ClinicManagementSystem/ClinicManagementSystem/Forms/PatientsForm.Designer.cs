// ============================================================
//  PatientsForm.Designer.cs  –  Forms/PatientsForm.Designer.cs
// ============================================================
namespace ClinicManagementSystem.Forms
{
    partial class PatientsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel              pnlHeader;
        private System.Windows.Forms.Label              lblTitle;
        private System.Windows.Forms.GroupBox           grpInput;
        private System.Windows.Forms.Label              lblName;
        private System.Windows.Forms.TextBox            txtName;
        private System.Windows.Forms.Label              lblAge;
        private System.Windows.Forms.TextBox            txtAge;
        private System.Windows.Forms.Label              lblGender;
        private System.Windows.Forms.ComboBox           cmbGender;
        private System.Windows.Forms.Label              lblPhone;
        private System.Windows.Forms.TextBox            txtPhone;
        private System.Windows.Forms.Button             btnAdd;
        private System.Windows.Forms.Button             btnUpdate;
        private System.Windows.Forms.Button             btnDelete;
        private System.Windows.Forms.Button             btnClear;
        private System.Windows.Forms.DataGridView       dgvPatients;
        private System.Windows.Forms.Label              lblCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var clrBlue  = System.Drawing.Color.FromArgb(21, 101, 192);
            var clrDark  = System.Drawing.Color.FromArgb(13, 71, 161);
            var clrLight = System.Drawing.Color.FromArgb(245, 248, 252);
            var clrWhite = System.Drawing.Color.White;
            var clrText  = System.Drawing.Color.FromArgb(30, 58, 95);

            this.SuspendLayout();

            this.Text            = "Patients Management";
            this.Size            = new System.Drawing.Size(960, 640);
            this.MinimumSize     = new System.Drawing.Size(960, 640);
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor       = clrLight;
            this.Font            = new System.Drawing.Font("Segoe UI", 9f);

            // ── Header ────────────────────────────────────────────────────────
            pnlHeader = new System.Windows.Forms.Panel
            {
                Dock      = System.Windows.Forms.DockStyle.Top,
                Height    = 60,
                BackColor = clrDark
            };
            this.Controls.Add(pnlHeader);

            lblTitle = new System.Windows.Forms.Label
            {
                Text      = "👤  Patients Management",
                ForeColor = clrWhite,
                Font      = new System.Drawing.Font("Segoe UI", 15f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Dock      = System.Windows.Forms.DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding   = new System.Windows.Forms.Padding(20, 0, 0, 0)
            };
            pnlHeader.Controls.Add(lblTitle);

            // ── Input GroupBox ────────────────────────────────────────────────
            grpInput = new System.Windows.Forms.GroupBox
            {
                Text      = "Patient Details",
                Size      = new System.Drawing.Size(920, 155),
                Location  = new System.Drawing.Point(16, 76),
                Font      = new System.Drawing.Font("Segoe UI", 9f,
                                System.Drawing.FontStyle.Bold),
                ForeColor = clrBlue,
                BackColor = clrWhite,
                Padding   = new System.Windows.Forms.Padding(10)
            };
            this.Controls.Add(grpInput);

            // Row 1: Name | Age | Gender | Phone
            int lbY = 28, tbY = 48, lbH = 18, tbH = 28, col1 = 10, col2 = 240, col3 = 470, col4 = 700;
            int tbW = 200;

            lblName = MakeLabel("Full Name", col1, lbY, clrText);
            txtName = MakeTextBox("txtName", col1, tbY, tbW, tbH, clrLight, clrText);

            lblAge = MakeLabel("Age", col2, lbY, clrText);
            txtAge = MakeTextBox("txtAge", col2, tbY, 100, tbH, clrLight, clrText);

            lblGender = MakeLabel("Gender", col3, lbY, clrText);
            cmbGender = new System.Windows.Forms.ComboBox
            {
                Name      = "cmbGender",
                Size      = new System.Drawing.Size(150, tbH),
                Location  = new System.Drawing.Point(col3, tbY),
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
                BackColor = clrLight,
                ForeColor = clrText,
                Font      = new System.Drawing.Font("Segoe UI", 9.5f)
            };
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });

            lblPhone = MakeLabel("Phone Number", col4, lbY, clrText);
            txtPhone = MakeTextBox("txtPhone", col4, tbY, tbW, tbH, clrLight, clrText);

            grpInput.Controls.AddRange(new System.Windows.Forms.Control[]
            { lblName, txtName, lblAge, txtAge, lblGender, cmbGender, lblPhone, txtPhone });

            // Row 2: Buttons
            btnAdd    = MakeButton("btnAdd",    "➕ Add",    col1,       90, System.Drawing.Color.FromArgb(21,  101, 192), clrWhite);
            btnUpdate = MakeButton("btnUpdate", "✏ Update", col1 + 120, 90, System.Drawing.Color.FromArgb(0,   131, 143), clrWhite);
            btnDelete = MakeButton("btnDelete", "🗑 Delete",  col1 + 240, 90, System.Drawing.Color.FromArgb(198, 40,  40),  clrWhite);
            btnClear  = MakeButton("btnClear",  "✖ Clear",  col1 + 360, 90, System.Drawing.Color.FromArgb(90,  120, 155), clrWhite);

            btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);
            btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            btnClear.Click  += new System.EventHandler(this.btnClear_Click);

            grpInput.Controls.AddRange(new System.Windows.Forms.Control[]
            { btnAdd, btnUpdate, btnDelete, btnClear });

            // ── DataGridView ──────────────────────────────────────────────────
            dgvPatients = new System.Windows.Forms.DataGridView
            {
                Name     = "dgvPatients",
                Size     = new System.Drawing.Size(920, 340),
                Location = new System.Drawing.Point(16, 242),
                BackgroundColor  = clrWhite,
                BorderStyle      = System.Windows.Forms.BorderStyle.None,
                CellBorderStyle  = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor        = System.Drawing.Color.FromArgb(210, 225, 245),
                RowHeadersVisible= false,
                Font             = new System.Drawing.Font("Segoe UI", 9f),
                DefaultCellStyle = { SelectionBackColor = System.Drawing.Color.FromArgb(200, 225, 255),
                                     SelectionForeColor = clrText }
            };
            dgvPatients.SelectionChanged += new System.EventHandler(this.dgvPatients_SelectionChanged);
            this.Controls.Add(dgvPatients);

            lblCount = new System.Windows.Forms.Label
            {
                Text      = "",
                ForeColor = System.Drawing.Color.FromArgb(90, 120, 155),
                Font      = new System.Drawing.Font("Segoe UI", 8.5f),
                AutoSize  = true,
                Location  = new System.Drawing.Point(16, 590)
            };
            this.Controls.Add(lblCount);

            this.ResumeLayout(false);
        }

        // ── Helper factories ─────────────────────────────────────────────────
        private static System.Windows.Forms.Label MakeLabel(
            string text, int x, int y, System.Drawing.Color fore)
        {
            return new System.Windows.Forms.Label
            {
                Text      = text,
                ForeColor = fore,
                Font      = new System.Drawing.Font("Segoe UI", 8.5f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Size      = new System.Drawing.Size(200, 18),
                Location  = new System.Drawing.Point(x, y)
            };
        }

        private static System.Windows.Forms.TextBox MakeTextBox(
            string name, int x, int y, int w, int h,
            System.Drawing.Color back, System.Drawing.Color fore)
        {
            return new System.Windows.Forms.TextBox
            {
                Name        = name,
                Size        = new System.Drawing.Size(w, h),
                Location    = new System.Drawing.Point(x, y),
                Font        = new System.Drawing.Font("Segoe UI", 9.5f),
                BackColor   = back,
                ForeColor   = fore,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };
        }

        private static System.Windows.Forms.Button MakeButton(
            string name, string text, int x, int y,
            System.Drawing.Color back, System.Drawing.Color fore)
        {
            var b = new System.Windows.Forms.Button
            {
                Name      = name,
                Text      = text,
                Size      = new System.Drawing.Size(108, 32),
                Location  = new System.Drawing.Point(x, y),
                Font      = new System.Drawing.Font("Segoe UI", 9f,
                                System.Drawing.FontStyle.Bold),
                BackColor = back,
                ForeColor = fore,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor    = System.Windows.Forms.Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }
    }
}

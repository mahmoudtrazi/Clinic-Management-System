// ============================================================
//  ReportsForm.Designer.cs  –  Forms/ReportsForm.Designer.cs
//  KPI summary cards + treatment breakdown grid.
// ============================================================
namespace ClinicManagementSystem.Forms
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel        pnlHeader;
        private System.Windows.Forms.Label        lblTitle;
        private System.Windows.Forms.Panel        pnlCards;
        // Card 1 – Total Patients
        private System.Windows.Forms.Panel        cardPatients;
        private System.Windows.Forms.Label        lblCardPatientsIcon;
        private System.Windows.Forms.Label        lblTotalPatients;
        private System.Windows.Forms.Label        lblCardPatientsLabel;
        // Card 2 – Total Appointments
        private System.Windows.Forms.Panel        cardAppts;
        private System.Windows.Forms.Label        lblCardApptsIcon;
        private System.Windows.Forms.Label        lblTotalAppointments;
        private System.Windows.Forms.Label        lblCardApptsLabel;
        // Card 3 – Total Revenue
        private System.Windows.Forms.Panel        cardRevenue;
        private System.Windows.Forms.Label        lblCardRevenueIcon;
        private System.Windows.Forms.Label        lblTotalRevenue;
        private System.Windows.Forms.Label        lblCardRevenueLabel;
        // Card 4 – Most Common
        private System.Windows.Forms.Panel        cardCommon;
        private System.Windows.Forms.Label        lblCardCommonIcon;
        private System.Windows.Forms.Label        lblMostCommon;
        private System.Windows.Forms.Label        lblCardCommonLabel;

        private System.Windows.Forms.Label        lblBreakdownTitle;
        private System.Windows.Forms.DataGridView dgvSummary;
        private System.Windows.Forms.Button       btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var clrBrown = System.Drawing.Color.FromArgb(121, 85, 72);
            var clrDark  = System.Drawing.Color.FromArgb(78, 52, 46);
            var clrLight = System.Drawing.Color.FromArgb(250, 247, 245);
            var clrWhite = System.Drawing.Color.White;
            var clrText  = System.Drawing.Color.FromArgb(30, 58, 95);

            this.SuspendLayout();

            this.Text          = "Reports & Statistics";
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
                Text = "📊  Reports & Statistics", ForeColor = clrWhite,
                Font = new System.Drawing.Font("Segoe UI", 15f, System.Drawing.FontStyle.Bold),
                AutoSize = false, Dock = System.Windows.Forms.DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Padding = new System.Windows.Forms.Padding(20, 0, 0, 0)
            };
            pnlHeader.Controls.Add(lblTitle);

            // ── Cards ─────────────────────────────────────────────────────────
            // Assign labels first (cannot assign fields inside array initializer)
            lblTotalPatients     = new System.Windows.Forms.Label();
            lblTotalAppointments = new System.Windows.Forms.Label();
            lblTotalRevenue      = new System.Windows.Forms.Label();
            lblMostCommon        = new System.Windows.Forms.Label();

            var cardDefs = new (string Icon, string Label, System.Drawing.Color Bg,
                                System.Windows.Forms.Label ValueLabel)[]
            {
                ("👤", "Total Patients",        System.Drawing.Color.FromArgb(21,  101, 192), lblTotalPatients),
                ("📅", "Total Appointments",    System.Drawing.Color.FromArgb(0,  131, 143),  lblTotalAppointments),
                ("💰", "Total Revenue",         System.Drawing.Color.FromArgb(56,  142, 60),  lblTotalRevenue),
                ("🏆", "Most Common Treatment", System.Drawing.Color.FromArgb(121,  85, 72),  lblMostCommon),
            };

            int cardX = 16;
            foreach (var (Icon, Label, Bg, ValueLabel) in cardDefs)
            {
                var card = new System.Windows.Forms.Panel
                {
                    Size      = new System.Drawing.Size(218, 120),
                    Location  = new System.Drawing.Point(cardX, 76),
                    BackColor = Bg
                };

                var icon = new System.Windows.Forms.Label
                {
                    Text      = Icon,
                    ForeColor = System.Drawing.Color.FromArgb(180, 255, 255, 255),
                    Font      = new System.Drawing.Font("Segoe UI", 26f),
                    AutoSize  = false,
                    Size      = new System.Drawing.Size(60, 60),
                    Location  = new System.Drawing.Point(10, 10),
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };

                ValueLabel.Text      = "…";
                ValueLabel.ForeColor = clrWhite;
                ValueLabel.Font      = new System.Drawing.Font("Segoe UI", 22f, System.Drawing.FontStyle.Bold);
                ValueLabel.AutoSize  = false;
                ValueLabel.Size      = new System.Drawing.Size(218, 40);
                ValueLabel.Location  = new System.Drawing.Point(0, 38);
                ValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                var lbl = new System.Windows.Forms.Label
                {
                    Text      = Label,
                    ForeColor = System.Drawing.Color.FromArgb(200, 255, 255, 255),
                    Font      = new System.Drawing.Font("Segoe UI", 8.5f),
                    AutoSize  = false,
                    Size      = new System.Drawing.Size(218, 25),
                    Location  = new System.Drawing.Point(0, 80),
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };

                card.Controls.AddRange(new System.Windows.Forms.Control[]
                    { icon, ValueLabel, lbl });
                this.Controls.Add(card);
                cardX += 234;
            }

            // ── Breakdown Label ───────────────────────────────────────────────
            lblBreakdownTitle = new System.Windows.Forms.Label
            {
                Text = "Treatment Breakdown",
                ForeColor = System.Drawing.Color.FromArgb(90, 60, 50),
                Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold),
                AutoSize = true, Location = new System.Drawing.Point(16, 210)
            };
            this.Controls.Add(lblBreakdownTitle);

            // ── Breakdown Grid ────────────────────────────────────────────────
            dgvSummary = new System.Windows.Forms.DataGridView
            {
                Name = "dgvSummary",
                Size = new System.Drawing.Size(940, 380),
                Location = new System.Drawing.Point(16, 240),
                BackgroundColor = clrWhite,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = System.Drawing.Color.FromArgb(220, 205, 200),
                RowHeadersVisible = false,
                Font = new System.Drawing.Font("Segoe UI", 9f)
            };
            this.Controls.Add(dgvSummary);

            // ── Refresh Button ────────────────────────────────────────────────
            btnRefresh = new System.Windows.Forms.Button
            {
                Text = "🔄  Refresh",
                Size = new System.Drawing.Size(140, 36),
                Location = new System.Drawing.Point(820, 205),
                Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold),
                BackColor = clrBrown, ForeColor = clrWhite,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.Controls.Add(btnRefresh);

            this.ResumeLayout(false);
        }
    }
}

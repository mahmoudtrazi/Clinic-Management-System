// ============================================================
//  DashboardForm.Designer.cs  –  Forms/DashboardForm.Designer.cs
//  Navigation dashboard – blue top header + tile grid.
// ============================================================
namespace ClinicManagementSystem.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel  pnlHeader;
        private System.Windows.Forms.Label  lblAppName;
        private System.Windows.Forms.Label  lblGreeting;
        private System.Windows.Forms.Label  lblDateTime;
        private System.Windows.Forms.Panel  pnlBody;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Button btnAppointments;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var clrDark    = System.Drawing.Color.FromArgb(13, 71, 161);
            var clrMid     = System.Drawing.Color.FromArgb(21, 101, 192);
            var clrLight   = System.Drawing.Color.FromArgb(245, 248, 252);
            var clrWhite   = System.Drawing.Color.White;
            var clrText    = System.Drawing.Color.FromArgb(30, 58, 95);

            this.SuspendLayout();

            this.Text            = "ClinicCare – Dashboard";
            this.Size            = new System.Drawing.Size(1000, 640);
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = clrLight;
            this.Font            = new System.Drawing.Font("Segoe UI", 9f);

            // ── Header ────────────────────────────────────────────────────────
            pnlHeader = new System.Windows.Forms.Panel
            {
                Dock      = System.Windows.Forms.DockStyle.Top,
                Height    = 80,
                BackColor = clrDark
            };
            this.Controls.Add(pnlHeader);

            lblAppName = new System.Windows.Forms.Label
            {
                Text      = "✚  ClinicCare  |  Patient Management System",
                ForeColor = clrWhite,
                Font      = new System.Drawing.Font("Segoe UI", 16f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Size      = new System.Drawing.Size(580, 80),
                Location  = new System.Drawing.Point(24, 0),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            pnlHeader.Controls.Add(lblAppName);

            lblGreeting = new System.Windows.Forms.Label
            {
                Text      = "",
                ForeColor = System.Drawing.Color.FromArgb(180, 220, 255),
                Font      = new System.Drawing.Font("Segoe UI", 10f),
                AutoSize  = false,
                Size      = new System.Drawing.Size(360, 35),
                Location  = new System.Drawing.Point(620, 10),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight
            };
            pnlHeader.Controls.Add(lblGreeting);

            lblDateTime = new System.Windows.Forms.Label
            {
                Text      = "",
                ForeColor = System.Drawing.Color.FromArgb(130, 175, 230),
                Font      = new System.Drawing.Font("Segoe UI", 8.5f),
                AutoSize  = false,
                Size      = new System.Drawing.Size(360, 28),
                Location  = new System.Drawing.Point(620, 46),
                TextAlign = System.Drawing.ContentAlignment.MiddleRight
            };
            pnlHeader.Controls.Add(lblDateTime);

            // ── Section Label ─────────────────────────────────────────────────
            var lblSection = new System.Windows.Forms.Label
            {
                Text      = "Quick Access",
                ForeColor = System.Drawing.Color.FromArgb(90, 120, 155),
                Font      = new System.Drawing.Font("Segoe UI", 11f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = true,
                Location  = new System.Drawing.Point(48, 110)
            };
            this.Controls.Add(lblSection);

            // ── Tiles ─────────────────────────────────────────────────────────
            // (icon, label, x, y, color, event)
            var tiles = new (string Icon, string Title, int X, int Y,
                             System.Drawing.Color Bg, System.EventHandler Handler)[]
            {
                ("👤", "Patients\nManagement",    48,  150, System.Drawing.Color.FromArgb(21, 101, 192), btnPatients_Click),
                ("📅", "Appointments\nManagement",290, 150, System.Drawing.Color.FromArgb(0, 131, 143),  btnAppointments_Click),
                ("🔍", "Search\n& Filter",        532, 150, System.Drawing.Color.FromArgb(56, 142, 60),  btnSearch_Click),
                ("📊", "Reports\n& Statistics",   774, 150, System.Drawing.Color.FromArgb(121, 85, 72),  btnReports_Click),
            };

            foreach (var t in tiles)
            {
                var tile = new System.Windows.Forms.Button
                {
                    Size      = new System.Drawing.Size(200, 180),
                    Location  = new System.Drawing.Point(t.X, t.Y),
                    Text      = $"{t.Icon}\n\n{t.Title}",
                    ForeColor = clrWhite,
                    BackColor = t.Bg,
                    Font      = new System.Drawing.Font("Segoe UI", 13f,
                                    System.Drawing.FontStyle.Bold),
                    FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                    Cursor    = System.Windows.Forms.Cursors.Hand,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };
                tile.FlatAppearance.BorderSize = 0;
                tile.Click += t.Handler;
                this.Controls.Add(tile);
            }

            // Note: the tile buttons are created inside the foreach loop above.
            // The private fields btnPatients/btnAppointments/btnSearch/btnReports
            // are declared in the Designer but the actual visible buttons are
            // the 'tile' locals created in the loop — this is intentional.

            // ── Logout Button ─────────────────────────────────────────────────
            btnLogout = new System.Windows.Forms.Button
            {
                Text      = "⬅  Logout",
                Size      = new System.Drawing.Size(150, 40),
                Location  = new System.Drawing.Point(825, 560),
                Font      = new System.Drawing.Font("Segoe UI", 9.5f),
                BackColor = System.Drawing.Color.FromArgb(198, 40, 40),
                ForeColor = clrWhite,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor    = System.Windows.Forms.Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            this.Controls.Add(btnLogout);

            this.ResumeLayout(false);
        }
    }
}

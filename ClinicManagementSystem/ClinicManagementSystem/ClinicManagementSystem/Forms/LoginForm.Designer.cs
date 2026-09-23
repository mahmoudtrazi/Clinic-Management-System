// ============================================================
//  LoginForm.Designer.cs  –  Forms/LoginForm.Designer.cs
//  Auto-generated designer code for the Login Form.
//  Blue & White medical clinic theme.
// ============================================================
namespace ClinicManagementSystem.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Panel      pnlLeft;
        private System.Windows.Forms.Panel      pnlRight;
        private System.Windows.Forms.Label      lblBrand;
        private System.Windows.Forms.Label      lblTagline;
        private System.Windows.Forms.Label      lblIcon;
        private System.Windows.Forms.Panel      pnlCard;
        private System.Windows.Forms.Label      lblWelcome;
        private System.Windows.Forms.Label      lblSubtitle;
        private System.Windows.Forms.Label      lblUsername;
        private System.Windows.Forms.TextBox    txtUsername;
        private System.Windows.Forms.Label      lblPassword;
        private System.Windows.Forms.TextBox    txtPassword;
        private System.Windows.Forms.Button     btnLogin;
        private System.Windows.Forms.Button     btnExit;
        private System.Windows.Forms.Label      lblVersion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Colors ───────────────────────────────────────────────────────
            var clrDarkBlue  = System.Drawing.Color.FromArgb(13,  71, 161);   // #0D47A1
            var clrMidBlue   = System.Drawing.Color.FromArgb(21, 101, 192);   // #1565C0
            var clrAccent    = System.Drawing.Color.FromArgb(30, 136, 229);   // #1E88E5
            var clrWhite     = System.Drawing.Color.White;
            var clrLightGray = System.Drawing.Color.FromArgb(245, 248, 252);
            var clrTextDark  = System.Drawing.Color.FromArgb(30,  58,  95);
            var clrTextMid   = System.Drawing.Color.FromArgb(90, 120, 155);

            this.SuspendLayout();

            // ── Form ─────────────────────────────────────────────────────────
            this.Text            = "Clinic Management System – Login";
            this.Size            = new System.Drawing.Size(900, 560);
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = clrWhite;
            this.Font            = new System.Drawing.Font("Segoe UI", 9f);
            this.Name            = "LoginForm";

            // ── Left Panel (blue branding stripe) ────────────────────────────
            pnlLeft = new System.Windows.Forms.Panel
            {
                Dock      = System.Windows.Forms.DockStyle.Left,
                Width     = 380,
                BackColor = clrDarkBlue
            };
            this.Controls.Add(pnlLeft);

            // Cross / plus icon (medical)
            lblIcon = new System.Windows.Forms.Label
            {
                Text      = "✚",
                ForeColor = System.Drawing.Color.FromArgb(100, 180, 255),
                Font      = new System.Drawing.Font("Segoe UI", 56f,
                                System.Drawing.FontStyle.Regular),
                AutoSize  = false,
                Size      = new System.Drawing.Size(380, 100),
                Location  = new System.Drawing.Point(0, 110),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            pnlLeft.Controls.Add(lblIcon);

            lblBrand = new System.Windows.Forms.Label
            {
                Text      = "ClinicCare",
                ForeColor = clrWhite,
                Font      = new System.Drawing.Font("Segoe UI", 28f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Size      = new System.Drawing.Size(380, 50),
                Location  = new System.Drawing.Point(0, 220),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            pnlLeft.Controls.Add(lblBrand);

            lblTagline = new System.Windows.Forms.Label
            {
                Text      = "Patient Management System",
                ForeColor = System.Drawing.Color.FromArgb(160, 210, 255),
                Font      = new System.Drawing.Font("Segoe UI", 11f,
                                System.Drawing.FontStyle.Italic),
                AutoSize  = false,
                Size      = new System.Drawing.Size(380, 30),
                Location  = new System.Drawing.Point(0, 275),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            pnlLeft.Controls.Add(lblTagline);

            // Decorative lines
            var line1 = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 255, 255, 255),
                Size      = new System.Drawing.Size(200, 1),
                Location  = new System.Drawing.Point(90, 320)
            };
            pnlLeft.Controls.Add(line1);

            var lblCopy = new System.Windows.Forms.Label
            {
                Text      = "© 2025  Clinic Management System",
                ForeColor = System.Drawing.Color.FromArgb(120, 160, 210),
                Font      = new System.Drawing.Font("Segoe UI", 8f),
                AutoSize  = false,
                Size      = new System.Drawing.Size(380, 25),
                Location  = new System.Drawing.Point(0, 490),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            pnlLeft.Controls.Add(lblCopy);

            // ── Right Panel (white login card) ────────────────────────────────
            pnlRight = new System.Windows.Forms.Panel
            {
                Dock      = System.Windows.Forms.DockStyle.Fill,
                BackColor = clrLightGray
            };
            this.Controls.Add(pnlRight);
            pnlRight.BringToFront();

            // ── Login Card ────────────────────────────────────────────────────
            pnlCard = new System.Windows.Forms.Panel
            {
                Size      = new System.Drawing.Size(380, 390),
                Location  = new System.Drawing.Point(70, 80),
                BackColor = clrWhite,
                // Simulate drop-shadow with a border
            };
            pnlRight.Controls.Add(pnlCard);

            // Card border hack via container panel
            var pnlShadow = new System.Windows.Forms.Panel
            {
                Size      = new System.Drawing.Size(386, 396),
                Location  = new System.Drawing.Point(67, 77),
                BackColor = System.Drawing.Color.FromArgb(200, 220, 240)
            };
            pnlRight.Controls.Add(pnlShadow);
            pnlRight.Controls.SetChildIndex(pnlCard, 0);    // card on top

            // ── Welcome Labels ────────────────────────────────────────────────
            lblWelcome = new System.Windows.Forms.Label
            {
                Text      = "Welcome Back",
                ForeColor = clrTextDark,
                Font      = new System.Drawing.Font("Segoe UI", 20f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Size      = new System.Drawing.Size(340, 40),
                Location  = new System.Drawing.Point(20, 28),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            pnlCard.Controls.Add(lblWelcome);

            lblSubtitle = new System.Windows.Forms.Label
            {
                Text      = "Sign in to your account",
                ForeColor = clrTextMid,
                Font      = new System.Drawing.Font("Segoe UI", 9.5f),
                AutoSize  = false,
                Size      = new System.Drawing.Size(340, 22),
                Location  = new System.Drawing.Point(20, 70),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            pnlCard.Controls.Add(lblSubtitle);

            var divider = new System.Windows.Forms.Panel
            {
                BackColor = System.Drawing.Color.FromArgb(210, 225, 245),
                Size      = new System.Drawing.Size(340, 1),
                Location  = new System.Drawing.Point(20, 98)
            };
            pnlCard.Controls.Add(divider);

            // ── Username ──────────────────────────────────────────────────────
            lblUsername = new System.Windows.Forms.Label
            {
                Text      = "USERNAME",
                ForeColor = clrTextMid,
                Font      = new System.Drawing.Font("Segoe UI", 7.5f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Size      = new System.Drawing.Size(340, 20),
                Location  = new System.Drawing.Point(20, 116)
            };
            pnlCard.Controls.Add(lblUsername);

            txtUsername = new System.Windows.Forms.TextBox
            {
                Name      = "txtUsername",
                Size      = new System.Drawing.Size(340, 36),
                Location  = new System.Drawing.Point(20, 138),
                Font      = new System.Drawing.Font("Segoe UI", 10.5f),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                BackColor = clrLightGray,
                ForeColor = clrTextDark
            };
            txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            pnlCard.Controls.Add(txtUsername);

            // ── Password ──────────────────────────────────────────────────────
            lblPassword = new System.Windows.Forms.Label
            {
                Text      = "PASSWORD",
                ForeColor = clrTextMid,
                Font      = new System.Drawing.Font("Segoe UI", 7.5f,
                                System.Drawing.FontStyle.Bold),
                AutoSize  = false,
                Size      = new System.Drawing.Size(340, 20),
                Location  = new System.Drawing.Point(20, 192)
            };
            pnlCard.Controls.Add(lblPassword);

            txtPassword = new System.Windows.Forms.TextBox
            {
                Name        = "txtPassword",
                Size        = new System.Drawing.Size(340, 36),
                Location    = new System.Drawing.Point(20, 214),
                Font        = new System.Drawing.Font("Segoe UI", 10.5f),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                BackColor   = clrLightGray,
                ForeColor   = clrTextDark,
                UseSystemPasswordChar = false   // set PasswordChar in .cs
            };
            txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            pnlCard.Controls.Add(txtPassword);

            // ── Login Button ──────────────────────────────────────────────────
            btnLogin = new System.Windows.Forms.Button
            {
                Name      = "btnLogin",
                Text      = "LOGIN",
                Size      = new System.Drawing.Size(340, 44),
                Location  = new System.Drawing.Point(20, 278),
                Font      = new System.Drawing.Font("Segoe UI", 11f,
                                System.Drawing.FontStyle.Bold),
                BackColor = clrMidBlue,
                ForeColor = clrWhite,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor    = System.Windows.Forms.Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize     = 0;
            btnLogin.FlatAppearance.MouseOverBackColor  =
                System.Drawing.Color.FromArgb(13, 71, 161);
            btnLogin.FlatAppearance.MouseDownBackColor  =
                System.Drawing.Color.FromArgb(10, 50, 130);
            btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            pnlCard.Controls.Add(btnLogin);

            // ── Exit Button ───────────────────────────────────────────────────
            btnExit = new System.Windows.Forms.Button
            {
                Name      = "btnExit",
                Text      = "EXIT",
                Size      = new System.Drawing.Size(340, 36),
                Location  = new System.Drawing.Point(20, 332),
                Font      = new System.Drawing.Font("Segoe UI", 9f),
                BackColor = System.Drawing.Color.FromArgb(240, 244, 250),
                ForeColor = clrTextMid,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Cursor    = System.Windows.Forms.Cursors.Hand
            };
            btnExit.FlatAppearance.BorderColor    = System.Drawing.Color.FromArgb(210, 225, 245);
            btnExit.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(220, 232, 248);
            btnExit.Click += new System.EventHandler(this.btnExit_Click);
            pnlCard.Controls.Add(btnExit);

            // ── Version label ─────────────────────────────────────────────────
            lblVersion = new System.Windows.Forms.Label
            {
                Text      = "v1.0.0",
                ForeColor = clrTextMid,
                Font      = new System.Drawing.Font("Segoe UI", 8f),
                AutoSize  = true,
                Location  = new System.Drawing.Point(33, 490)
            };
            pnlRight.Controls.Add(lblVersion);

            this.ResumeLayout(false);
        }
    }
}

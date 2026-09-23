// ============================================================
//  LoginForm.cs  –  Forms/LoginForm.cs
//  Medical Clinic – Login Screen
//  Blue / White professional theme.
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagementSystem.Data;

namespace ClinicManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        public string LoggedInUsername { get; private set; } = string.Empty;

        // ── Constructor ──────────────────────────────────────────────────────
        public LoginForm()
        {
            InitializeComponent();
            ApplyTheme();
            this.ActiveControl = txtUsername;   // Focus on username field
        }

        // ── Theme / Visual Customisation ─────────────────────────────────────

        private void ApplyTheme()
        {
            // Reveal the password-char after a short delay (nicer UX)
            txtPassword.PasswordChar = '●';

            // Set placeholder-style grey text
            SetPlaceholder(txtUsername, "Enter username…");
        }

        // ── Placeholder helpers ──────────────────────────────────────────────

        private void SetPlaceholder(TextBox tb, string placeholder)
        {
            tb.ForeColor = Color.Gray;
            tb.Text      = placeholder;

            tb.GotFocus  += (s, e) =>
            {
                if (tb.Text == placeholder)
                { tb.Text = ""; tb.ForeColor = Color.FromArgb(30, 58, 95); }
            };
            tb.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                { tb.Text = placeholder; tb.ForeColor = Color.Gray; }
            };
        }

        // ── btnLogin_Click ───────────────────────────────────────────────────

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Basic empty-field guard
            if (string.IsNullOrWhiteSpace(username) ||
                username == "Enter username…" ||
                string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter both username and password.");
                return;
            }

            // Animate the button while checking
            btnLogin.Enabled = false;
            btnLogin.Text    = "Logging in…";

            try
            {
                bool success = DatabaseManager.Instance.ValidateLogin(username, password);

                if (success)
                {
                    LoggedInUsername = username;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowError("Invalid username or password. Please try again.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text    = "LOGIN";
            }
        }

        // ── btnExit_Click ────────────────────────────────────────────────────

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ── Allow pressing Enter in password box ─────────────────────────────

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // prevents the 'ding'
                PerformLogin();
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPassword.Focus();
            }
        }

        // ── Helper ───────────────────────────────────────────────────────────

        private void ShowError(string msg) =>
            MessageBox.Show(msg, "Login Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}

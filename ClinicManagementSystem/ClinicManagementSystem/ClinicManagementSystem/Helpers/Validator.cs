// ============================================================
//  Validator.cs  –  Helpers/Validator.cs
//  Centralised, reusable validation methods.
// ============================================================
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClinicManagementSystem.Helpers
{
    /// <summary>
    /// Static utility class that validates form inputs.
    /// All methods display a MessageBox on failure and return false.
    /// </summary>
    public static class Validator
    {
        // ── IsNotEmpty ───────────────────────────────────────────────────────

        /// <summary>Returns false (and shows a message) if the value is blank.</summary>
        public static bool IsNotEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Show($"'{fieldName}' cannot be empty.");
                return false;
            }
            return true;
        }

        // ── IsValidAge ───────────────────────────────────────────────────────

        public static bool IsValidAge(string value, out int age)
        {
            if (!int.TryParse(value.Trim(), out age) || age <= 0 || age >= 150)
            {
                Show("Age must be a whole number between 1 and 149.");
                age = 0;
                return false;
            }
            return true;
        }

        // ── IsValidPhone ─────────────────────────────────────────────────────

        /// <summary>
        /// Accepts 7-15 digit phone numbers, optionally starting with +.
        /// </summary>
        public static bool IsValidPhone(string value)
        {
            var cleaned = value.Trim();
            if (!Regex.IsMatch(cleaned, @"^\+?\d{7,15}$"))
            {
                Show("Phone number must contain 7–15 digits and may start with '+'.");
                return false;
            }
            return true;
        }

        // ── IsValidDecimal ───────────────────────────────────────────────────

        public static bool IsValidDecimal(string value, string fieldName, out decimal result)
        {
            if (!decimal.TryParse(value.Trim(), out result) || result < 0)
            {
                Show($"'{fieldName}' must be a non-negative number (e.g. 150.00).");
                result = 0;
                return false;
            }
            return true;
        }

        // ── IsValidDate ──────────────────────────────────────────────────────

        public static bool IsValidDate(string value, string fieldName, out DateTime result)
        {
            if (!DateTime.TryParse(value.Trim(), out result))
            {
                Show($"'{fieldName}' is not a valid date/time.");
                result = DateTime.MinValue;
                return false;
            }
            return true;
        }

        // ── AllNotEmpty (convenience overload) ───────────────────────────────

        /// <summary>
        /// Returns true only when every (value, fieldName) pair is non-empty.
        /// Pass values and names as alternating elements:
        ///   AllNotEmpty(txt1, "Name", txt2, "Phone")
        /// </summary>
        public static bool AllNotEmpty(params string[] valuesAndNames)
        {
            if (valuesAndNames.Length % 2 != 0)
                throw new ArgumentException("Provide value/name pairs.");

            for (int i = 0; i < valuesAndNames.Length; i += 2)
            {
                if (!IsNotEmpty(valuesAndNames[i], valuesAndNames[i + 1]))
                    return false;
            }
            return true;
        }

        // ── Private helpers ──────────────────────────────────────────────────

        private static void Show(string msg) =>
            MessageBox.Show(msg, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}

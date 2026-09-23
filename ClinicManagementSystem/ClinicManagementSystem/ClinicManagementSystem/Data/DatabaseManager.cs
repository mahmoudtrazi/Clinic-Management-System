// ============================================================
//  DatabaseManager.cs  –  Data/DatabaseManager.cs
//  Central database access class (Singleton).
//  Handles connection, CRUD operations, and query execution.
// ============================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Data
{
    /// <summary>
    /// Singleton class that manages all SQL Server database operations.
    /// Update the CONNECTION_STRING constant to match your environment.
    /// </summary>
    public sealed class DatabaseManager
    {
        // ── Connection String ────────────────────────────────────────────────
        // ⚠  Change Server= to your SQL Server instance name.
        // e.g.  "Server=.\SQLEXPRESS;..."  or  "Server=localhost;..."
        private const string CONNECTION_STRING =
            "Server=localhost\\SQLEXPRESS;" +
            "Database=ClinicManagementDB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;" +
            "Connection Timeout=30;";

        // ── Singleton ────────────────────────────────────────────────────────
        private static DatabaseManager? _instance;
        public static DatabaseManager Instance =>
            _instance ??= new DatabaseManager();

        private DatabaseManager() { }   // private ctor

        // ── Connection Helpers ───────────────────────────────────────────────

        /// <summary>Opens and returns a new SqlConnection.</summary>
        public SqlConnection OpenConnection()
        {
            var conn = new SqlConnection(CONNECTION_STRING);
            conn.Open();
            return conn;
        }

        /// <summary>Safely closes a connection if it is not already closed.</summary>
        public void CloseConnection(SqlConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
                conn.Close();
        }

        /// <summary>Tests that the database is reachable.</summary>
        public bool TestConnection()
        {
            try
            {
                using var conn = OpenConnection();
                return conn.State == ConnectionState.Open;
            }
            catch { return false; }
        }

        // ── Generic Execute Helpers ──────────────────────────────────────────

        /// <summary>Executes a non-query command (INSERT/UPDATE/DELETE).</summary>
        public bool ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using var conn = OpenConnection();
                using var cmd  = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>Executes a SELECT and returns a DataTable.</summary>
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            try
            {
                using var conn    = OpenConnection();
                using var cmd     = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>Executes a SELECT and returns a single scalar value.</summary>
        public object? ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            try
            {
                using var conn = OpenConnection();
                using var cmd  = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  AUTHENTICATION
        // ════════════════════════════════════════════════════════════════════

        /// <summary>Returns true if credentials match a Users row.</summary>
        public bool ValidateLogin(string username, string password)
        {
            const string sql =
                "SELECT COUNT(*) FROM Users " +
                "WHERE Username = @Username AND PasswordHash = @Password";

            var result = ExecuteScalar(sql,
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password));

            return result != null && Convert.ToInt32(result) > 0;
        }

        // ════════════════════════════════════════════════════════════════════
        //  PATIENTS – CRUD
        // ════════════════════════════════════════════════════════════════════

        public DataTable GetAllPatients()
        {
            return ExecuteQuery(
                "SELECT PatientID, FullName, Age, Gender, PhoneNumber, CreatedDate " +
                "FROM Patients ORDER BY FullName");
        }

        public bool AddPatient(Patient p)
        {
            const string sql =
                "INSERT INTO Patients (FullName, Age, Gender, PhoneNumber) " +
                "VALUES (@FullName, @Age, @Gender, @PhoneNumber)";

            return ExecuteNonQuery(sql,
                new SqlParameter("@FullName",    p.FullName),
                new SqlParameter("@Age",         p.Age),
                new SqlParameter("@Gender",      p.Gender),
                new SqlParameter("@PhoneNumber", p.PhoneNumber));
        }

        public bool UpdatePatient(Patient p)
        {
            const string sql =
                "UPDATE Patients SET FullName=@FullName, Age=@Age, " +
                "Gender=@Gender, PhoneNumber=@PhoneNumber " +
                "WHERE PatientID=@PatientID";

            return ExecuteNonQuery(sql,
                new SqlParameter("@FullName",    p.FullName),
                new SqlParameter("@Age",         p.Age),
                new SqlParameter("@Gender",      p.Gender),
                new SqlParameter("@PhoneNumber", p.PhoneNumber),
                new SqlParameter("@PatientID",   p.PatientID));
        }

        public bool DeletePatient(int patientID)
        {
            return ExecuteNonQuery(
                "DELETE FROM Patients WHERE PatientID=@PatientID",
                new SqlParameter("@PatientID", patientID));
        }

        public DataTable SearchPatientsByName(string name)
        {
            return ExecuteQuery(
                "SELECT PatientID, FullName, Age, Gender, PhoneNumber, CreatedDate " +
                "FROM Patients WHERE FullName LIKE @Name ORDER BY FullName",
                new SqlParameter("@Name", $"%{name}%"));
        }

        public DataTable SearchPatientsByPhone(string phone)
        {
            return ExecuteQuery(
                "SELECT PatientID, FullName, Age, Gender, PhoneNumber, CreatedDate " +
                "FROM Patients WHERE PhoneNumber LIKE @Phone ORDER BY FullName",
                new SqlParameter("@Phone", $"%{phone}%"));
        }

        // ════════════════════════════════════════════════════════════════════
        //  APPOINTMENTS – CRUD
        // ════════════════════════════════════════════════════════════════════

        public DataTable GetAllAppointments()
        {
            return ExecuteQuery(
                "SELECT a.AppointmentID, p.FullName AS PatientName, " +
                "a.PatientID, a.AppointmentDate, a.TreatmentType, a.Cost " +
                "FROM Appointments a " +
                "INNER JOIN Patients p ON a.PatientID = p.PatientID " +
                "ORDER BY a.AppointmentDate DESC");
        }

        public bool AddAppointment(Appointment a)
        {
            const string sql =
                "INSERT INTO Appointments (PatientID, AppointmentDate, TreatmentType, Cost) " +
                "VALUES (@PatientID, @AppointmentDate, @TreatmentType, @Cost)";

            return ExecuteNonQuery(sql,
                new SqlParameter("@PatientID",       a.PatientID),
                new SqlParameter("@AppointmentDate", a.AppointmentDate),
                new SqlParameter("@TreatmentType",   a.TreatmentType),
                new SqlParameter("@Cost",            a.Cost));
        }

        public bool UpdateAppointment(Appointment a)
        {
            const string sql =
                "UPDATE Appointments SET PatientID=@PatientID, " +
                "AppointmentDate=@AppointmentDate, TreatmentType=@TreatmentType, " +
                "Cost=@Cost WHERE AppointmentID=@AppointmentID";

            return ExecuteNonQuery(sql,
                new SqlParameter("@PatientID",       a.PatientID),
                new SqlParameter("@AppointmentDate", a.AppointmentDate),
                new SqlParameter("@TreatmentType",   a.TreatmentType),
                new SqlParameter("@Cost",            a.Cost),
                new SqlParameter("@AppointmentID",   a.AppointmentID));
        }

        public bool DeleteAppointment(int appointmentID)
        {
            return ExecuteNonQuery(
                "DELETE FROM Appointments WHERE AppointmentID=@AppointmentID",
                new SqlParameter("@AppointmentID", appointmentID));
        }

        public DataTable FilterAppointmentsByTreatment(string treatment)
        {
            return ExecuteQuery(
                "SELECT a.AppointmentID, p.FullName AS PatientName, " +
                "a.PatientID, a.AppointmentDate, a.TreatmentType, a.Cost " +
                "FROM Appointments a " +
                "INNER JOIN Patients p ON a.PatientID = p.PatientID " +
                "WHERE a.TreatmentType LIKE @Treatment " +
                "ORDER BY a.AppointmentDate DESC",
                new SqlParameter("@Treatment", $"%{treatment}%"));
        }

        public DataTable FilterAppointmentsByDate(DateTime startDate, DateTime endDate)
        {
            return ExecuteQuery(
                "SELECT a.AppointmentID, p.FullName AS PatientName, " +
                "a.PatientID, a.AppointmentDate, a.TreatmentType, a.Cost " +
                "FROM Appointments a " +
                "INNER JOIN Patients p ON a.PatientID = p.PatientID " +
                "WHERE a.AppointmentDate BETWEEN @StartDate AND @EndDate " +
                "ORDER BY a.AppointmentDate DESC",
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate",   endDate));
        }

        // ════════════════════════════════════════════════════════════════════
        //  REPORTS / STATISTICS
        // ════════════════════════════════════════════════════════════════════

        public int GetTotalPatients()
        {
            var result = ExecuteScalar("SELECT COUNT(*) FROM Patients");
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public int GetTotalAppointments()
        {
            var result = ExecuteScalar("SELECT COUNT(*) FROM Appointments");
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public decimal GetTotalRevenue()
        {
            var result = ExecuteScalar("SELECT ISNULL(SUM(Cost), 0) FROM Appointments");
            return result == null ? 0m : Convert.ToDecimal(result);
        }

        public string GetMostCommonTreatment()
        {
            var result = ExecuteScalar(
                "SELECT TOP 1 TreatmentType FROM Appointments " +
                "GROUP BY TreatmentType ORDER BY COUNT(*) DESC");
            return result?.ToString() ?? "N/A";
        }

        /// <summary>
        /// Returns a DataTable with appointment count per treatment type,
        /// useful for charting or detailed report grids.
        /// </summary>
        public DataTable GetTreatmentSummary()
        {
            return ExecuteQuery(
                "SELECT TreatmentType, COUNT(*) AS AppointmentCount, " +
                "SUM(Cost) AS TotalRevenue " +
                "FROM Appointments " +
                "GROUP BY TreatmentType " +
                "ORDER BY AppointmentCount DESC");
        }

        // ── Patients dropdown helper ─────────────────────────────────────────
        public DataTable GetPatientList()
        {
            return ExecuteQuery(
                "SELECT PatientID, FullName FROM Patients ORDER BY FullName");
        }
    }
}

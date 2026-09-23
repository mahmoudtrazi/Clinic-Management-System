// ============================================================
//  Appointment.cs  –  Models/Appointment.cs
//  Plain C# class representing the Appointments table.
// ============================================================
using System;

namespace ClinicManagementSystem.Models
{
    /// <summary>
    /// Represents an appointment record from the Appointments table.
    /// </summary>
    public class Appointment
    {
        public int      AppointmentID   { get; set; }
        public int      PatientID       { get; set; }
        public string   PatientName     { get; set; } = string.Empty;   // Joined field
        public DateTime AppointmentDate { get; set; }
        public string   TreatmentType   { get; set; } = string.Empty;
        public decimal  Cost            { get; set; }

        public Appointment() { }

        public Appointment(int appointmentID, int patientID, string patientName,
                           DateTime appointmentDate, string treatmentType, decimal cost)
        {
            AppointmentID   = appointmentID;
            PatientID       = patientID;
            PatientName     = patientName;
            AppointmentDate = appointmentDate;
            TreatmentType   = treatmentType;
            Cost            = cost;
        }

        public override string ToString() =>
            $"{TreatmentType} on {AppointmentDate:dd/MM/yyyy} – {Cost:C}";
    }
}

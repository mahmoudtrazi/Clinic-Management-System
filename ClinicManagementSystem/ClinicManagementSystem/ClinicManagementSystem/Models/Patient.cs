// ============================================================
//  Patient.cs  –  Models/Patient.cs
//  Plain C# class representing the Patients table.
// ============================================================
using System;

namespace ClinicManagementSystem.Models
{
    /// <summary>
    /// Represents a patient record from the Patients table.
    /// </summary>
    public class Patient
    {
        public int       PatientID   { get; set; }
        public string    FullName    { get; set; } = string.Empty;
        public int       Age         { get; set; }
        public string    Gender      { get; set; } = string.Empty;
        public string    PhoneNumber { get; set; } = string.Empty;
        public DateTime  CreatedDate { get; set; } = DateTime.Now;

        // Parameterless constructor for object initializers
        public Patient() { }

        public Patient(int patientID, string fullName, int age,
                       string gender, string phoneNumber, DateTime createdDate)
        {
            PatientID   = patientID;
            FullName    = fullName;
            Age         = age;
            Gender      = gender;
            PhoneNumber = phoneNumber;
            CreatedDate = createdDate;
        }

        public override string ToString() => $"{FullName} (ID: {PatientID})";
    }
}

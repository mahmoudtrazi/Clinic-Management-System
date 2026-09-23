-- ============================================================
--  Clinic Management System - SQL Server Setup Script
--  File: ClinicDB_Setup.sql
--  Run this in SQL Server Management Studio (SSMS)
--  against your target SQL Server instance.
-- ============================================================

USE master;
GO

-- ── 1. Create Database ──────────────────────────────────────
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'ClinicManagementDB')
BEGIN
    ALTER DATABASE ClinicManagementDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ClinicManagementDB;
END
GO

CREATE DATABASE ClinicManagementDB
    COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

USE ClinicManagementDB;
GO

-- ── 2. Users Table (for Login) ──────────────────────────────
CREATE TABLE Users
(
    UserID       INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,   -- Store SHA-256 hex or plain for demo
    FullName     NVARCHAR(100) NOT NULL,
    Role         NVARCHAR(20)  NOT NULL DEFAULT 'Staff'   -- Admin / Staff
);
GO

-- ── 3. Patients Table ───────────────────────────────────────
CREATE TABLE Patients
(
    PatientID   INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(100) NOT NULL,
    Age         INT           NOT NULL CHECK (Age > 0 AND Age < 150),
    Gender      NVARCHAR(10)  NOT NULL CHECK (Gender IN ('Male', 'Female', 'Other')),
    PhoneNumber NVARCHAR(20)  NOT NULL,
    CreatedDate DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- ── 4. Appointments Table ───────────────────────────────────
CREATE TABLE Appointments
(
    AppointmentID   INT              NOT NULL IDENTITY(1,1) PRIMARY KEY,
    PatientID       INT              NOT NULL,
    AppointmentDate DATETIME         NOT NULL,
    TreatmentType   NVARCHAR(100)    NOT NULL,
    Cost            DECIMAL(10, 2)   NOT NULL CHECK (Cost >= 0),

    CONSTRAINT FK_Appointments_Patients
        FOREIGN KEY (PatientID)
        REFERENCES Patients(PatientID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- ── 5. Indexes ──────────────────────────────────────────────
CREATE INDEX IX_Appointments_PatientID
    ON Appointments(PatientID);

CREATE INDEX IX_Appointments_Date
    ON Appointments(AppointmentDate);

CREATE INDEX IX_Patients_FullName
    ON Patients(FullName);
GO

-- ── 6. Seed Data ────────────────────────────────────────────

-- Default admin account (password: admin123)
INSERT INTO Users (Username, PasswordHash, FullName, Role)
VALUES ('admin', 'admin123', 'System Administrator', 'Admin');

-- Default staff account (password: staff123)
INSERT INTO Users (Username, PasswordHash, FullName, Role)
VALUES ('staff', 'staff123', 'Clinic Staff', 'Staff');
GO

-- Sample Patients
INSERT INTO Patients (FullName, Age, Gender, PhoneNumber) VALUES
('Ahmed Al-Rashidi',   34, 'Male',   '0501234567'),
('Sara Mohammed',      28, 'Female', '0557654321'),
('Khalid Ibrahim',     45, 'Male',   '0509876543'),
('Fatima Al-Sayed',    52, 'Female', '0551122334'),
('Omar Hassan',        19, 'Male',   '0503344556');
GO

-- Sample Appointments
INSERT INTO Appointments (PatientID, AppointmentDate, TreatmentType, Cost) VALUES
(1, '2025-05-01 09:00', 'Teeth Cleaning',     150.00),
(1, '2025-06-15 10:30', 'Cavity Filling',     300.00),
(2, '2025-05-10 11:00', 'Root Canal',         800.00),
(3, '2025-05-20 14:00', 'Teeth Whitening',    400.00),
(4, '2025-05-25 09:30', 'Dental Extraction',  250.00),
(5, '2025-06-01 16:00', 'Orthodontics',      1200.00),
(2, '2025-06-10 13:00', 'Teeth Cleaning',     150.00);
GO

-- ── 7. Stored Procedures (optional helpers) ─────────────────

-- Get all appointments with patient name
CREATE PROCEDURE sp_GetAppointmentsWithPatient
AS
BEGIN
    SELECT
        a.AppointmentID,
        p.FullName          AS PatientName,
        a.AppointmentDate,
        a.TreatmentType,
        a.Cost,
        a.PatientID
    FROM Appointments a
    INNER JOIN Patients p ON a.PatientID = p.PatientID
    ORDER BY a.AppointmentDate DESC;
END
GO

-- Get report statistics
CREATE PROCEDURE sp_GetReportStats
AS
BEGIN
    SELECT
        (SELECT COUNT(*) FROM Patients)                        AS TotalPatients,
        (SELECT COUNT(*) FROM Appointments)                    AS TotalAppointments,
        (SELECT ISNULL(SUM(Cost), 0) FROM Appointments)        AS TotalRevenue,
        (SELECT TOP 1 TreatmentType
         FROM Appointments
         GROUP BY TreatmentType
         ORDER BY COUNT(*) DESC)                               AS MostCommonTreatment;
END
GO

PRINT '✅ ClinicManagementDB created and seeded successfully.';
GO

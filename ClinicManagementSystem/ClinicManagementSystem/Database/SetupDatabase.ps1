# SetupDatabase.ps1
# Runs the clinic SQL setup script against LocalDB using .NET SqlClient
# No sqlcmd needed!

param(
    [string]$Server = "(localdb)\MSSQLLocalDB"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  ClinicManagementDB - Setup Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Server: $Server" -ForegroundColor Yellow
Write-Host ""

# Load SQL Server assembly
Add-Type -AssemblyName "System.Data"

function Exec-Sql {
    param([string]$connStr, [string]$sql)
    $conn = New-Object System.Data.SqlClient.SqlConnection($connStr)
    $conn.Open()
    $cmd = $conn.CreateCommand()
    $cmd.CommandTimeout = 60
    $cmd.CommandText = $sql
    $cmd.ExecuteNonQuery() | Out-Null
    $conn.Close()
}

# ── Step 1: Create Database ──────────────────────────────────────────────────
Write-Host "[1/7] Creating database ClinicManagementDB..." -ForegroundColor White

$masterConn = "Server=$Server;Database=master;Integrated Security=True;TrustServerCertificate=True;"

try {
    Exec-Sql $masterConn @"
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'ClinicManagementDB')
BEGIN
    ALTER DATABASE ClinicManagementDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ClinicManagementDB;
END
"@
    Exec-Sql $masterConn "CREATE DATABASE ClinicManagementDB COLLATE SQL_Latin1_General_CP1_CI_AS;"
    Write-Host "    OK" -ForegroundColor Green
} catch {
    Write-Host "    ERROR: $_" -ForegroundColor Red
    exit 1
}

$dbConn = "Server=$Server;Database=ClinicManagementDB;Integrated Security=True;TrustServerCertificate=True;"

# ── Step 2: Users Table ───────────────────────────────────────────────────────
Write-Host "[2/7] Creating Users table..." -ForegroundColor White
try {
    Exec-Sql $dbConn @"
CREATE TABLE Users (
    UserID       INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    FullName     NVARCHAR(100) NOT NULL,
    Role         NVARCHAR(20)  NOT NULL DEFAULT 'Staff'
);
"@
    Write-Host "    OK" -ForegroundColor Green
} catch { Write-Host "    ERROR: $_" -ForegroundColor Red; exit 1 }

# ── Step 3: Patients Table ────────────────────────────────────────────────────
Write-Host "[3/7] Creating Patients table..." -ForegroundColor White
try {
    Exec-Sql $dbConn @"
CREATE TABLE Patients (
    PatientID   INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(100) NOT NULL,
    Age         INT           NOT NULL CHECK (Age > 0 AND Age < 150),
    Gender      NVARCHAR(10)  NOT NULL CHECK (Gender IN ('Male', 'Female', 'Other')),
    PhoneNumber NVARCHAR(20)  NOT NULL,
    CreatedDate DATETIME      NOT NULL DEFAULT GETDATE()
);
"@
    Write-Host "    OK" -ForegroundColor Green
} catch { Write-Host "    ERROR: $_" -ForegroundColor Red; exit 1 }

# ── Step 4: Appointments Table ────────────────────────────────────────────────
Write-Host "[4/7] Creating Appointments table..." -ForegroundColor White
try {
    Exec-Sql $dbConn @"
CREATE TABLE Appointments (
    AppointmentID   INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    PatientID       INT            NOT NULL,
    AppointmentDate DATETIME       NOT NULL,
    TreatmentType   NVARCHAR(100)  NOT NULL,
    Cost            DECIMAL(10,2)  NOT NULL CHECK (Cost >= 0),
    CONSTRAINT FK_Appointments_Patients
        FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
        ON DELETE CASCADE ON UPDATE CASCADE
);
"@
    Write-Host "    OK" -ForegroundColor Green
} catch { Write-Host "    ERROR: $_" -ForegroundColor Red; exit 1 }

# ── Step 5: Seed Users ────────────────────────────────────────────────────────
Write-Host "[5/7] Inserting default users..." -ForegroundColor White
try {
    Exec-Sql $dbConn "INSERT INTO Users (Username,PasswordHash,FullName,Role) VALUES ('admin','admin123','System Administrator','Admin');"
    Exec-Sql $dbConn "INSERT INTO Users (Username,PasswordHash,FullName,Role) VALUES ('staff','staff123','Clinic Staff','Staff');"
    Write-Host "    OK (admin / admin123)" -ForegroundColor Green
} catch { Write-Host "    ERROR: $_" -ForegroundColor Red; exit 1 }

# ── Step 6: Seed Patients ─────────────────────────────────────────────────────
Write-Host "[6/7] Inserting sample patients..." -ForegroundColor White
try {
    Exec-Sql $dbConn "INSERT INTO Patients (FullName,Age,Gender,PhoneNumber) VALUES ('Ahmed Al-Rashidi',34,'Male','0501234567');"
    Exec-Sql $dbConn "INSERT INTO Patients (FullName,Age,Gender,PhoneNumber) VALUES ('Sara Mohammed',28,'Female','0557654321');"
    Exec-Sql $dbConn "INSERT INTO Patients (FullName,Age,Gender,PhoneNumber) VALUES ('Khalid Ibrahim',45,'Male','0509876543');"
    Exec-Sql $dbConn "INSERT INTO Patients (FullName,Age,Gender,PhoneNumber) VALUES ('Fatima Al-Sayed',52,'Female','0551122334');"
    Exec-Sql $dbConn "INSERT INTO Patients (FullName,Age,Gender,PhoneNumber) VALUES ('Omar Hassan',19,'Male','0503344556');"
    Write-Host "    OK (5 patients)" -ForegroundColor Green
} catch { Write-Host "    ERROR: $_" -ForegroundColor Red; exit 1 }

# ── Step 7: Seed Appointments ─────────────────────────────────────────────────
Write-Host "[7/7] Inserting sample appointments..." -ForegroundColor White
try {
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (1,'2025-05-01 09:00','Teeth Cleaning',150.00);"
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (1,'2025-06-15 10:30','Cavity Filling',300.00);"
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (2,'2025-05-10 11:00','Root Canal',800.00);"
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (3,'2025-05-20 14:00','Teeth Whitening',400.00);"
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (4,'2025-05-25 09:30','Dental Extraction',250.00);"
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (5,'2025-06-01 16:00','Orthodontics',1200.00);"
    Exec-Sql $dbConn "INSERT INTO Appointments (PatientID,AppointmentDate,TreatmentType,Cost) VALUES (2,'2025-06-10 13:00','Teeth Cleaning',150.00);"
    Write-Host "    OK (7 appointments)" -ForegroundColor Green
} catch { Write-Host "    ERROR: $_" -ForegroundColor Red; exit 1 }

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Database setup COMPLETE!" -ForegroundColor Green
Write-Host "  Login: admin / admin123" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan

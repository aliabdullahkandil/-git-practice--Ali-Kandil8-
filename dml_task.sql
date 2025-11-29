-- 1) SELECT — Retrieve all columns from the Doctor table
SELECT *
FROM Doctor;


-- 2) ORDER BY — List patients in ascending order of their ages
SELECT *
FROM Patient
ORDER BY Age ASC;


-- 3) OFFSET FETCH — Retrieve the first 10 patients starting from the 5th record
SELECT *
FROM Patient
ORDER BY URNumber
OFFSET 5 ROWS FETCH NEXT 10 ROWS ONLY;


-- 4) SELECT TOP — Retrieve the top 5 doctors
SELECT TOP 5 *
FROM Doctor;


-- 5) SELECT DISTINCT — Get a list of unique addresses from the Patient table
SELECT DISTINCT Address
FROM Patient;


-- 6) WHERE — Patients aged 25
SELECT *
FROM Patient
WHERE Age = 25;


-- 7) NULL — Patients whose email is not provided
SELECT *
FROM Patient
WHERE Email IS NULL;


-- 8) AND — Doctors with experience > 5 years AND specialty = 'Cardiology'
SELECT *
FROM Doctor
WHERE YearsExperience > 5
  AND Specialty = 'Cardiology';


--9) IN — Doctors whose specialty is Dermatology or Oncology
SELECT *
FROM Doctor
WHERE Specialty IN ('Dermatology', 'Oncology');


--10) BETWEEN — Patients aged between 18 and 30
SELECT *
FROM Patient
WHERE Age BETWEEN 18 AND 30;


-- 11) LIKE — Doctors whose names start with 'Dr.'
SELECT *
FROM Doctor
WHERE Name LIKE 'Dr.%';


-- 12) Column & Table Aliases — Show doctor name and email with aliases
SELECT Name AS DoctorName,
       Email AS DoctorEmail
FROM Doctor;


-- 13) JOIN — Retrieve all prescriptions with corresponding patient names
SELECT pr.PrescriptionID, pr.Date, pr.Quantity, p.Name AS PatientName
FROM Prescription pr
JOIN Patient p ON pr.URNumber = p.URNumber;


--14) GROUP BY — Count of patients grouped by city (address)
SELECT Address AS City, COUNT(*) AS PatientCount
FROM Patient
GROUP BY Address;


--15) HAVING — Cities with more than 3 patients
SELECT Address AS City, COUNT(*) AS PatientCount
FROM Patient
GROUP BY Address
HAVING COUNT(*) > 3;


--16) GROUPING SETS — Count patients grouped by city and age
SELECT Address, Age, COUNT(*) AS Total
FROM Patient
GROUP BY GROUPING SETS (
    (Address),
    (Age),
    ()
);


--17) CUBE — All combinations of city and age
SELECT Address, Age, COUNT(*) AS Total
FROM Patient
GROUP BY CUBE (Address, Age);


--18) ROLLUP — Patient counts rolled up by city
SELECT Address, COUNT(*) AS Total
FROM Patient
GROUP BY ROLLUP (Address);


--19) EXISTS — Patients who have at least one prescription
SELECT *
FROM Patient p
WHERE EXISTS (
    SELECT 1
    FROM Prescription pr
    WHERE pr.URNumber = p.URNumber
);


--20) UNION — Combined list of doctor names and patient names
SELECT Name FROM Doctor
UNION
SELECT Name FROM Patient;


--21) Common Table Expression (CTE) — Patients and their doctors
WITH PatientDoctorCTE AS (
    SELECT p.Name AS PatientName, d.Name AS DoctorName
    FROM Patient p
    JOIN Doctor d ON p.PrimaryDoctorID = d.DoctorID
)
SELECT * FROM PatientDoctorCTE;


--22) INSERT — Insert a new doctor
INSERT INTO Doctor
VALUES (200, 'Dr. Hany', 'hany@mail.com', '0105555', 'Neurology', 12);


--23) INSERT Multiple Rows — Insert multiple patients
INSERT INTO Patient
VALUES
(10, 'Ali', 'Cairo', 22, 'ali@mail.com', '0123', NULL, 200),
(11, 'Mona', 'Giza', 35, 'mona@mail.com', '0114', 'MC555', 200),
(12, 'Yara', 'Alex', 29, NULL, '0107', NULL, 200);


--24) UPDATE — Update the phone number of a doctor
UPDATE Doctor
SET Phone = '0159999'
WHERE DoctorID = 200;


--25) UPDATE JOIN — Update the city of patients who received prescriptions from a specific doctor
UPDATE p
SET Address = 'New Cairo'
FROM Patient p
JOIN Prescription pr ON p.URNumber = pr.URNumber
WHERE pr.DoctorID = 200;


--26) DELETE — Delete a patient from the Patient table
DELETE FROM Patient
WHERE URNumber = 12;


--27) TRANSACTION — Insert a doctor and a patient safely together
BEGIN TRANSACTION;

INSERT INTO Doctor VALUES (300, 'Dr. Zain', 'z@mail.com', '012299', 'Pediatrics', 7);

INSERT INTO Patient VALUES (20, 'Karim', 'Alex', 27, 'k@mail.com', '010233', NULL, 300);

COMMIT;


--28) VIEW — Create a view combining patient and doctor info
CREATE VIEW vw_PatientDoctorInfo AS
SELECT p.Name AS Patient, d.Name AS Doctor, d.Specialty
FROM Patient p
JOIN Doctor d ON p.PrimaryDoctorID = d.DoctorID;


--29) INDEX — Create an index on Patient phone column
CREATE INDEX idx_PatientPhone
ON Patient(Phone);


--30) BACKUP — Backup the entire database
BACKUP DATABASE BarwonHealthDB
TO DISK = 'C:\Backups\BarwonHealthDB.bak';

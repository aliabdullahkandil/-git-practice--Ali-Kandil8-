-- 1) Create Employees Table
CREATE TABLE Employees (
    ID INT,
    Name VARCHAR(100),
    Salary DECIMAL(10,2)
);

-- 2) Add column Department
ALTER TABLE Employees
ADD Department VARCHAR(50);

-- 3) Remove Salary column
ALTER TABLE Employees
DROP COLUMN Salary;

-- 4) Rename Department → DeptName
EXEC sp_rename 'Employees.Department', 'DeptName', 'COLUMN';

-- 5) Create Projects table
CREATE TABLE Projects (
    ProjectID INT,
    ProjectName VARCHAR(100)
);

-- 6) Add Primary Key on Employees.ID
ALTER TABLE Employees
ADD CONSTRAINT PK_Employees_ID PRIMARY KEY (ID);

-- 7) Create Foreign Key (Employees → Projects)
ALTER TABLE Employees
ADD ProjectID INT;  
ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Projects
FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID);

-- 8) Remove the Foreign Key
ALTER TABLE Employees
DROP CONSTRAINT FK_Employees_Projects;

-- 9) Add UNIQUE on Name
ALTER TABLE Employees
ADD CONSTRAINT UQ_Employees_Name UNIQUE (Name);

-- 10) Create Customers table
CREATE TABLE Customers (
    CustomerID INT,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    Email VARCHAR(200),
    Status VARCHAR(50)
);

-- 11) UNIQUE on FirstName + LastName
ALTER TABLE Customers
ADD CONSTRAINT UQ_Customer_FullName UNIQUE (FirstName, LastName);

-- 12) Default value for Status = 'Active'
ALTER TABLE Customers
ADD CONSTRAINT DF_Customers_Status DEFAULT 'Active' FOR Status;

-- 13) Create Orders table
CREATE TABLE Orders (
    OrderID INT,
    CustomerID INT,
    OrderDate DATETIME,
    TotalAmount DECIMAL(10,2)
);

-- 14) Check TotalAmount > 0
ALTER TABLE Orders
ADD CONSTRAINT CHK_TotalAmount CHECK (TotalAmount > 0);

-- 15) Create schema Sales
CREATE SCHEMA Sales;

-- 16) Move Orders table to Sales schema
ALTER SCHEMA Sales TRANSFER dbo.Orders;

-- 17) Rename Orders → SalesOrders
EXEC sp_rename 'Sales.Orders', 'SalesOrders';
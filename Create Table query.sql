﻿--1
CREATE TABLE DEPARTMENT
	(
	Department_Id INT NOT NULL IDENTITY (1,1),
	Speciality VARCHAR(25) NOT NULL,
	[Location] NVARCHAR(200) NOT NULL,
	Contact NVARCHAR(15) NOT NULL,
	NoOfDoctors INT NULL,
	CONSTRAINT DEPARTMENT_PK PRIMARY KEY (Department_Id)
	);

--2
CREATE TABLE DOCTOR
	(
	Doctor_Id INT NOT NULL IDENTITY (1,1),
	FirstName VARCHAR(15) NOT NULL,
	LastName VARCHAR(15) NOT NULL,
	Contact NVARCHAR(15) NOT NULL,
	Dept_Id INT NULL,
	CONSTRAINT DOCTOR_PK PRIMARY KEY (Doctor_Id),
	CONSTRAINT DOCTOR_DEPARTMENT_FK FOREIGN KEY (Dept_Id) REFERENCES DEPARTMENT (Department_Id)
	ON DELETE SET DEFAULT ON UPDATE CASCADE
	);

--3
CREATE TABLE WARD
	(
	Ward_No INT NOT NULL IDENTITY (1,1),
	WardName NVARCHAR(40) NOT NULL,
	[Location] NVARCHAR(200) NOT NULL,
	NoOfStaff INT  NULL,
	NoOfNurses INT  NULL
	CONSTRAINT WARD_PK PRIMARY KEY (Ward_No)
	);

--4
CREATE TABLE STAFF
	(
	Staff_Id INT NOT NULL IDENTITY (1,1),
	FirstName VARCHAR(15) NOT NULL,
	LastName VARCHAR(15) NOT NULL,
	Contact NVARCHAR(15) NOT NULL,
	StaffType Char(20) NOT NULL,
	Ward_Id INT NULL,
	CONSTRAINT STAFF_PK PRIMARY KEY (Staff_Id),
	CONSTRAINT STAFF_WARD_FK FOREIGN KEY (Ward_Id) REFERENCES WARD (Ward_No)
	ON DELETE SET DEFAULT ON UPDATE CASCADE
	);

--5
CREATE TABLE NURSE
	(
	Nurse_Id INT NOT NULL IDENTITY (1,1),
	FirstName VARCHAR(15) NOT NULL,
	LastName VARCHAR(15) NOT NULL,
	Contact NVARCHAR(15) NOT NULL,
	WardId INT NULL,
	CONSTRAINT NURSE_PK PRIMARY KEY (Nurse_Id),
	CONSTRAINT NURSE_WARD_FK FOREIGN KEY (WardId) REFERENCES WARD (Ward_No)
	ON DELETE SET DEFAULT ON UPDATE CASCADE
	);

--6
CREATE TABLE [dbo].[REPORT] (
    [Report_Id] INT           NOT NULL,
    [Description]      VARCHAR (200) NOT NULL,
    CONSTRAINT [REPORT_PK] PRIMARY KEY CLUSTERED ([Report_Id] ASC),
    );


--8
CREATE TABLE [dbo].[PATIENT] (
    [Patient_Id]       INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]        VARCHAR (15)  NOT NULL,
    [LastName]         VARCHAR (15)  NOT NULL,
    [Gender]           CHAR (10)     NOT NULL,
    [Age]              INT           NOT NULL,
    [Contact]          NVARCHAR (15) NOT NULL,
    [AdmitDateAndTime] DATETIME      NOT NULL,
    [Doc_Id]           INT           NULL,
    [Ward_Id]          INT           NULL,
    [Report_Id]        INT           NULL,
	[ReportDateAndTime] DATETIME      NOT NULL,
    CONSTRAINT [PATIENT_PK] PRIMARY KEY CLUSTERED ([Patient_Id] ASC),
    CONSTRAINT [PATIENT_DOCTOR_FK] 
    FOREIGN KEY ([Doc_Id]) REFERENCES [dbo].[DOCTOR] ([Doctor_Id]),
    CONSTRAINT [PATIENT_WARD_FK] FOREIGN KEY ([Ward_Id]) REFERENCES [dbo].[WARD] ([Ward_No]),
	CONSTRAINT [PATIENT_REPORT_FK] FOREIGN KEY ([Report_Id]) REFERENCES [dbo].[REPORT] ([Report_Id])
	ON DELETE SET DEFAULT ON UPDATE CASCADE
);

--7 if used
CREATE TABLE [dbo].[PATIENT_EMERGENCY_CONTACT] (
	[Person_Id] INT IDENTITY (1,1),
	[FullName] VARCHAR (300) NOT NULL,
	[PhoneNumber] NVARCHAR (15) NOT NULL,
	[Relation] VARCHAR (50) NOT NULL,
	CONSTRAINT [EMERGENCY_CONTACT] PRIMARY KEY CLUSTERED ([Person_Id] ASC)
);

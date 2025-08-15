CREATE DATABASE MusicSchoolDB;
GO

USE MusicSchoolDB;
GO

CREATE TABLE Schools (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Code NVARCHAR(50) UNIQUE NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX)
);
GO

CREATE TABLE Teachers (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Identifier NVARCHAR(50) UNIQUE NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    SchoolId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Teachers_Schools FOREIGN KEY (SchoolId) REFERENCES Schools(Id)
);
GO

CREATE TABLE Students (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Identifier NVARCHAR(50) UNIQUE NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL
);
GO

CREATE TABLE StudentTeachers (
    StudentId UNIQUEIDENTIFIER NOT NULL,
    TeacherId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (StudentId, TeacherId),
    CONSTRAINT FK_StudentTeachers_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_StudentTeachers_Teachers FOREIGN KEY (TeacherId) REFERENCES Teachers(Id)
);
GO

CREATE TABLE StudentSchools (
    StudentId UNIQUEIDENTIFIER NOT NULL,
    SchoolId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (StudentId, SchoolId),
    CONSTRAINT FK_StudentSchools_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_StudentSchools_Schools FOREIGN KEY (SchoolId) REFERENCES Schools(Id)
);
GO

-- Example stored procedures (Create/Update/Delete/Get by Id)
CREATE PROCEDURE dbo.School_Create
    @Id UNIQUEIDENTIFIER,
    @Code NVARCHAR(50),
    @Name NVARCHAR(200),
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO dbo.Schools (Id, Code, Name, Description)
    VALUES (@Id, @Code, @Name, @Description);
END
GO

CREATE PROCEDURE dbo.Teacher_Create
    @Id UNIQUEIDENTIFIER,
    @Identifier NVARCHAR(50),
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO dbo.Teachers (Id, Identifier, FirstName, LastName, SchoolId)
    VALUES (@Id, @Identifier, @FirstName, @LastName, @SchoolId);
END
GO

CREATE PROCEDURE dbo.Student_Create
    @Id UNIQUEIDENTIFIER,
    @Identifier NVARCHAR(50),
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @DateOfBirth DATE
AS
BEGIN
    INSERT INTO dbo.Students (Id, Identifier, FirstName, LastName, DateOfBirth)
    VALUES (@Id, @Identifier, @FirstName, @LastName, @DateOfBirth);
END
GO

DROP DATABASE IF EXISTS RaceDay;
GO

CREATE DATABASE RaceDay;
GO

USE RaceDay;
GO

----User Type Table------------------
CREATE TABLE UserType (
    UserTypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL,
    TypeDescription VARCHAR(200) 
);
GO

-----USER Table---------------
CREATE TABLE [USER] (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Surname VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    UserTypeId INT NOT NULL,
    FOREIGN KEY (UserTypeId) REFERENCES UserType(UserTypeId)
);
GO

------Event Table------------
CREATE TABLE EVENT (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventName VARCHAR(100) NOT NULL,
    EventDescription VARCHAR(200),
    EventDate DATETIME NOT NULL,
    Distance DECIMAL(5,2),
    Location VARCHAR(50) NOT NULL,
    EventType VARCHAR(50) NOT NULL
);
GO

----Category Table------------
CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL,
    CategoryDescription VARCHAR(255),
    EventID INT NOT NULL,
    FOREIGN KEY (EventID) REFERENCES Event(EventID)
);
GO

----EventEnrollment Table--------------
CREATE TABLE EventEnrollment (
    EventEnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(50) NOT NULL,
    UserID INT NOT NULL,
    CategoryID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES [User](UserID),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);
GO

----Result Table-----------
CREATE TABLE Result (
    ResultID INT IDENTITY(1,1) PRIMARY KEY,
    FinishingTime TIME,
    FinishingPosition INT,
    ResultDate DATETIME DEFAULT GETDATE(),
    EventEnrollmentID INT NOT NULL,
    FOREIGN KEY (EventEnrollmentID) REFERENCES EventEnrollment(EventEnrollmentID)
);
GO

-------Table insertions--------
INSERT INTO UserType (TypeName, TypeDescription) VALUES
('Organiser',   'Manages events '),
('Participant', 'Competes in events');
GO

INSERT INTO [USER] (Name, Surname, Email, PasswordHash, UserTypeId) VALUES
('Ethan', 'Paul',   'EthanPaul02@gmail.com', 'HASHED_PASSWORD_1', 1),
('Adele', 'John',   'ADJ1@gmail.com',        'HASHED_PASSWORD_2', 1),
('Cloud', 'Strife', 'Cloud7@gmail.com',      'HASHED_PASSWORD_3', 2),
('Aiko',  'Naidoo', 'AikoAiko376@gmail.com', 'HASHED_PASSWORD_4', 2);
GO

INSERT INTO EVENT (EventName, EventDescription, EventDate, Location, Distance, EventType) VALUES
('Kings Park',                       'Stadium Race',        '2026-10-18 09:00:00', 'Durban, KZN',   0.40, 'Track'),
('Kenneth Stainbank Nature Reserve', 'Sizable forest path', '2026-11-22 07:30:00', 'Durban, KZN',   5.00, 'Walk'),
('Gateway Park',                     'Park venue',          '2026-12-12 08:00:00', 'uMhlanga, KZN', 5.00, 'Run');
GO

INSERT INTO Category (EventID, CategoryName, CategoryDescription) VALUES
(1, '400m Sprint',  'One full lap around the stadium track'),
(1, '800m Dash',    'Two laps around the stadium track'),
(2, 'Open Trail',   'Standard forest walking path for all ages'),
(2, 'Senior Walk',  'Gentle paced walk for ages 60+'),
(3, '5km Park Run', 'Standard 5km morning run around the park'),
(3, 'Kids 2km',     'Short distance run for children');
GO

INSERT INTO EventEnrollment (UserID, CategoryID, Status) VALUES
(3, 1, 'Confirmed'),
(4, 5, 'Confirmed'),
(3, 3, 'Confirmed'),
(4, 1, 'Confirmed');
GO

INSERT INTO Result (EventEnrollmentID, FinishingTime, FinishingPosition) VALUES
(1, '00:00:58', 1), 
(2, '00:24:15', 1), 
(4, '00:01:12', 2); 
GO
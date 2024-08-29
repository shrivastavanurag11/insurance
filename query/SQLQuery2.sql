
CREATE TABLE PolicyDetails(
    PolicyDetailID INT PRIMARY KEY,
    PolicyID INT NOT NULL,
    PolicyName VARCHAR(50) NOT NULL,
    PolicyDescription VARCHAR(200) NOT NULL,
    FOREIGN KEY (PolicyID) REFERENCES Policies(PolicyID)
);

-- ---------------- - - - - - ---  - - - - - ---   - - - --  ------------------
drop table Users
CREATE TABLE Users(
    UserID INT PRIMARY KEY Identity(1,1),
    UserName VARCHAR(25) UNIQUE NOT NULL,
    Password Varchar(50) NOT NULL,
    UserType VARCHAR(20) NOT NULL,                --Admin or Customer--
    FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Email VARCHAR(50) UNIQUE NOT NULL,
	ContactNo VARCHAR(20) UNIQUE NOT NULL,
	Address VARCHAR(200) NOT NULL,
	CustomerImage VARBINARY(MAX)
);


CREATE TABLE Policies(
    PolicyID INT PRIMARY KEY,
    PolicyType VARCHAR(20) NOT NULL, 
    PolicyName VARCHAR(50) NOT NULL,
    PolicyDescription VARCHAR(200) NOT NULL,
);


CREATE TABLE PolicySold(
    UserID INT Foreign key references Users(UserId),
    PolicyID INT foreign key  references Policies(PolicyId),
    SoldDate datetime
);

CREATE TABLE Cart();

CREATE TABLE Claims();

GO;
------------
alter proc Registration @username varchar(25), @password varchar(50), @firstname varchar(20), @lastname varchar(20), @email varchar(50), @contactNo varchar(20), @address varchar(200)
as
insert into Users values (@username , @password , 'costumer' , @firstname , @lastname , @email , @contactNo , @address, null)
Go;
-----------
CREATE PROCEDURE login  @username VARCHAR(25), @password VARCHAR(50)
AS
BEGIN
    SELECT UserType FROM Users
    WHERE UserName = @username AND Password = @password;
END
GO

USE [master]
GO
/****** Object:  Database [insurance]    Script Date: 9/3/2024 11:15:45 AM ******/
CREATE DATABASE [insurance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'insurance', FILENAME = N'C:\Users\admin\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\insurance.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'insurance_log', FILENAME = N'C:\Users\admin\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\insurance.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [insurance] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [insurance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [insurance] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [insurance] SET ANSI_NULLS ON 
GO
ALTER DATABASE [insurance] SET ANSI_PADDING ON 
GO
ALTER DATABASE [insurance] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [insurance] SET ARITHABORT ON 
GO
ALTER DATABASE [insurance] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [insurance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [insurance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [insurance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [insurance] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [insurance] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [insurance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [insurance] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [insurance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [insurance] SET  DISABLE_BROKER 
GO
ALTER DATABASE [insurance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [insurance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [insurance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [insurance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [insurance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [insurance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [insurance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [insurance] SET RECOVERY FULL 
GO
ALTER DATABASE [insurance] SET  MULTI_USER 
GO
ALTER DATABASE [insurance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [insurance] SET DB_CHAINING OFF 
GO
ALTER DATABASE [insurance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [insurance] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [insurance] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [insurance] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [insurance] SET QUERY_STORE = OFF
GO
USE [insurance]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 9/3/2024 11:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[PolicyId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Claims]    Script Date: 9/3/2024 11:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claims](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseId] [int] NULL,
	[ClaimAmount] [money] NULL,
	[remainingAmount] [money] NULL,
	[ClaimDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 9/3/2024 11:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[PolicyID] [int] NOT NULL,
	[PolicyType] [varchar](20) NOT NULL,
	[PolicyName] [varchar](50) NOT NULL,
	[InsuranceAmount] [money] NOT NULL,
	[PolicyValidity] [int] NOT NULL,
	[PolicyDescription] [varchar](200) NOT NULL,
	[Available] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolicySold]    Script Date: 9/3/2024 11:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolicySold](
	[PurchaseId] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[PolicyID] [int] NULL,
	[SoldDate] [datetime] NOT NULL,
	[Amount] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/3/2024 11:15:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](25) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserType] [varchar](20) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[Age] [int] NULL,
	[Gender] [char](1) NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNo] [varchar](20) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[CustomerImage] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Claims] ON 

INSERT [dbo].[Claims] ([ClaimId], [PurchaseId], [ClaimAmount], [remainingAmount], [ClaimDate]) VALUES (1, 1, 10000.0000, 90000.0000, CAST(N'2024-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Claims] ([ClaimId], [PurchaseId], [ClaimAmount], [remainingAmount], [ClaimDate]) VALUES (2, 2, 50000.0000, 150000.0000, CAST(N'2024-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Claims] ([ClaimId], [PurchaseId], [ClaimAmount], [remainingAmount], [ClaimDate]) VALUES (3, 3, 10000.0000, 40000.0000, CAST(N'2024-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Claims] ([ClaimId], [PurchaseId], [ClaimAmount], [remainingAmount], [ClaimDate]) VALUES (4, 4, 75000.0000, 75000.0000, CAST(N'2024-10-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Claims] ([ClaimId], [PurchaseId], [ClaimAmount], [remainingAmount], [ClaimDate]) VALUES (5, 5, 5000.0000, 25000.0000, CAST(N'2024-11-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Claims] OFF
GO
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (1, N'Life', N'Life Insurance Basic', 100000.0000, 20, N'Basic life insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (2, N'Life', N'Life Insurance Premium', 200000.0000, 25, N'Premium life insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (3, N'Health', N'Health Insurance Basic', 50000.0000, 15, N'Basic health insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (4, N'Health', N'Health Insurance Premium', 150000.0000, 20, N'Premium health insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (5, N'Car', N'Car Insurance Basic', 30000.0000, 5, N'Basic car insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (6, N'Car', N'Car Insurance Premium', 100000.0000, 10, N'Premium car insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (7, N'Home', N'Home Insurance Basic', 250000.0000, 30, N'Basic home insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (8, N'Home', N'Home Insurance Premium', 500000.0000, 35, N'Premium home insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (9, N'Travel', N'Travel Insurance Basic', 20000.0000, 2, N'Basic travel insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (10, N'Travel', N'Travel Insurance Premium', 50000.0000, 5, N'Premium travel insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (11, N'Life', N'Life Insurance Plus', 150000.0000, 22, N'Enhanced life insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (12, N'Health', N'Health Insurance Plus', 80000.0000, 18, N'Enhanced health insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (13, N'Car', N'Car Insurance Plus', 70000.0000, 8, N'Enhanced car insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (14, N'Home', N'Home Insurance Plus', 350000.0000, 33, N'Enhanced home insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (15, N'Travel', N'Travel Insurance Plus', 40000.0000, 3, N'Enhanced travel insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (16, N'Life', N'Life Insurance Ultimate', 250000.0000, 30, N'Ultimate life insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (17, N'Health', N'Health Insurance Ultimate', 100000.0000, 25, N'Ultimate health insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (18, N'Car', N'Car Insurance Ultimate', 120000.0000, 12, N'Ultimate car insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (19, N'Home', N'Home Insurance Ultimate', 600000.0000, 40, N'Ultimate home insurance policy.', N'Y')
INSERT [dbo].[Policies] ([PolicyID], [PolicyType], [PolicyName], [InsuranceAmount], [PolicyValidity], [PolicyDescription], [Available]) VALUES (20, N'Travel', N'Travel Insurance Ultimate', 75000.0000, 7, N'Ultimate travel insurance policy.', N'Y')
GO
SET IDENTITY_INSERT [dbo].[PolicySold] ON 

INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (1, 1, 1, CAST(N'2024-01-01T00:00:00.000' AS DateTime), 100000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (2, 2, 2, CAST(N'2024-02-01T00:00:00.000' AS DateTime), 200000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (3, 3, 3, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 50000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (4, 4, 4, CAST(N'2024-04-01T00:00:00.000' AS DateTime), 150000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (5, 5, 5, CAST(N'2024-05-01T00:00:00.000' AS DateTime), 30000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (6, 6, 6, CAST(N'2024-06-01T00:00:00.000' AS DateTime), 100000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (7, 7, 7, CAST(N'2024-07-01T00:00:00.000' AS DateTime), 250000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (8, 8, 8, CAST(N'2024-08-01T00:00:00.000' AS DateTime), 500000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (9, 9, 9, CAST(N'2024-09-01T00:00:00.000' AS DateTime), 20000.0000)
INSERT [dbo].[PolicySold] ([PurchaseId], [UserID], [PolicyID], [SoldDate], [Amount]) VALUES (10, 10, 10, CAST(N'2024-10-01T00:00:00.000' AS DateTime), 50000.0000)
SET IDENTITY_INSERT [dbo].[PolicySold] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (1, N'user1', N'123', N'Customer', N'John', N'Doe', 30, N'M', N'user1@example.com', N'1234567890', N'Address 1', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (2, N'user2', N'123', N'Customer', N'Jane', N'Doe', 25, N'F', N'user2@example.com', N'1234567891', N'Address 2', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (3, N'user3', N'123', N'Customer', N'Alice', N'Smith', 28, N'F', N'user3@example.com', N'1234567892', N'Address 3', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (4, N'user4', N'123', N'Customer', N'Bob', N'Johnson', 35, N'M', N'user4@example.com', N'1234567893', N'Address 4', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (5, N'user5', N'123', N'Customer', N'Charlie', N'Brown', 40, N'M', N'user5@example.com', N'1234567894', N'Address 5', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (6, N'user6', N'123', N'Customer', N'David', N'White', 22, N'M', N'user6@example.com', N'1234567895', N'Address 6', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (7, N'user7', N'123', N'Customer', N'Eve', N'Black', 29, N'F', N'user7@example.com', N'1234567896', N'Address 7', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (8, N'user8', N'123', N'Customer', N'Frank', N'Green', 45, N'M', N'user8@example.com', N'1234567897', N'Address 8', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (9, N'user9', N'123', N'Customer', N'Grace', N'Blue', 32, N'F', N'user9@example.com', N'1234567898', N'Address 9', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (10, N'user10', N'123', N'Customer', N'Henry', N'Yellow', 37, N'M', N'user10@example.com', N'1234567899', N'Address 10', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (11, N'user11', N'123', N'Customer', N'Isabella', N'Red', 31, N'F', N'user11@example.com', N'1234567800', N'Address 11', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (12, N'user12', N'123', N'Customer', N'Jack', N'Purple', 42, N'M', N'user12@example.com', N'1234567801', N'Address 12', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (13, N'user13', N'123', N'Customer', N'Katherine', N'Orange', 26, N'F', N'user13@example.com', N'1234567802', N'Address 13', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (14, N'user14', N'123', N'Customer', N'Liam', N'Pink', 38, N'M', N'user14@example.com', N'1234567803', N'Address 14', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (15, N'user15', N'123', N'Customer', N'Mia', N'Gray', 33, N'F', N'user15@example.com', N'1234567804', N'Address 15', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (16, N'user16', N'123', N'Customer', N'Noah', N'White', 36, N'M', N'user16@example.com', N'1234567805', N'Address 16', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (17, N'user17', N'123', N'Customer', N'Olivia', N'Black', 27, N'F', N'user17@example.com', N'1234567806', N'Address 17', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (18, N'user18', N'123', N'Customer', N'Paul', N'Green', 34, N'M', N'user18@example.com', N'1234567807', N'Address 18', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (19, N'user19', N'123', N'Customer', N'Quinn', N'Blue', 39, N'M', N'user19@example.com', N'1234567808', N'Address 19', NULL)
INSERT [dbo].[Users] ([UserID], [UserName], [Password], [UserType], [FirstName], [LastName], [Age], [Gender], [Email], [ContactNo], [Address], [CustomerImage]) VALUES (20, N'user20', N'123', N'Customer', N'Rachel', N'Brown', 28, N'F', N'user20@example.com', N'1234567809', N'Address 20', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__5C667C05ECC74897]    Script Date: 9/3/2024 11:15:46 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[ContactNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D105343B73404B]    Script Date: 9/3/2024 11:15:46 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__C9F28456216141F6]    Script Date: 9/3/2024 11:15:46 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([PolicyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Claims]  WITH CHECK ADD FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[PolicySold] ([PurchaseId])
GO
ALTER TABLE [dbo].[PolicySold]  WITH CHECK ADD FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policies] ([PolicyID])
GO
ALTER TABLE [dbo].[PolicySold]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
USE [master]
GO
ALTER DATABASE [insurance] SET  READ_WRITE 
GO

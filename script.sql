USE [master]
GO
/****** Object:  Database [Quiz _Management]    Script Date: 9/16/2024 8:56:53 PM ******/
CREATE DATABASE [Quiz _Management]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Quiz _Management', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Quiz _Management.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Quiz _Management_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Quiz _Management_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Quiz _Management] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Quiz _Management].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Quiz _Management] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Quiz _Management] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Quiz _Management] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Quiz _Management] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Quiz _Management] SET ARITHABORT OFF 
GO
ALTER DATABASE [Quiz _Management] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Quiz _Management] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Quiz _Management] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Quiz _Management] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Quiz _Management] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Quiz _Management] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Quiz _Management] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Quiz _Management] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Quiz _Management] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Quiz _Management] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Quiz _Management] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Quiz _Management] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Quiz _Management] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Quiz _Management] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Quiz _Management] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Quiz _Management] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Quiz _Management] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Quiz _Management] SET RECOVERY FULL 
GO
ALTER DATABASE [Quiz _Management] SET  MULTI_USER 
GO
ALTER DATABASE [Quiz _Management] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Quiz _Management] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Quiz _Management] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Quiz _Management] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Quiz _Management] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Quiz _Management] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Quiz _Management', N'ON'
GO
ALTER DATABASE [Quiz _Management] SET QUERY_STORE = OFF
GO
USE [Quiz _Management]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 9/16/2024 8:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[dob] [date] NULL,
	[first_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NULL,
	[create_dt] [datetime] NULL,
	[update_dt] [datetime] NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 9/16/2024 8:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[menu_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[link] [nvarchar](50) NULL,
	[icon] [nvarchar](50) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuRole]    Script Date: 9/16/2024 8:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuRole](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NOT NULL,
	[menu_id] [int] NOT NULL,
 CONSTRAINT [PK_MenuRole] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 9/16/2024 8:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([user_id], [email], [password], [dob], [first_name], [last_name], [create_dt], [update_dt], [role_id]) VALUES (1, N'hungle692002@gmail.com', N'123', CAST(N'2002-06-09' AS Date), N'Hung', N'Le', CAST(N'2024-07-07T00:00:00.000' AS DateTime), CAST(N'2024-07-07T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Account] ([user_id], [email], [password], [dob], [first_name], [last_name], [create_dt], [update_dt], [role_id]) VALUES (2, N'hungle@gmail.com', N'123', CAST(N'2024-07-01' AS Date), N'Hung', N'Le', CAST(N'2024-07-07T21:53:28.710' AS DateTime), CAST(N'2024-07-07T21:53:28.710' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([menu_id], [name], [link], [icon]) VALUES (1, N'Home', N'/Home', N'bi-house-door-fill-nav-menu')
INSERT [dbo].[Menu] ([menu_id], [name], [link], [icon]) VALUES (2, N'Account List', N'/AccountList', N'bi-card-list-nav-menu')
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[MenuRole] ON 

INSERT [dbo].[MenuRole] ([id], [role_id], [menu_id]) VALUES (1, 2, 1)
INSERT [dbo].[MenuRole] ([id], [role_id], [menu_id]) VALUES (2, 2, 2)
INSERT [dbo].[MenuRole] ([id], [role_id], [menu_id]) VALUES (4, 1, 1)
INSERT [dbo].[MenuRole] ([id], [role_id], [menu_id]) VALUES (5, 1, 2)
SET IDENTITY_INSERT [dbo].[MenuRole] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'ADMIN')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'USER')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[MenuRole]  WITH CHECK ADD  CONSTRAINT [FK_MenuRole_Menu] FOREIGN KEY([menu_id])
REFERENCES [dbo].[Menu] ([menu_id])
GO
ALTER TABLE [dbo].[MenuRole] CHECK CONSTRAINT [FK_MenuRole_Menu]
GO
ALTER TABLE [dbo].[MenuRole]  WITH CHECK ADD  CONSTRAINT [FK_MenuRole_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[MenuRole] CHECK CONSTRAINT [FK_MenuRole_Role]
GO
USE [master]
GO
ALTER DATABASE [Quiz _Management] SET  READ_WRITE 
GO

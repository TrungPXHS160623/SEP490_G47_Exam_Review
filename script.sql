USE [master]
GO
/****** Object:  Database [QuizManagement]    Script Date: 9/17/2024 10:07:52 PM ******/
CREATE DATABASE [QuizManagement]
GO
ALTER DATABASE [QuizManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuizManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuizManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuizManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuizManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuizManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuizManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuizManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuizManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuizManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuizManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuizManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuizManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuizManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuizManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuizManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuizManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuizManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuizManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuizManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuizManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuizManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuizManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuizManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuizManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [QuizManagement] SET  MULTI_USER 
GO
ALTER DATABASE [QuizManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuizManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuizManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuizManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuizManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuizManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuizManagement', N'ON'
GO
ALTER DATABASE [QuizManagement] SET QUERY_STORE = OFF
GO
USE [QuizManagement]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 9/17/2024 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](250) NULL,
	[password] [nvarchar](250) NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainQuestion]    Script Date: 9/17/2024 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainQuestion](
	[main_id] [int] IDENTITY(1,1) NOT NULL,
	[main_content] [nvarchar](250) NULL,
	[subject_id] [varchar](10) NULL,
	[images] [nvarchar](250) NULL,
	[question_type] [int] NULL,
 CONSTRAINT [PK_MainQuestion] PRIMARY KEY CLUSTERED 
(
	[main_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionHistory]    Script Date: 9/17/2024 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NULL,
	[main_id] [int] NULL,
	[update_dt] [datetime] NULL,
 CONSTRAINT [PK_QuestionHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionSubject]    Script Date: 9/17/2024 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionSubject](
	[subject_id] [varchar](10) NOT NULL,
	[subject_name] [nvarchar](50) NULL,
	[time] [varchar](50) NULL,
 CONSTRAINT [PK_QuestionSubject] PRIMARY KEY CLUSTERED 
(
	[subject_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 9/17/2024 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](250) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubQuestion]    Script Date: 9/17/2024 10:07:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubQuestion](
	[sub_id] [int] IDENTITY(1,1) NOT NULL,
	[main_id] [int] NULL,
	[sub_content] [nvarchar](250) NULL,
	[is_answer] [nchar](10) NULL,
 CONSTRAINT [PK_SubQuestion] PRIMARY KEY CLUSTERED 
(
	[sub_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([account_id], [email], [password], [role_id]) VALUES (1, N'hungle@gmail.com', N'123', 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[MainQuestion]  WITH CHECK ADD  CONSTRAINT [FK_MainQuestion_QuestionSubject] FOREIGN KEY([subject_id])
REFERENCES [dbo].[QuestionSubject] ([subject_id])
GO
ALTER TABLE [dbo].[MainQuestion] CHECK CONSTRAINT [FK_MainQuestion_QuestionSubject]
GO
ALTER TABLE [dbo].[QuestionHistory]  WITH CHECK ADD  CONSTRAINT [FK_QuestionHistory_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([account_id])
GO
ALTER TABLE [dbo].[QuestionHistory] CHECK CONSTRAINT [FK_QuestionHistory_Account]
GO
ALTER TABLE [dbo].[QuestionHistory]  WITH CHECK ADD  CONSTRAINT [FK_QuestionHistory_MainQuestion] FOREIGN KEY([main_id])
REFERENCES [dbo].[MainQuestion] ([main_id])
GO
ALTER TABLE [dbo].[QuestionHistory] CHECK CONSTRAINT [FK_QuestionHistory_MainQuestion]
GO
ALTER TABLE [dbo].[SubQuestion]  WITH CHECK ADD  CONSTRAINT [FK_SubQuestion_MainQuestion] FOREIGN KEY([main_id])
REFERENCES [dbo].[MainQuestion] ([main_id])
GO
ALTER TABLE [dbo].[SubQuestion] CHECK CONSTRAINT [FK_SubQuestion_MainQuestion]
GO
USE [master]
GO
ALTER DATABASE [QuizManagement] SET  READ_WRITE 
GO

USE [master]
GO
/****** Object:  Database [SWP391DB]    Script Date: 2/19/2024 10:13:35 AM ******/
CREATE DATABASE [SWP391DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SWP391DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SWP391DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SWP391DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SWP391DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SWP391DB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SWP391DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SWP391DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SWP391DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SWP391DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SWP391DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SWP391DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SWP391DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SWP391DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SWP391DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SWP391DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SWP391DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SWP391DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SWP391DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SWP391DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SWP391DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SWP391DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SWP391DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SWP391DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SWP391DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SWP391DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SWP391DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SWP391DB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SWP391DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SWP391DB] SET RECOVERY FULL 
GO
ALTER DATABASE [SWP391DB] SET  MULTI_USER 
GO
ALTER DATABASE [SWP391DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SWP391DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SWP391DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SWP391DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SWP391DB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SWP391DB', N'ON'
GO
ALTER DATABASE [SWP391DB] SET QUERY_STORE = OFF
GO
USE [SWP391DB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SWP391DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/19/2024 10:13:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Account]    Script Date: 2/19/2024 10:13:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[username] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[role] [varchar](10) NOT NULL,
 CONSTRAINT [PK__Account__F3DBC5731230846A] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BasementType]    Script Date: 2/19/2024 10:13:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasementType](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[unitPrice] [money] NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_BasementType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConstructDetail]    Script Date: 2/19/2024 10:13:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstructDetail](
	[quotationId] [char](6) NOT NULL,
	[width] [decimal](7, 2) NOT NULL,
	[length] [decimal](7, 2) NOT NULL,
	[facade] [int] NOT NULL,
	[alley] [nvarchar](20) NOT NULL,
	[floor] [int] NOT NULL,
	[room] [int] NOT NULL,
	[mezzanine] [decimal](7, 2) NOT NULL,
	[rooftopFloor] [decimal](7, 2) NOT NULL,
	[balcony] [bit] NOT NULL,
	[garden] [decimal](6, 1) NOT NULL,
	[constructionId] [char](3) NOT NULL,
	[investmentId] [char](3) NOT NULL,
	[foundationId] [char](3) NOT NULL,
	[rooftopId] [char](3) NOT NULL,
	[basementId] [char](3) NOT NULL,
 CONSTRAINT [PK__Construc__7536E3527BF2F7DA] PRIMARY KEY CLUSTERED 
(
	[quotationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConstructionType]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstructionType](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_ConstructionType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [char](5) NOT NULL,
	[name] [nvarchar](30) NOT NULL,
	[phoneNum] [varchar](15) NULL,
	[email] [varchar](35) NULL,
	[gender] [varchar](6) NULL,
	[username] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomQuotaionTask]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomQuotaionTask](
	[taskId] [char](5) NOT NULL,
	[quotationId] [char](6) NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK__CustomQu__EA0E34779FFE6727] PRIMARY KEY CLUSTERED 
(
	[taskId] ASC,
	[quotationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomQuotation]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomQuotation](
	[id] [char](6) NOT NULL,
	[Date] [datetime] NOT NULL,
	[acreage] [varchar](10) NULL,
	[location] [nvarchar](30) NOT NULL,
	[status] [int] NOT NULL,
	[description] [nvarchar](500) NULL,
	[total] [money] NOT NULL,
	[sellerId] [char](5) NOT NULL,
	[engineerId] [char](5) NOT NULL,
	[managerId] [char](5) NOT NULL,
	[requestId] [char](5) NOT NULL,
 CONSTRAINT [PK_CustomQuotation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FoundationType]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoundationType](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[areaRatio] [decimal](4, 2) NULL,
	[unitPrice] [money] NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_FoundationType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvestmentType]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestmentType](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_InvestmentType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Material]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[id] [char](5) NOT NULL,
	[name] [nvarchar](80) NOT NULL,
	[inventoryQuantity] [int] NOT NULL,
	[unitPrice] [money] NOT NULL,
	[unit] [varchar](5) NOT NULL,
	[status] [bit] NOT NULL,
	[categoryId] [char](3) NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaterialCategory]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialCategory](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_MaterialCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaterialDetail]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialDetail](
	[quotationId] [char](6) NOT NULL,
	[materialId] [char](5) NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [money] NULL,
 CONSTRAINT [PK__Material__BCAD866D29D0C3FC] PRIMARY KEY CLUSTERED 
(
	[quotationId] ASC,
	[materialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pricing]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pricing](
	[ConstructTypeId] [char](3) NOT NULL,
	[InvestmentTypeId] [char](3) NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK__Pricing__82221887E948CB0C] PRIMARY KEY CLUSTERED 
(
	[ConstructTypeId] ASC,
	[InvestmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[id] [char](5) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[location] [nvarchar](50) NOT NULL,
	[scale] [nvarchar](50) NOT NULL,
	[size] [varchar](10) NOT NULL,
	[description] [nvarchar](500) NULL,
	[status] [bit] NOT NULL,
	[customerId] [char](5) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Overview] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestForm]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestForm](
	[id] [char](5) NOT NULL,
	[generateDate] [datetime] NOT NULL,
	[description] [nvarchar](500) NULL,
	[constructType] [nvarchar](50) NULL,
	[acreage] [varchar](10) NULL,
	[location] [nvarchar](30) NOT NULL,
	[status] [bit] NOT NULL,
	[customerId] [char](5) NOT NULL,
 CONSTRAINT [PK_RequestForm] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestFormMaterial]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestFormMaterial](
	[requestId] [char](5) NOT NULL,
	[materialId] [char](5) NOT NULL,
 CONSTRAINT [PK__RequestF__2A5EBB0EE35F5BBE] PRIMARY KEY CLUSTERED 
(
	[requestId] ASC,
	[materialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RooftopType]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RooftopType](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[unitPrice] [money] NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_RooftopType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staff]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[id] [char](5) NOT NULL,
	[name] [nvarchar](30) NOT NULL,
	[phoneNum] [varchar](15) NULL,
	[email] [varchar](35) NULL,
	[gender] [varchar](6) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[managerId] [char](5) NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StandardQuotation]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StandardQuotation](
	[id] [char](5) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[description] [nvarchar](500) NULL,
	[price] [money] NOT NULL,
	[status] [bit] NOT NULL,
	[constructionId] [char](3) NOT NULL,
 CONSTRAINT [PK_StandardQuotation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StandardQuotationMaterial]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StandardQuotationMaterial](
	[quotationId] [char](5) NOT NULL,
	[materialId] [char](5) NOT NULL,
 CONSTRAINT [PK__Standard__BCAD866D662F5E93] PRIMARY KEY CLUSTERED 
(
	[quotationId] ASC,
	[materialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StandardQuotationTask]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StandardQuotationTask](
	[quotationId] [char](5) NOT NULL,
	[taskId] [char](5) NOT NULL,
 CONSTRAINT [PK__Standard__48E336F665A8BFCB] PRIMARY KEY CLUSTERED 
(
	[quotationId] ASC,
	[taskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[id] [char](5) NOT NULL,
	[name] [nvarchar](80) NOT NULL,
	[description] [nvarchar](500) NULL,
	[unitPrice] [money] NOT NULL,
	[status] [bit] NOT NULL,
	[categoryId] [char](3) NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskCategory]    Script Date: 2/19/2024 10:13:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskCategory](
	[id] [char](3) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_TaskCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstructDetail_basementId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConstructDetail_basementId] ON [dbo].[ConstructDetail]
(
	[basementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstructDetail_constructionId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConstructDetail_constructionId] ON [dbo].[ConstructDetail]
(
	[constructionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstructDetail_foundationId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConstructDetail_foundationId] ON [dbo].[ConstructDetail]
(
	[foundationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstructDetail_investmentId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConstructDetail_investmentId] ON [dbo].[ConstructDetail]
(
	[investmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ConstructDetail_rooftopId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_ConstructDetail_rooftopId] ON [dbo].[ConstructDetail]
(
	[rooftopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Customer_username]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Customer_username] ON [dbo].[Customer]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CustomQuotaionTask_quotationId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomQuotaionTask_quotationId] ON [dbo].[CustomQuotaionTask]
(
	[quotationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CustomQuotation_engineerId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomQuotation_engineerId] ON [dbo].[CustomQuotation]
(
	[engineerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CustomQuotation_managerId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomQuotation_managerId] ON [dbo].[CustomQuotation]
(
	[managerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CustomQuotation_requestId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomQuotation_requestId] ON [dbo].[CustomQuotation]
(
	[requestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CustomQuotation_sellerId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_CustomQuotation_sellerId] ON [dbo].[CustomQuotation]
(
	[sellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Material_categoryId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Material_categoryId] ON [dbo].[Material]
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_MaterialDetail_materialId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_MaterialDetail_materialId] ON [dbo].[MaterialDetail]
(
	[materialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Pricing_InvestmentTypeId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Pricing_InvestmentTypeId] ON [dbo].[Pricing]
(
	[InvestmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Project_customerId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Project_customerId] ON [dbo].[Project]
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RequestForm_customerId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_RequestForm_customerId] ON [dbo].[RequestForm]
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RequestFormMaterial_materialId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_RequestFormMaterial_materialId] ON [dbo].[RequestFormMaterial]
(
	[materialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Staff_managerId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Staff_managerId] ON [dbo].[Staff]
(
	[managerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Staff_username]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Staff_username] ON [dbo].[Staff]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StandardQuotation_constructionId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_StandardQuotation_constructionId] ON [dbo].[StandardQuotation]
(
	[constructionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StandardQuotationMaterial_materialId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_StandardQuotationMaterial_materialId] ON [dbo].[StandardQuotationMaterial]
(
	[materialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_StandardQuotationTask_taskId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_StandardQuotationTask_taskId] ON [dbo].[StandardQuotationTask]
(
	[taskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Task_categoryId]    Script Date: 2/19/2024 10:13:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Task_categoryId] ON [dbo].[Task]
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [Date]
GO
ALTER TABLE [dbo].[ConstructDetail]  WITH CHECK ADD  CONSTRAINT [FK__Construct__basem__4D94879B] FOREIGN KEY([basementId])
REFERENCES [dbo].[BasementType] ([id])
GO
ALTER TABLE [dbo].[ConstructDetail] CHECK CONSTRAINT [FK__Construct__basem__4D94879B]
GO
ALTER TABLE [dbo].[ConstructDetail]  WITH CHECK ADD  CONSTRAINT [FK__Construct__const__4E88ABD4] FOREIGN KEY([constructionId])
REFERENCES [dbo].[ConstructionType] ([id])
GO
ALTER TABLE [dbo].[ConstructDetail] CHECK CONSTRAINT [FK__Construct__const__4E88ABD4]
GO
ALTER TABLE [dbo].[ConstructDetail]  WITH CHECK ADD  CONSTRAINT [FK__Construct__found__4D94879B] FOREIGN KEY([foundationId])
REFERENCES [dbo].[FoundationType] ([id])
GO
ALTER TABLE [dbo].[ConstructDetail] CHECK CONSTRAINT [FK__Construct__found__4D94879B]
GO
ALTER TABLE [dbo].[ConstructDetail]  WITH CHECK ADD  CONSTRAINT [FK__Construct__inves__5070F446] FOREIGN KEY([investmentId])
REFERENCES [dbo].[InvestmentType] ([id])
GO
ALTER TABLE [dbo].[ConstructDetail] CHECK CONSTRAINT [FK__Construct__inves__5070F446]
GO
ALTER TABLE [dbo].[ConstructDetail]  WITH CHECK ADD  CONSTRAINT [FK__Construct__quota__5165187F] FOREIGN KEY([quotationId])
REFERENCES [dbo].[CustomQuotation] ([id])
GO
ALTER TABLE [dbo].[ConstructDetail] CHECK CONSTRAINT [FK__Construct__quota__5165187F]
GO
ALTER TABLE [dbo].[ConstructDetail]  WITH CHECK ADD  CONSTRAINT [FK__Construct__rooft__52593CB8] FOREIGN KEY([rooftopId])
REFERENCES [dbo].[RooftopType] ([id])
GO
ALTER TABLE [dbo].[ConstructDetail] CHECK CONSTRAINT [FK__Construct__rooft__52593CB8]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK__Customer__userna__534D60F1] FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK__Customer__userna__534D60F1]
GO
ALTER TABLE [dbo].[CustomQuotaionTask]  WITH CHECK ADD  CONSTRAINT [FK__CustomQuo__quota__5441852A] FOREIGN KEY([quotationId])
REFERENCES [dbo].[CustomQuotation] ([id])
GO
ALTER TABLE [dbo].[CustomQuotaionTask] CHECK CONSTRAINT [FK__CustomQuo__quota__5441852A]
GO
ALTER TABLE [dbo].[CustomQuotaionTask]  WITH CHECK ADD  CONSTRAINT [FK__CustomQuo__taskI__534D60F1] FOREIGN KEY([taskId])
REFERENCES [dbo].[Task] ([id])
GO
ALTER TABLE [dbo].[CustomQuotaionTask] CHECK CONSTRAINT [FK__CustomQuo__taskI__534D60F1]
GO
ALTER TABLE [dbo].[CustomQuotation]  WITH CHECK ADD  CONSTRAINT [FK__CustomQuo__engin__5629CD9C] FOREIGN KEY([engineerId])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[CustomQuotation] CHECK CONSTRAINT [FK__CustomQuo__engin__5629CD9C]
GO
ALTER TABLE [dbo].[CustomQuotation]  WITH CHECK ADD  CONSTRAINT [FK__CustomQuo__manag__571DF1D5] FOREIGN KEY([managerId])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[CustomQuotation] CHECK CONSTRAINT [FK__CustomQuo__manag__571DF1D5]
GO
ALTER TABLE [dbo].[CustomQuotation]  WITH CHECK ADD  CONSTRAINT [FK__CustomQuo__reque__5812160E] FOREIGN KEY([requestId])
REFERENCES [dbo].[RequestForm] ([id])
GO
ALTER TABLE [dbo].[CustomQuotation] CHECK CONSTRAINT [FK__CustomQuo__reque__5812160E]
GO
ALTER TABLE [dbo].[CustomQuotation]  WITH CHECK ADD  CONSTRAINT [FK__CustomQuo__selle__59063A47] FOREIGN KEY([sellerId])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[CustomQuotation] CHECK CONSTRAINT [FK__CustomQuo__selle__59063A47]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK__Material__catego__5812160E] FOREIGN KEY([categoryId])
REFERENCES [dbo].[MaterialCategory] ([id])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK__Material__catego__5812160E]
GO
ALTER TABLE [dbo].[MaterialDetail]  WITH CHECK ADD  CONSTRAINT [FK__MaterialD__mater__59063A47] FOREIGN KEY([materialId])
REFERENCES [dbo].[Material] ([id])
GO
ALTER TABLE [dbo].[MaterialDetail] CHECK CONSTRAINT [FK__MaterialD__mater__59063A47]
GO
ALTER TABLE [dbo].[MaterialDetail]  WITH CHECK ADD  CONSTRAINT [FK__MaterialD__quota__5BE2A6F2] FOREIGN KEY([quotationId])
REFERENCES [dbo].[CustomQuotation] ([id])
GO
ALTER TABLE [dbo].[MaterialDetail] CHECK CONSTRAINT [FK__MaterialD__quota__5BE2A6F2]
GO
ALTER TABLE [dbo].[Pricing]  WITH CHECK ADD  CONSTRAINT [FK__Pricing__Constru__5CD6CB2B] FOREIGN KEY([ConstructTypeId])
REFERENCES [dbo].[ConstructionType] ([id])
GO
ALTER TABLE [dbo].[Pricing] CHECK CONSTRAINT [FK__Pricing__Constru__5CD6CB2B]
GO
ALTER TABLE [dbo].[Pricing]  WITH CHECK ADD  CONSTRAINT [FK__Pricing__Investm__5DCAEF64] FOREIGN KEY([InvestmentTypeId])
REFERENCES [dbo].[InvestmentType] ([id])
GO
ALTER TABLE [dbo].[Pricing] CHECK CONSTRAINT [FK__Pricing__Investm__5DCAEF64]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK__Project__custome__5AEE82B9] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK__Project__custome__5AEE82B9]
GO
ALTER TABLE [dbo].[RequestForm]  WITH CHECK ADD  CONSTRAINT [FK__RequestFo__custo__5FB337D6] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[RequestForm] CHECK CONSTRAINT [FK__RequestFo__custo__5FB337D6]
GO
ALTER TABLE [dbo].[RequestFormMaterial]  WITH CHECK ADD  CONSTRAINT [FK__RequestFo__mater__5CD6CB2B] FOREIGN KEY([materialId])
REFERENCES [dbo].[Material] ([id])
GO
ALTER TABLE [dbo].[RequestFormMaterial] CHECK CONSTRAINT [FK__RequestFo__mater__5CD6CB2B]
GO
ALTER TABLE [dbo].[RequestFormMaterial]  WITH CHECK ADD  CONSTRAINT [FK__RequestFo__reque__619B8048] FOREIGN KEY([requestId])
REFERENCES [dbo].[RequestForm] ([id])
GO
ALTER TABLE [dbo].[RequestFormMaterial] CHECK CONSTRAINT [FK__RequestFo__reque__619B8048]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK__Staff__managerId__628FA481] FOREIGN KEY([managerId])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK__Staff__managerId__628FA481]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK__Staff__username__6383C8BA] FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK__Staff__username__6383C8BA]
GO
ALTER TABLE [dbo].[StandardQuotation]  WITH CHECK ADD  CONSTRAINT [FK__StandardQ__const__6477ECF3] FOREIGN KEY([constructionId])
REFERENCES [dbo].[ConstructionType] ([id])
GO
ALTER TABLE [dbo].[StandardQuotation] CHECK CONSTRAINT [FK__StandardQ__const__6477ECF3]
GO
ALTER TABLE [dbo].[StandardQuotationMaterial]  WITH CHECK ADD  CONSTRAINT [FK__StandardQ__mater__619B8048] FOREIGN KEY([materialId])
REFERENCES [dbo].[Material] ([id])
GO
ALTER TABLE [dbo].[StandardQuotationMaterial] CHECK CONSTRAINT [FK__StandardQ__mater__619B8048]
GO
ALTER TABLE [dbo].[StandardQuotationMaterial]  WITH CHECK ADD  CONSTRAINT [FK__StandardQ__quota__66603565] FOREIGN KEY([quotationId])
REFERENCES [dbo].[StandardQuotation] ([id])
GO
ALTER TABLE [dbo].[StandardQuotationMaterial] CHECK CONSTRAINT [FK__StandardQ__quota__66603565]
GO
ALTER TABLE [dbo].[StandardQuotationTask]  WITH CHECK ADD  CONSTRAINT [FK__StandardQ__quota__6754599E] FOREIGN KEY([quotationId])
REFERENCES [dbo].[StandardQuotation] ([id])
GO
ALTER TABLE [dbo].[StandardQuotationTask] CHECK CONSTRAINT [FK__StandardQ__quota__6754599E]
GO
ALTER TABLE [dbo].[StandardQuotationTask]  WITH CHECK ADD  CONSTRAINT [FK__StandardQ__taskI__6477ECF3] FOREIGN KEY([taskId])
REFERENCES [dbo].[Task] ([id])
GO
ALTER TABLE [dbo].[StandardQuotationTask] CHECK CONSTRAINT [FK__StandardQ__taskI__6477ECF3]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK__Task__categoryId__656C112C] FOREIGN KEY([categoryId])
REFERENCES [dbo].[TaskCategory] ([id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK__Task__categoryId__656C112C]
GO
USE [master]
GO
ALTER DATABASE [SWP391DB] SET  READ_WRITE 
GO

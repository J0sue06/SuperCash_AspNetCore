USE [master]
GO
/****** Object:  Database [supercash]    Script Date: 3/23/2021 1:39:15 PM ******/
CREATE DATABASE [supercash]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'supercash', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\supercash.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'supercash_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\supercash_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [supercash] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [supercash].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [supercash] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [supercash] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [supercash] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [supercash] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [supercash] SET ARITHABORT OFF 
GO
ALTER DATABASE [supercash] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [supercash] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [supercash] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [supercash] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [supercash] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [supercash] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [supercash] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [supercash] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [supercash] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [supercash] SET  ENABLE_BROKER 
GO
ALTER DATABASE [supercash] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [supercash] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [supercash] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [supercash] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [supercash] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [supercash] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [supercash] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [supercash] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [supercash] SET  MULTI_USER 
GO
ALTER DATABASE [supercash] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [supercash] SET DB_CHAINING OFF 
GO
ALTER DATABASE [supercash] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [supercash] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [supercash] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [supercash] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [supercash] SET QUERY_STORE = OFF
GO
USE [supercash]
GO
/****** Object:  User [mperez]    Script Date: 3/23/2021 1:39:15 PM ******/
CREATE USER [mperez] FOR LOGIN [mperez] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [mperez]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [mperez]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [mperez]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [mperez]
GO
ALTER ROLE [db_datareader] ADD MEMBER [mperez]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [mperez]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/23/2021 1:39:16 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[niveles_directo]    Script Date: 3/23/2021 1:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[niveles_directo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nivel] [int] NULL,
	[ganancia] [int] NULL,
	[costo] [int] NULL,
 CONSTRAINT [PK_niveles_directo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[niveles_equipos]    Script Date: 3/23/2021 1:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[niveles_equipos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nivel] [int] NULL,
	[ganancia] [int] NULL,
	[costo] [int] NULL,
 CONSTRAINT [PK_niveles_equipos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pagos]    Script Date: 3/23/2021 1:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pagos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_padre] [int] NULL,
	[monto_trx] [float] NULL,
	[tipo_pago] [varchar](95) NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_pagos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transacciones]    Script Date: 3/23/2021 1:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transacciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[depositado] [float] NOT NULL,
	[recibido] [float] NOT NULL,
	[tipo_pago] [nvarchar](max) NULL,
	[id_transaccion] [nvarchar](max) NULL,
	[fecha] [datetime] NULL,
	[estado] [varchar](95) NULL,
 CONSTRAINT [PK_transacciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 3/23/2021 1:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_padre] [int] NULL,
	[email] [varchar](145) NULL,
	[clave] [varchar](145) NULL,
	[wallet] [varchar](50) NULL,
	[ganancia_directa] [float] NULL,
	[ganancia_equipo] [float] NULL,
	[balance] [float] NULL,
	[rango] [varchar](95) NULL,
	[nivel_directo] [int] NULL,
	[nivel_equipo] [int] NULL,
	[primera_compra] [int] NULL,
	[fecha_registro] [date] NULL,
	[acceso] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [supercash] SET  READ_WRITE 
GO

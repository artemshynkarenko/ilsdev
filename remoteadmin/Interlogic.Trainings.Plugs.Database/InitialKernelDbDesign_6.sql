USE [master]
GO
/****** Object:  Database [ASH_Trainings_RemoteAdmin]    Script Date: 12/09/2008 23:00:27 ******/
CREATE DATABASE [ASH_Trainings_RemoteAdmin] ON  PRIMARY 
( NAME = N'ASH_Trainings_RemoteAdmin', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\ASH_Trainings_RemoteAdmin.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ASH_Trainings_RemoteAdmin_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\ASH_Trainings_RemoteAdmin_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE Ukrainian_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'ASH_Trainings_RemoteAdmin', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ASH_Trainings_RemoteAdmin].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET ARITHABORT OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET  READ_WRITE 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET RECOVERY FULL 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET  MULTI_USER 
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ASH_Trainings_RemoteAdmin] SET DB_CHAINING OFF 
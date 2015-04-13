USE [master]
GO
/****** Object:  Database [FleetDeal]    Script Date: 06/15/2010 16:44:42 ******/
CREATE DATABASE [FleetDeal] ON  PRIMARY 
( NAME = N'FleetDeal', FILENAME = N'E:\Prasad Raskar\Fleet Deal\Db Design\FleetDeal.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FleetDeal_log', FILENAME = N'E:\Prasad Raskar\Fleet Deal\Db Design\FleetDeal_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'FleetDeal', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FleetDeal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FleetDeal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FleetDeal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FleetDeal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FleetDeal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FleetDeal] SET ARITHABORT OFF 
GO
ALTER DATABASE [FleetDeal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FleetDeal] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [FleetDeal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FleetDeal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FleetDeal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FleetDeal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FleetDeal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FleetDeal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FleetDeal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FleetDeal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FleetDeal] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FleetDeal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FleetDeal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FleetDeal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FleetDeal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FleetDeal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FleetDeal] SET  READ_WRITE 
GO
ALTER DATABASE [FleetDeal] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FleetDeal] SET  MULTI_USER 
GO
ALTER DATABASE [FleetDeal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FleetDeal] SET DB_CHAINING OFF 
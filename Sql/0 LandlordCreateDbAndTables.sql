USE master;
--IF DB_ID('Landlord') IS NOT NULL
   
CREATE DATABASE Landlord;
GO

USE [Landlord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE AreaTypes(AreaTypeID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
					  AreaTypeName [nvarchar](255) not null); 
GO

CREATE TABLE Areas(
		[AreaID][int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
		[AreaTypeID] int not null foreign key references [dbo].AreaTypes(AreaTypeID),
        [AreaDescription] [nvarchar](MAX) NOT NULL,
		[OwnerName] [nvarchar](300) NOT NULL,
        [ContactaName] [nvarchar](300) NULL,
        [ContactaPhone1] [nvarchar](21) NOT NULL,
        [ContactaPhone2] [nvarchar](21) NULL,
        [ContactaPhone3] [nvarchar](21) NULL,
        [LegalAddressRegion] [nvarchar](300) NULL,
		[LegalAddressCity] [nvarchar](300) NULL,
		[LegalAddressStreet] [nvarchar](300) NULL,
        [RentAreaAddressRegion] [nvarchar](50) NOT NULL,
		[RentAreaAddressCity] [nvarchar](100) NOT NULL,
		[RentAreaAddressStreet] [nvarchar](300) NOT NULL,
        [SquareArea] [numeric](10, 2) NOT NULL,
        [MonthPrice] [numeric](10, 2) NOT NULL,
        [IsAvailable] [bit] NOT NULL,
        [Rating] [int] NOT NULL,
        [Latitude] [numeric](16, 4) NULL,
        [Longitude] [numeric](16, 4) NULL);



CREATE TABLE Photos(PhotoID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
					AreaID int not null foreign key references [dbo].[Areas](AreaID),
					PathToPhoto [nvarchar](255) not null,
					PhotoName [nvarchar](255) not null,
					PhotoExtension [nvarchar](255) not null,
					Latitude numeric(16, 4) NULL,
					Longitude numeric(16, 4) NULL); 

GO
SET ANSI_PADDING OFF
GO


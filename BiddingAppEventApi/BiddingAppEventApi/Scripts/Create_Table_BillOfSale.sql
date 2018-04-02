USE [Stage_Purchasing]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BillOfSale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[vehicleID] [nvarchar](255) NULL,
	[vehicleMake] [nvarchar](50) NULL,
	[vehicleYear] [nvarchar](50) NULL,
	[vehicleModel] [nvarchar](50) NULL,
	[vehicleVin] [nvarchar](20) NULL,
	[vehicleOdoMeter] [nvarchar](20) NULL,
	[vehicleExterior] [nvarchar](255) NULL,
	[vehicleInterior] [nvarchar](255) NULL,
	[vehicleXLE] [nvarchar](255) NULL,
	[vehicleMPG] [nvarchar](100) NULL,
	[vehicleOptions] [nvarchar](500) NULL,
	[vehicleDescription] [nvarchar](500) NULL,
	[vehicleSubmittedOn] [datetime] NULL,
	[PONumber] [nvarchar](50) NULL,
	[DateOfPurchase] [datetime] NULL,
	[PurchasePrice] [float] NULL,
	[buyerFirstName] [nvarchar](255) NULL,
	[buyerLastName] [nvarchar](255) NULL,
	[buyerID] [int] NULL,
	[buyerEmail] [nvarchar](255) NULL,
	[buyerPhone] [nvarchar](20) NULL,
	[buyerRole] [nvarchar](20) NULL,
	[buyerIsActive] [nvarchar](50) NULL,
	[sellerFirstName] [nvarchar](255) NULL,
	[sellerLastName] [nvarchar](255) NULL,
	[sellerID] [int] NULL,
	[sellerEmail] [nvarchar](255) NULL,
	[sellerPhone] [nvarchar](20) NULL,
	[sellerRole] [nvarchar](20) NULL,
	[sellerIsActive] [nvarchar](1) NULL,
	[dealershipName] [nvarchar](255) NULL,
	[dealershipStreetAddress] [nvarchar](255) NULL,
	[dealershipCity] [nvarchar](255) NULL,
	[dealershipState] [nvarchar](255) NULL,
	[dealershipZipCode] [nvarchar](255) NULL,
	[dealershipCountry] [nvarchar](255) NULL,
	[dealershipContactName] [nvarchar](255) NULL,
	[dealershipContactPhone] [nvarchar](20) NULL,
	[dealershipContactEmail] [nvarchar](255) NULL,
	[dealershipIsActive] [nvarchar](1) NULL,
	[Insert_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BillOfSale] ADD  CONSTRAINT [DF_BillOfSale_Insert_Date]  DEFAULT (getdate()) FOR [Insert_Date]
GO



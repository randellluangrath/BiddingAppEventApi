USE [Stage_Purchasing]
GO

/****** Object:  View [dbo].[vwBillOfSale]    Script Date: 12/14/2017 11:18:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER VIEW [dbo].[vwBillOfSale]

AS

SELECT DISTINCT 
 a.vehicleVin as VIN,
 a.dealershipName as DealershipName,
 a.dealershipStreetAddress as DealershipStreetAddress,
 a.dealershipCity as DealershipCity,
 a.dealershipState as DealershipState,
 a.dealershipZipCode as DealershipZipCode,
 a.dealershipCountry as DealershipCountry,
 a.dealershipContactName as DealershipContactName,
 a.dealershipContactPhone as DealershipContactPhone,
 a.dealershipContactEmail as DealershipContactEmail,
 a.buyerFirstName as BuyerFirstName,
 a.buyerLastName as BuyerLastName,
 a.buyerPhone as BuyerPhone,
 a.buyerEmail as BuyerEmail,
 b.Insert_Date
FROM [dbo].[Event] a
 INNER JOIN 
 (
	SELECT
	 vehicleVin,
	 MAX(Insert_Date) as Insert_Date
	FROM [dbo].[Event] 
	GROUP BY vehicleVin
 ) b on a.vehicleVin = b.vehicleVin AND b.Insert_Date = a.Insert_Date
WHERE a.DateOfPurchase IS NOT NULL

GO



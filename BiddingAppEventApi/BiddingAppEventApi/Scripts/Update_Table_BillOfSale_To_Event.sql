EXEC sp_rename 'BillOfSale', 'Event';  
ALTER TABLE [dbo].[Event] DROP  CONSTRAINT [DF_BillOfSale_Insert_Date] 
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_Insert_Date]  DEFAULT (getdate()) FOR [Insert_Date]
GO
ALTER TABLE [dbo].[EVENT] ADD CONSTRAINT UC_VEHICLE_ID UNIQUE (vehicleID)
GO
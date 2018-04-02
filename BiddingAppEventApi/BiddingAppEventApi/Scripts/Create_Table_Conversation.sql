﻿USE [Stage_Purchasing]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Conversation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VehicleID] [nvarchar](255) NULL,
	[ConversationID] [nvarchar](255) NULL,
	[Message] [nvarchar](255) NULL,
	[PostedBy] [nvarchar](50) NULL,
	[PostedOn] [DATETIME] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO








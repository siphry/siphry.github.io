﻿CREATE TABLE [dbo].[Records]
(
	[ID] INT IDENTITY(0,1) NOT NULL,
	[Date]		DateTime NOT NULL,
	[Input]		NVARCHAR(128) NOT NULL,
	[GiphyURL]  NVARCHAR(128) NOT NULL,
	[IP]		VARCHAR(16) NOT NULL,
	[Browser-Agent]	VARCHAR(255) NOT NULL
	CONSTRAINT [PK_dbo.Records] PRIMARY KEY CLUSTERED ([ID] ASC)
);
ALTER TABLE [dbo].[Bids] DROP CONSTRAINT  [FK_Bids_Items];
ALTER TABLE [dbo].[Bids] DROP CONSTRAINT  [FK_Bids_Buyers];
ALTER TABLE [dbo].[Items] DROP CONSTRAINT  [FK_Items_Sellers];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Buyers];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Sellers];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Items];

-- Remove Table from OurItems database
DROP TABLE [dbo].[Bids];
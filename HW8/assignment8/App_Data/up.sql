CREATE TABLE [dbo].[Buyers]
(
	[BuyerId] INT IDENTITY (0, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL
)

CREATE TABLE [dbo].[Sellers]
(
	[SellerId] INT IDENTITY (0, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NOT NULL
)

CREATE TABLE [dbo].[Items]
(
	[ItemId] INT IDENTITY (1001, 1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL, 
	[Seller] INT NOT NULL,
    CONSTRAINT [FK_Items_Sellers] FOREIGN KEY ([Seller]) REFERENCES [Sellers]([SellerId])
)

CREATE TABLE [dbo].[Bids]
(	
	[Item] INT NOT NULL,
    CONSTRAINT [FK_Bids_Items] FOREIGN KEY ([Item]) REFERENCES [Items]([ItemId]),
	[Buyer] INT NOT NULL,
    CONSTRAINT [FK_Bids_Buyers] FOREIGN KEY ([Buyer]) REFERENCES [Buyers]([BuyerId]), 
    [Price] INT NOT NULL, 
    [Timestamp] DATETIME NOT NULL
)

INSERT INTO [dbo].[Buyers](Name) VALUES
('Jane Stone'),
('Tom McMasters'),
('Otto Vanderwall');

INSERT INTO [dbo].[Sellers](Name) VALUES
('Gayle Hardy'),
('Lyle Banks'),
('Pearl Greene');

INSERT INTO [dbo].[Items](Name, Description, Seller) VALUES
('Abraham Lincoln Hammer','A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 2),
('Albert Einsteins Telescope','A brass telescope owned by Albert Einstein in Germany, circa 1927', 0),
('Bob Dylan Love Poems','Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 1);

INSERT INTO [dbo].[Bids](Item, Buyer, Price, Timestamp) VALUES
(1001, 2, 250000,'12/04/2017 09:04:22'),
(1003, 0, 95000,'12/04/2017 08:44:03');
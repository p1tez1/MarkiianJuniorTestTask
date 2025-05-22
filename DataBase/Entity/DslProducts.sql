CREATE TABLE [dbo].[DslProducts]
(
	[Id] Uniqueidentifier NOT NULL PRIMARY KEY Default newid(),
	[CustomerId] Uniqueidentifier not null,
	[Product] Nvarchar(50) not null,
	[StartDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [EndDate] DATETIME NULL
)

CREATE TABLE [dbo].[MatchedCustomers]
(
	[Id] Uniqueidentifier NOT NULL PRIMARY KEY Default newid(),
	[CustomerTvId] Uniqueidentifier NOT NULL,
	[CustomerDslId] Uniqueidentifier not null,
	[StartDate] DateTime not null
)

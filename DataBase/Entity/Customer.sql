CREATE TABLE [dbo].[Customer]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY Default newid(),
    [Email] NVARCHAR(50) NOT NULL,
    [Address] NVARCHAR(50) NOT NULL
)


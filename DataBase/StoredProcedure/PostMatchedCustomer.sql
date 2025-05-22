CREATE PROCEDURE PostMatchedCustomer
    @CustomerTvId UNIQUEIDENTIFIER,
    @CustomerDslId UNIQUEIDENTIFIER,
    @StartDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM MatchedCustomers
        WHERE CustomerTvId = @CustomerTvId
          AND CustomerDslId = @CustomerDslId
          AND StartDate = @StartDate
    )
    BEGIN
        INSERT INTO MatchedCustomers (CustomerTvId, CustomerDslId, StartDate)
        VALUES (@CustomerTvId, @CustomerDslId, @StartDate);
    END
END;

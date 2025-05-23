CREATE PROCEDURE FindAndPostMatchedCustomers
AS
BEGIN
    SET NOCOUNT ON;

    CREATE TABLE #InsertedMatched (
        CustomerTvId UNIQUEIDENTIFIER,
        CustomerDslId UNIQUEIDENTIFIER,
        StartDate DATETIME
    );

    INSERT INTO MatchedCustomers (CustomerTvId, CustomerDslId, StartDate)
    OUTPUT inserted.CustomerTvId, inserted.CustomerDslId, inserted.StartDate
        INTO #InsertedMatched
    SELECT DISTINCT
        c1.Id AS CustomerIdTv,
        c2.Id AS CustomerIdDsl,
        tv.StartDate
    FROM TvProducts tv
    JOIN Customer c1 ON tv.CustomerId = c1.Id
    JOIN DslProducts dsl ON 1 = 1
    JOIN Customer c2 ON dsl.CustomerId = c2.Id
    WHERE 
        c1.Email = c2.Email
        AND c1.Address = c2.Address
        AND c1.Id <> c2.Id
        AND NOT EXISTS (
            SELECT 1
            FROM MatchedCustomers mc
            WHERE mc.CustomerTvId = c1.Id
              AND mc.CustomerDslId = c2.Id
              AND mc.StartDate = tv.StartDate
        );

    SELECT * FROM #InsertedMatched;

    DROP TABLE #InsertedMatched;
END;

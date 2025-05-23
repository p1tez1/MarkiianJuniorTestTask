CREATE PROCEDURE [dbo].[FindMatchedCustomer]
	AS
BEGIN
    SET NOCOUNT ON;

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
    ORDER BY tv.StartDate;
END;

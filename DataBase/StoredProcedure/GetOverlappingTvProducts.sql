CREATE PROCEDURE GetOverlappingTvProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        a.Id AS ProductAId,
        c.Id AS CustomerId,
        c.Email,
        c.Address,
        a.Product AS ProductAName,
        a.StartDate AS ProductAStartDate,
        a.EndDate AS ProductAEndDate,
        b.Id AS ProductBId,
        b.Product AS ProductBName,
        b.StartDate AS ProductBStartDate,
        b.EndDate AS ProductBEndDate
    FROM CustomersTvProducts a
    JOIN CustomersTvProducts b
        ON a.CustomerId = b.CustomerId
       AND a.Id < b.Id
    JOIN Customer c
        ON a.CustomerId = c.Id
    WHERE 
        a.StartDate < GETDATE() AND (a.EndDate IS NULL OR a.EndDate > GETDATE()) AND
        b.StartDate < GETDATE() AND (b.EndDate IS NULL OR b.EndDate > GETDATE()) AND
        a.StartDate < ISNULL(b.EndDate, '9999-12-31') AND
        ISNULL(a.EndDate, '9999-12-31') > b.StartDate;
END;

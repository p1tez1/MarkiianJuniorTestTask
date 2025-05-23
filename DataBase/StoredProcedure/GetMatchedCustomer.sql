CREATE PROCEDURE [dbo].[GetMatchedCustomer]
	AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CustomerTvId, 
        CustomerDslId, 
        StartDate 
    FROM MatchedCustomers;
END;

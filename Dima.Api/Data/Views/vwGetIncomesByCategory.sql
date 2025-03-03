CREATE OR ALTER VIEW [vwGetIncomesByCategory] AS
    SELECT 
        T.[UserId],
        C.[Title] AS Category,
        YEAR(T.[PaidOrReceivedAt]) AS [Year],
        SUM(T.[Amount]) AS [Incomes]
    FROM dbo.Transacoes as T INNER JOIN dbo.Categorias C 
        ON C.Id = T.CategoryId
    WHERE 
        T.[PaidOrReceivedAt] >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
    AND T.[PaidOrReceivedAt] <  DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
    AND T.[Type] = 1
    GROUP BY T.[UserId], C.[Title], YEAR(T.[PaidOrReceivedAt]) 
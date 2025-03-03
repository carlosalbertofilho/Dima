CREATE OR ALTER VIEW [vwGetExpensesByCategory] AS
    SELECT T.[UserId],
           C.[Title]                  AS [Category],
           YEAR(T.[PaidOrReceivedAt]) AS [Year],
           SUM(T.[Amount])            AS [Expenses]
    FROM dbo.[Transacoes] AS T INNER JOIN dbo.[Categorias] AS C 
        ON C.[Id] = T.[CategoryId]
    WHERE 
        T.[PaidOrReceivedAt] >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
    AND T.[PaidOrReceivedAt] < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
    AND T.[Type] = 2
    GROUP BY T.[UserId], C.[Title], YEAR(T.[PaidOrReceivedAt])

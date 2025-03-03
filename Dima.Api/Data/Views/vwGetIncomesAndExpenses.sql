CREATE OR ALTER VIEW [vwGetIncomesAndExpenses] AS
    SELECT T.[UserId],
           MONTH(T.[PaidOrReceivedAt])           AS [Month],
           YEAR(T.[PaidOrReceivedAt])            AS [Year],
           SUM(IIF(T.[Type] = 1, T.[Amount], 0)) AS [Incomes],
           SUM(IIF(T.[Type] = 2, T.[Amount], 0)) AS [Expenses]
    FROM dbo.Transacoes AS T
    WHERE 
        T.[PaidOrReceivedAt] >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
    AND T.[PaidOrReceivedAt] <  DATEADD(MONTH,   1, CAST(GETDATE() AS DATE))
    GROUP BY T.[UserId], MONTH(T.[PaidOrReceivedAt]), YEAR(T.[PaidOrReceivedAt]) 
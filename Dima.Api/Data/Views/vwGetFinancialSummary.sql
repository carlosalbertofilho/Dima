CREATE OR ALTER VIEW [vwGetFinancialSummary] AS
    SELECT 
        T.UserId,
        SUM(IIF(T.[Type] = 1, T.[Amount], 0)) AS [Incomes],
        SUM(IIF(T.[Type] = 2, T.[Amount], 0)) AS [Expenses]
    FROM dbo.Transacoes as T
    WHERE 
        T.[PaidOrReceivedAt] >= DATEADD(MONTH, 0, CAST(GETDATE() AS DATE))
    GROUP BY  t.[UserId]

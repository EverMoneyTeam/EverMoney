DELETE FROM dbo.Accounts
DELETE FROM dbo.HistoryChanges

INSERT INTO dbo.HistoryChanges
(
	Id,
    AccountId,
    [Column],
    RowId,
    [Table],
    USN,
    Value
)
VALUES
(	NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Id',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000010',  -- RowId - nvarchar(max)
    N'CashAccount',  -- Table - nvarchar(max)
    1,    -- USN - int
    N''   -- Value - nvarchar(max)
    ),

(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'AccountId',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000010',  -- RowId - nvarchar(max)
    N'CashAccount',  -- Table - nvarchar(max)
    2,    -- USN - int
    N'10000000-0000-0000-0000-000000000000'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Amount',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000010',  -- RowId - nvarchar(max)
    N'CashAccount',  -- Table - nvarchar(max)
    3,    -- USN - int
    N'1000'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'CurrencyId',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000010',  -- RowId - nvarchar(max)
    N'CashAccount',  -- Table - nvarchar(max)
    4,    -- USN - int
    N'00000000-0000-0000-0000-000000000001'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Name',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000010',  -- RowId - nvarchar(max)
    N'CashAccount',  -- Table - nvarchar(max)
    5,    -- USN - int
    N'My wallet'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Id',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000100',  -- RowId - nvarchar(max)
    N'CashFlowCategory',  -- Table - nvarchar(max)
    6,    -- USN - int
    N''   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'AccountId',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000100',  -- RowId - nvarchar(max)
    N'CashFlowCategory',  -- Table - nvarchar(max)
    7,    -- USN - int
    N'10000000-0000-0000-0000-000000000000'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Name',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000000100',  -- RowId - nvarchar(max)
    N'CashFlowCategory',  -- Table - nvarchar(max)
    8,    -- USN - int
    N'Main Category'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Id',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    9,    -- USN - int
    N''   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Amount',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    10,    -- USN - int
    N'-100'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'CashAccountId',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    11,    -- USN - int
    N'00000000-0000-0000-0000-000000000010'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'CashFlowCategoryId',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    12,    -- USN - int
    N'00000000-0000-0000-0000-000000000100'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Date',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    13,    -- USN - int
    N'2017-10-10 13:23:44'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'Description',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    14,    -- USN - int
    N'Synced CashFlow'   -- Value - nvarchar(max)
    ),
	
(   NEWID(),   
	N'10000000-0000-0000-0000-000000000000',  -- AccountId - nvarchar(max)
    N'AccountId',  -- Column - nvarchar(max)
    N'00000000-0000-0000-0000-000000001000',  -- RowId - nvarchar(max)
    N'CashFlow',  -- Table - nvarchar(max)
    15,    -- USN - int
    N'10000000-0000-0000-0000-000000000000'   -- Value - nvarchar(max)
    )
	
INSERT INTO dbo.Accounts
(
    Id,
    Login,
    Password
)
VALUES
(   '10000000-0000-0000-0000-000000000000', -- Id - uniqueidentifier
    N'login',  -- Login - nvarchar(max)
    N'$2b$10$kl7GoNXuuA8OOdp5N4dDYuEh64MdkXwLeF6oWnaiOAsqax2o5AIo.'   -- Password - nvarchar(max)
    )

	

SELECT * FROM dbo.HistoryChanges
ORDER BY USN

SELECT * FROM dbo.Accounts
CREATE TABLE [dbo].[Matrices] (
    [MatrixName] VARCHAR (24) NOT NULL,
    [RowNo]      SMALLINT     NOT NULL,
    [ColNo]      SMALLINT     NOT NULL,
    [CellValue]  VARCHAR (15) NULL,
    CONSTRAINT [PK_Matrices] PRIMARY KEY CLUSTERED ([MatrixName] ASC, [RowNo] ASC, [ColNo] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Matrices]
    ON [dbo].[Matrices]([ColNo] ASC, [RowNo] ASC);
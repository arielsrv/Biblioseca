alter table dbo.Borrows alter column BorrowedAt datetime2 not null
go

alter table dbo.Borrows alter column ReturnedAt datetime2 null
go
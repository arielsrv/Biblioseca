alter table dbo.Authors
    add Deleted bit default 0 not null
    go

alter table dbo.Books
    add Deleted bit default 0 not null
    go

-- Agregar las entidades que falten
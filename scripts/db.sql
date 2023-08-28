create table dbo.Authors
(
    Id        int identity
        constraint PK_Authors
        primary key,
    FirstName nvarchar(200) not null,
    LastName  nvarchar(200) not null
)
    go

create table dbo.Categories
(
    Id   int identity
        constraint PK_Categories
        primary key,
    Name nvarchar(200) not null
)
    go

create table dbo.Books
(
    Id          int identity
        constraint PK_Books
        primary key,
    Title       nvarchar(200) not null,
    ISBN        nvarchar(200) not null,
    Description nvarchar(500) not null,
    AuthorId    int not null
        constraint FK_Books_Authors
            references dbo.Authors,
    CategoryId  int
        constraint FK_Books_Categories
            references dbo.Categories,
    Price       decimal
)
    go

create table dbo.Partners
(
    Id        int identity
        constraint PK_Partners
        primary key,
    FirstName nvarchar(50) not null,
    LastName  nvarchar(50) not null,
    Username  nvarchar(50) not null
)
    go

create table dbo.Borrows
(
    Id         int identity
        constraint Borrows_pk
        primary key nonclustered,
    BookId     int      not null
        constraint Borrows_Books_Id_fk
            references dbo.Books,
    PartnerId  int      not null
        constraint Borrows_Partners_Id_fk
            references dbo.Partners,
    BorrowedAt datetime not null,
    ReturnedAt datetime
)
    go


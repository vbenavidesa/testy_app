CREATE TABLE ContactMasters
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Status NVARCHAR(1),
    CreatedDate DATETIME2(7) NOT NULL,
    CreatedBy NVARCHAR(40),
    UpdatedDate DATETIME2(7) NOT NULL,
    UpdatedBy NVARCHAR(40)
);
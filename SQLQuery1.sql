CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName VARCHAR(100) NOT NULL
);

CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(150) NOT NULL,
    CategoryID INT NOT NULL,    
    Price DECIMAL(10,2) NOT NULL,
    StockQty INT NOT NULL DEFAULT 0,
    Status VARCHAR(20) NOT NULL DEFAULT 'Active',

    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

CREATE TABLE InventoryLog (
    InvID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    Qty_In INT DEFAULT 0,
    Qty_Out INT DEFAULT 0,
    Date DATETIME NOT NULL DEFAULT GETDATE(),
    Remarks VARCHAR(200),

    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL
);

CREATE TABLE SalesItems (
    SalesItemID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName VARCHAR(150) NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(150) NOT NULL
);

CREATE TABLE Appointments (
    AppointID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    ServiceID INT NOT NULL,
    DateBooked DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending',

    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

CREATE TABLE ServiceHistory (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    AppointID INT NOT NULL,
    ProductUsedID INT,
    QuantityUsed INT,
    Notes VARCHAR(255),

    FOREIGN KEY (AppointID) REFERENCES Appointments(AppointID),
    FOREIGN KEY (ProductUsedID) REFERENCES Product(ProductID)
);
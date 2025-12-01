--ALTER TABLE Customers
--ADD Contact VARCHAR(50) NOT NULL,
--    Address VARCHAR(255)

--INSERT INTO Category (CategoryName) VALUES ('Food');
--INSERT INTO Category (CategoryName) VALUES ('Accessories');
--INSERT INTO Category (CategoryName) VALUES ('Health Supply');
--INSERT INTO Category (CategoryName) VALUES ('Grooming');

--ALTER TABLE InventoryLog
--DROP COLUMN Remarks;

--UPDATE Product
--SET Name = @Name,
--    CategoryID = @CategoryID,
--    Price = @Price,
--    StockQty = @StockQty,
--    Status = @Status
--WHERE ProductID = @Id

--ALTER TABLE InventoryLog
--DROP CONSTRAINT FK__Inventory__Produ__403A8C7D;

--ALTER TABLE InventoryLog
--ADD CONSTRAINT FK_InventoryLog_Product
--FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
--ON DELETE CASCADE;


--DROP TABLE ServiceHistory;

--ALTER TABLE Customers
--DROP COLUMN Address;

---- 2. Add Barangay and Province columns
--ALTER TABLE Customers
--ADD Barangay VARCHAR(100),
--    Province VARCHAR(100);

--ALTER TABLE Customers
--DROP COLUMN Name;

--ALTER TABLE Customers
--ADD FirstName VARCHAR(50) NOT NULL,
--    LastName VARCHAR(50) NOT NULL;

--INSERT INTO Services (ServiceName, Price)
--Values
--	('Pet Grooming', 899),
--	('Pet Boarding', 599),
--	('Pet Sitting', 349),
--	('Dog Walking', 199),
--	('Pet Daycare', 149),
--	('Vet Services', 1099)


--SELECT * FROM Appointments
--SELECT * FROM Category
--SELECT * FROM Customers
--SELECT * FROM InventoryLog	
--SELECT * FROM Product
--SELECT * FROM Sales
--SELECT * FROM SalesItems
--SELECT * FROM Services































--DELETE FROM Sales WHERE SaleID = 33


--SELECT COUNT(*)
--FROM Appointments
--WHERE Status = 'Completed'
--AND DateBooked >= DATEADD(WEEK, DATEDIFF(WEEK, 0, DATEADD(DAY, -1, GETDATE())), 0)
--AND DateBooked< DATEADD(WEEK, DATEDIFF(WEEK, 0, DATEADD(DAY, -1, GETDATE())) + 1, 0)


INSERT INTO Customer(FirstName, LastName, CustomerAddress) VALUES
	('Harry', 'Potter', 'The Cupboard Under the Stairs'),
	('Ron', 'Weasley', 'The Burrow'),
	('Hermione', 'Granger', 'London, England'),
	('Albus', 'Dumbledore', 'Headmasters Study, Hogwarts'),
	('Severus', 'Snape', 'Dungeons, Hogwarts'),
	('Rubeus', 'Hagrid', 'The Cabin, Hogwarts'),
	('Arthur', 'Weasley', 'The Burrow'),
	('Molly', 'Weasley', 'The Burrow'),
	('Charlie', 'Weasley', 'Romainia'),
	('Bill', 'Weasley', 'Shell Cottage'),
	('Percy', 'Weasley', 'Desk 4, The Ministry of Magic'),
	('Fred', 'Weasley', 'The Burrow'),
	('George', 'Weasley', 'The Burrow'),
	('Ginny', 'Weasley', 'The Burrow')

--SELECT * FROM Customer

INSERT INTO Product(ProductName, ProductDescription, UnitCost) VALUES
	('Cauldron Cake', 'Chocolatey Goodness','1.49'),
	('Pepsi', 'Inflicts 2d10 posion damage when consumed', '.99'),
	('Pumpkin Pasty', 'Halloween, all year long', '1.19'),
	('Chocolate Frog', 'Dumbledore Again...','1.69'),
	('Treacle Tart', 'Wizards need Poptarts too','1.89'),
	('Licorice Wand', 'No one ever buys the black ones.','.20'),
	('Butterbeer', 'Southern Cooking + Irish Hops','2.49'),
	('Firewhisky', 'Why are we selling this stuff to children?','4.99'),
	('Bertie Botts Every Flavor Beans', 'They mean every flavor!','2.69')

--SELECT * FROM Product

INSERT INTO Location(LocationName, LocationAddress) VALUES
	('The Trolley', 'Hallway, Hogwarts Express'),
	('Fred and Georges Underground Candy Shop', 'Compartment B-4, Hogwarts Express')

--SELECT * FROM Location

INSERT INTO Inventory(LocationId, ProductId, Quantity) VALUES
	(1, 1, 15),
	(1, 3, 60),
	(1, 4, 10),
	(1, 5, 17),
	(1, 6, 500),
	(1, 7, 20),

	(2, 2, 45),
	(2, 8, 20),
	(2, 9, 30)


--SELECT * FROM Inventory 

INSERT INTO Receipt(LocationId, CustomerId, ReceiptTimestamp) VALUES
	(1, 1, GETDATE()),
	(1, 1, GETDATE()),
	(2, 1, GETDATE()),
	
	(1, 2, GETDATE()),
	(1, 2, GETDATE()),
	(1, 3, GETDATE()),
	(1, 4, GETDATE()),
	(1, 5, GETDATE()),
	(1, 6, GETDATE()),
	(1, 7, GETDATE()),
	(1, 9, GETDATE()),
	(1, 9, GETDATE()),
	(1, 10, GETDATE()),
	(1, 11, GETDATE()),

	(2, 12, GETDATE()),
	(2, 13, GETDATE()),
	(2, 14, GETDATE())



--SELECT * FROM Receipt
--2 8 9
INSERT INTO Basket(ReceiptId, ProductId, Quantity) VALUES
	(1, 2, 7),
	(1, 6, 4),
	(1, 8, 1),

	(2, 1, 3),
	(3, 1, 7),
	(4, 1, 4),
	(5, 1, 6),
	(6, 1, 2),
	(7, 4, 15),
	(8, 1, 12),
	(9, 1, 6),
	(10, 1, 8),
	(11, 1, 5),
	(12, 1, 3),
	
	(13, 2, 6),
	(13, 8, 1),
	(14, 2, 4),
	(14, 8, 1),
	(14, 9, 3),
	(15, 8, 1)

SELECT * FROM Customer
SELECT * FROM Product
SELECT * FROM Location
SELECT * FROM Inventory
SELECT * FROM Receipt
SELECT * FROM Basket

